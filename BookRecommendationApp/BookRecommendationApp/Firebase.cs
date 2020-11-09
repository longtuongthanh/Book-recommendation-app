using BookRecommendationApp.Model;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendationApp
{
    public class Firebase
    {
        #region Singleton
        public static Firebase Ins => _ins;
        private static readonly Firebase _ins = new Firebase();
        private Firebase() { }
        #endregion

        #region Constant
        private const int TimeOut = 10000;
        #endregion

        #region FirebaseSetting
        // TODO: export to file
        // TODO: encrypt said file
        private const string firebaseApiKey = "AIzaSyDu098TxwLgFJsfaenUPBfC1z9jyGGT2N8";
        private const string databaseURL = "https://fir-test-bd7d1.firebaseio.com";
        private FirebaseAuthProvider authProvider =
            new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
        private FirebaseClient client = null;
        private FirebaseAuthLink token = null;
        private Task refreshToken;

        public FirebaseAuthLink Token
        {
            get => token;
            set
            {
                token = value;
                Client = new FirebaseClient(databaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(Token.FirebaseToken)
                });
            }
        }

        public FirebaseClient Client { get => client; set => client = value; }
        #endregion

        #region LoadData
        public bool SignUp(string email, string password)
        {
            var authActionSignUp = authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            bool error = false;
            try { authActionSignUp.Wait(TimeOut); }
            catch { error = true; }
            if (authActionSignUp.IsFaulted || error || !authActionSignUp.IsCompleted)
            {
                //MessageBox.Show(SignUpFailedPrompt);
                return false;
            }
            Token = authActionSignUp.Result;

            Client.Child("Users").Child(Token.User.LocalId).PutAsync(new Model.User
            {
                BookListID = new List<string>(),
                Score = 0
            }).Wait();
            return true;
        }

        public bool SignIn(string email, string password)
        {
            var authActionSignIn = authProvider.SignInWithEmailAndPasswordAsync(email, password);
            bool error = false;
            try { authActionSignIn.Wait(TimeOut); }
            catch { error = true; }
            if (authActionSignIn.IsFaulted || error || !authActionSignIn.IsCompleted)
            {
                return false;
                /*
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
                //*/
            }
            Token = authActionSignIn.Result;
            return true;
        }

        #region Tasks
        private bool LoadBook()
        {
            var task = Client.Child("Books").OnceAsync<Book>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                var taskResult = task.Result;
                Database.Books = taskResult.Select(item => item.Object).ToList();
                return !task.IsFaulted;
            }
            else return false;
        }
        private bool LoadTags()
        {
            var task = Client.Child("Tags").OnceSingleAsync<List<string>>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.Tags = task.Result;
                return !task.IsFaulted;
            }
            else return false;
        }
        private bool LoadUser()
        {
            var task = Client.Child("Users").Child(Token.User.LocalId).OnceSingleAsync<Model.User>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.User = task.Result;
                return !task.IsFaulted;
            }
            else return false;
        }
        private bool LoadSetting()
        {
            var task = Client.Child("Setting").OnceSingleAsync<Setting>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return false; }
                Database.Setting = task.Result;
                return !task.IsFaulted;
            }
            else return false;
        }
        // Returns Picture.Content
        public string LoadPicture(string FilePath)
        {
            // Firebase doesn't accept '.'
            if (FilePath == null) return null;
            FilePath = FilePath.Replace(".", ",");

            var task = Client.Child("Picture").Child(FilePath).OnceSingleAsync<string>();
            var taskEnd = Task.WhenAny(task, Task.Delay(TimeOut));
            taskEnd.Wait();
            if (taskEnd.Result == task)
            {
                try { task.Wait(); } catch { return null; }
                if (task.IsFaulted) return null;
                return task.Result;
            }
            else return null;
        }
        #endregion

        #region Refresh Token
        public async Task RefreshToken()
        {
            while (true)
            {
                var task = Token.GetFreshAuthAsync();
                try { task.Wait(); } catch { continue; }
                Token = task.Result;
                await Task.Delay(Token.ExpiresIn);
            }
        }
        #endregion

        public bool LoadFirebase(string username = null, string password = null)
        {
            bool result = true;
            if (Client == null)
                result = SignIn(username, password);
            if ((Client == null) || (result == false))
                return false;

            refreshToken = RefreshToken();
            result &= LoadBook();
            result &= LoadTags();
            result &= LoadUser();
            result &= LoadSetting();
            /*
            if (result == false)
                MessageBox.Show(LoadDataFromFirebaseFailed);
            //*/
            return result;
        }
        #endregion

        #region CloseFirebase
        public void CloseFirebase()
        {
            Client.Dispose();
            refreshToken.Dispose();
            Client = null;
            Token = null;
            refreshToken = null;
        }
        #endregion
    }
}
