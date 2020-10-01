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
        private const string SignUpFailedPrompt = 
            "Đã có lỗi xảy ra. Không đăng kí được.";
        private const string SignUpYesNoPromptContent = 
            "Không nhận ra bạn. Bạn có muốn đăng kí?";
        private const string SignUpPromptTitle = "Đăng kí?";
        private const string SignInFailedPrompt 
            = "Không thể đăng nhập. Xin thử lại sau ít phút.";
        #region FirebaseSetting
        // TODO: export to file
        static private string firebaseApiKey = "AIzaSyDu098TxwLgFJsfaenUPBfC1z9jyGGT2N8";
        static private string databaseURL = "https://fir-test-bd7d1.firebaseio.com";

        static private FirebaseAuthProvider authProvider;
        static private FirebaseClient client = null;
        static private FirebaseAuthLink token;
        #endregion

        private static List<Book> s_books;
        private static List<string> s_author;
        private static Setting s_setting;
        private static List<string> s_tags;
        private static User s_user;

        static public List<Book> Books => s_books;
        static public List<string> Tags => s_tags;
        static public User User => s_user;
        static public Setting Setting => s_setting;
        
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
            authActionSignIn.Wait();
            if (authActionSignIn.IsFaulted)
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
                        SignUp(email, password);
                        return SignIn(email, password);
                    }
                }
            }
            token = authActionSignIn.Result;
            return new FirebaseClient(databaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken)
                });
        }

        public static bool Load(string username, string password)
        {
            if (client == null)
                client = SignIn(username, password);
            if (client == null)
                return false;
            var task1 = client.Child("Books").OnceAsync<Book>();
            var task2 = client.Child("Tags").OnceAsync<string>();
            var task3 = client.Child("Users").Child(token.FirebaseToken).OnceAsync<Book>();
            var task4 = client.Child("Setting").OnceSingleAsync<Setting>();
            
            return true;
        }
        public static void Close()
        {
            
        }
    }
}
