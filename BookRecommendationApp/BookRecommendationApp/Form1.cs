using BookRecommendationApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Reactive.Linq;
using Firebase.Auth;
using Firebase.Database;

namespace BookRecommendationApp
{
    public partial class FormSignIn : Form
    {
        public FormSignIn()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoadFirebase(textBox1.Text, textBox2.Text)) 
            {
                FormMain main = new FormMain();
                main.Show(this);
                main.FormClosing += (obj, args) => this.Visible = true;
                this.Visible = false;
            }
        }

        #region Constant
        private const string SignUpFailedPrompt =
            "Đã có lỗi xảy ra. Không đăng kí được.";
        private const string SignUpYesNoPromptContent =
            "Không nhận ra bạn. Bạn có muốn đăng kí?";
        private const string SignUpPromptTitle = "Đăng kí?";
        private const string SignInFailedPrompt
            = "Không thể đăng nhập. Xin thử lại sau ít phút.";
        private const int TimeOut = 10000;
        private const string LoadDataFromFirebaseFailed = 
            "Không truy cập được hệ thống dữ liệu.\nXin thử lại sau ít phút.";
        #endregion

        #region FirebaseSetting
        // TODO: export to file
        private const string firebaseApiKey = "AIzaSyDu098TxwLgFJsfaenUPBfC1z9jyGGT2N8";
        private const string databaseURL = "https://fir-test-bd7d1.firebaseio.com";

        private FirebaseAuthProvider authProvider =
            new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
        private FirebaseClient client = null;
        private FirebaseAuthLink token = null;
        private Task refreshToken;
        #endregion

        #region LoadData
        private void SignUp(string email, string password)
        {
            var authActionSignUp = authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            authActionSignUp.Wait();
            if (authActionSignUp.IsFaulted)
                MessageBox.Show(SignUpFailedPrompt);
        }

        private FirebaseClient SignIn(string email, string password, bool triedOnce = false)
        {
            var authActionSignIn = authProvider.SignInWithEmailAndPasswordAsync(email, password);
            bool error = false;
            try { authActionSignIn.Wait(); }
            catch { error = true; }
            if (authActionSignIn.IsFaulted || error)
            {
                if (triedOnce)
                {
                    MessageBox.Show(SignInFailedPrompt);
                    return null;
                }
                else
                {
                    DialogResult result =
                        MessageBox.Show(SignUpYesNoPromptContent,
                        SignUpPromptTitle, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try { SignUp(email, password); }
                        catch
                        {
                            MessageBox.Show(SignUpFailedPrompt);
                            return null;
                        }
                        return SignIn(email, password);
                    }
                    else return null;
                }
            }
            token = authActionSignIn.Result;
            return new FirebaseClient(databaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken)
                });
        }

        #region Tasks
        private bool LoadBook()
        {
            var task = client.Child("Books").OnceAsync<Book>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                var taskResult = task.Result;
                Database.Books = taskResult.Select(item => item.Object).ToList();
                return task.IsFaulted;
            }
            else return false;
        }
        private bool LoadTags()
        {
            var task = client.Child("Tags").OnceSingleAsync<List<string>>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.Tags = task.Result;
                return task.IsFaulted || (!task.IsCompleted);
            }
            else return false;
        }
        private bool LoadUser()
        {
            client.Child("Users").Child(token.User.LocalId).PutAsync(new Model.User
            {
                BookListID = new List<string>(),
                Score = 0
            }).Wait();
            var task = client.Child("Users").Child(token.User.LocalId).OnceSingleAsync<Model.User>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.User = task.Result;
                return true;
            }
            else return false;
        }
        private bool LoadSetting()
        {
            var task = client.Child("Setting").OnceSingleAsync<Setting>(new TimeSpan(0, 0, 2));
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.Setting = task.Result;
                return true;
            }
            else return false;
        }
        public async Task RefreshToken()
        {
            while (true)
            {
                var task = token.GetFreshAuthAsync();
                try { task.Wait(); } catch { continue; }
                token = task.Result;
                await Task.Delay(token.ExpiresIn);
            }
        }
        #endregion

        public bool LoadFirebase(string username, string password)
        {
            if (client == null)
                client = SignIn(username, password);
            if (client == null)
                return false;

            refreshToken = RefreshToken();
            bool result = true;
            result &= LoadBook();
            result &= LoadTags();
            result &= LoadUser();
            result &= LoadSetting();

            if (result == false)
                MessageBox.Show(LoadDataFromFirebaseFailed);

            return result;
        }
        #endregion

        #region CloseFirebase
        public void CloseFirebase()
        {
            client.Dispose();
            refreshToken.Dispose();
            client = null;
            token = null;
            refreshToken = null;
        }
        #endregion
    }

    public partial class FormMain : Form { }
}
