using Firebase.Auth;
using Firebase.Database;
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
    }
}
