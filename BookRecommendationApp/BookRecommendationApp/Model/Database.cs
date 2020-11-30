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
            if (book.IsValid())
            {
                Firebase.Ins.Client.Child("Books").Child(book.Name).PutAsync(JsonConvert.SerializeObject(book)).Wait();
                Books.Add(book);
            }
            else Console.WriteLine("ERROR: book name is null");
        }
        static public void Add(Picture pic)
        {
            if (pic.FilePath == null || pic.Content == null)
            {
                Console.WriteLine("ERROR: invalid picture");
                return;
            }

            string FilePath = pic.FilePath.Replace(".", ",");
            Firebase.Ins.Client.Child("Picture").Child(FilePath).PutAsync(JsonConvert.SerializeObject(pic.Content)).Wait();
        }
        static public void EditUser()
        {
            string uid = Firebase.Ins.Token.User.LocalId;
            if (uid != null)
                Firebase.Ins.Client.Child("Users").Child(uid).PutAsync(User);
            else Console.WriteLine("ERROR: UID is null");
        }
        static public void Add(string tag)
        {
            Firebase.Ins.Client.Child("Tags").PostAsync(tag);
            Tags.Add(tag);
        }
        static public string LoadPicture(string FilePath)
        {
            return Firebase.Ins.LoadPicture(FilePath);
        }
        #endregion
    }
}
