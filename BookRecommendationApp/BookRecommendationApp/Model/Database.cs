using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        static public List<Book> Books
        {
            get => s_books;
            set => s_books = value;
        }
        static public List<string> Tags
        {
            get => s_tags;
            set => s_tags = value;
        }
        static public User User
        {
            get => s_user;
            set => s_user = value;
        }
        static public Setting Setting
        {
            get => s_setting;
            set => s_setting = value;
        }
        #endregion

        #region Functionality
        static public void Add(Book book)
        {
            try
            {
                if (book.IsValid())
                {
                    Firebase.Ins.Client.Child("Books").Child(book.Name).PutAsync(JsonConvert.SerializeObject(book)).Wait();
                    Books.Add(book);
                }
                else
                {
                    PostError("ERROR: book name is null \nAt Database::Add(book) with current Book: " +
                        JsonConvert.SerializeObject(book));
                }
            }
            catch (Exception e)
            {
                PostError(e);
            }
        }
        static public void Add(Picture pic)
        {
            try
            {
                if (pic.FilePath == null || pic.Content == null ||
                    pic.FilePath == "" || pic.Content == "")
                {
                    PostError("ERROR: invalid picture \nAt Database::Add(Picture) with current Picture: " +
                        JsonConvert.SerializeObject(pic));
                    return;
                }

                string FilePath = pic.FilePath.Replace(".", ",");
                Firebase.Ins.Client.Child("Picture").Child(FilePath).PutAsync(JsonConvert.SerializeObject(pic.Content)).Wait();
            }
            catch (Exception e)
            {
                PostError(e);
            }
        }
        static public void EditUser()
        {
            try
            {
                string uid = Firebase.Ins.Token.User.LocalId;
                if (uid != null || uid == "")
                    Firebase.Ins.Client.Child("Users").Child(uid).PutAsync(User);
                else
                {
                    PostError("ERROR: UID is null \nAt Database::EditUser() with current User: " + 
                        JsonConvert.SerializeObject(User));
                }
            }
            catch (Exception e)
            {
                PostError(e);
            }
        }
        static public void Add(string tag)
        {
            try
            {
                if (tag != null && tag != "")
                    Firebase.Ins.Client.Child("Tags").PostAsync(tag);
                Tags.Add(tag);
            }
            catch (Exception e)
            {
                PostError(e);
            }
        }
        static public string LoadPicture(string FilePath)
        {
            try
            {
                return Firebase.Ins.LoadPicture(FilePath);
            }
            catch (Exception e)
            {
                PostError(e);
                return null;
            }
        }
        static public void PostError(Exception e)
        {
            try
            {
                if (e != null)
                {
                    string uid = Firebase.Ins.Token.User.LocalId;
                    if (uid != null || uid == "")
                        Firebase.Ins.Client.Child("Error").Child(uid + DateTime.Now.ToString()).PutAsync(e.ToString());
                    else Console.WriteLine("ERROR: UID is null");
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("ERROR: cannot post error to database. Current error: " + 
                    e2.ToString() + "\nTarget Error: " + e.ToString());
            }
        }
        static public void PostError(string e)
        {
            try
            {
                if (e != null)
                {
                    string uid = Firebase.Ins.Token.User.LocalId;
                    if (uid != null || uid == "")
                        Firebase.Ins.Client.Child("Error").Child(uid + DateTime.Now.ToString()).PutAsync(e);
                    else Console.WriteLine("ERROR: UID is null");
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("ERROR: cannot post error to database. Current error: " +
                    e2.ToString() + "\nTarget Error: " + e);
            }
        }
        #endregion
    }
}
