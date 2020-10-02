using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace BookRecommendationApp.Model
{
    public partial class Database
    {
        #region Database
        private static List<Book> s_books;
        private static List<string> s_author;
        private static Setting s_setting;
        private static List<string> s_tags;
        private static User s_user;

        static public List<Book> Books => s_books;
        static public List<string> Tags => s_tags;
        static public User User => s_user;
        static public Setting Setting => s_setting;
        #endregion

        #region ConstantString
        private const string SignUpFailedPrompt =
            "Đã có lỗi xảy ra. Không đăng kí được.";
        private const string SignUpYesNoPromptContent =
            "Không nhận ra bạn. Bạn có muốn đăng kí?";
        private const string SignUpPromptTitle = "Đăng kí?";
        private const string SignInFailedPrompt
            = "Không thể đăng nhập. Xin thử lại sau ít phút.";
        private const int TimeOut = 10000;
        #endregion

        #region FirebaseSetting
        // TODO: export to file
        static private string firebaseApiKey = "AIzaSyDu098TxwLgFJsfaenUPBfC1z9jyGGT2N8";
        static private string databaseURL = "https://fir-test-bd7d1.firebaseio.com";

        static private FirebaseAuthProvider authProvider =
            new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
        static private FirebaseClient client = null;
        static private FirebaseAuthLink token = null;
        #endregion

        #region LoadData
        static private void SignUp(string email, string password)
        {
            var authActionSignUp = authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            authActionSignUp.Wait();
            if (authActionSignUp.IsFaulted)
                MessageBox.Show(SignUpFailedPrompt);
        }

        static private FirebaseClient SignIn(string email, string password, bool triedOnce = false)
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
                        catch {
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
        private static bool LoadBook()
        {
            var task = client.Child("Books").OnceAsync<Book>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                var taskResult = task.Result;
                s_books = taskResult.Select(item => item.Object).ToList();
                return task.IsFaulted;
            }
            else return false;
        }
        private static bool LoadTags()
        {
            var task = client.Child("Tags").OnceSingleAsync<List<string>>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                s_tags = task.Result;
                return task.IsFaulted || (!task.IsCompleted);
            }
            else return false;
        }
        private static bool LoadUser()
        {
            client.Child("Users").Child(token.User.LocalId).PutAsync(new User
            {
                BookListID = new List<string>(),
                Score = 0
            }).Wait();
            var task = client.Child("Users").Child(token.User.LocalId).OnceSingleAsync<User>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                s_user = task.Result;
                return true;
            }
            else return false;
        }
        private static bool LoadSetting()
        {
            var task = client.Child("Setting").OnceSingleAsync<Setting>(new TimeSpan(0, 0, 2));
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                s_setting = task.Result;
                return true;
            }
            else return false;
        }
        public static async Task RefreshToken()
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
        public static bool Load(string username, string password)
        {
            if (client == null)
                client = SignIn(username, password);
            if (client == null)
                return false;
            var task0 = RefreshToken();
            var task1 = LoadBook();
            var task2 = LoadTags();
            var task3 = LoadUser();
            var task4 = LoadSetting();
            /*
            bool error, result;
            result = true;
            try { error = await task1; } catch { error = true; }
            if (error)
            {
                task1 = LoadBook();
                try { error = await task1; } catch { error = true; }
            }
            result &= error;
            try { error = await task2; } catch { error = true; }
            if (error)
            {
                task2 = LoadTags();
                try { error = await task2; } catch { error = true; }
            }
            result &= error;
            try { error = await task3; } catch { error = true; }
            if (error)
            {
                task3 = LoadUser();
                try { error = await task3; } catch { error = true; }
            }
            result &= error;
            try { error = await task4; } catch { error = true; }
            if (error)
            {
                task4 = LoadSetting();
                try { error = await task4; } catch { error = true; }
            }
            //*/
            return true;
        }
        #endregion

        #region CloseFirebase
        public static void Close()
        {
            client.Dispose();
            client = null;
            token = null;
        }
        #endregion
    }
}
