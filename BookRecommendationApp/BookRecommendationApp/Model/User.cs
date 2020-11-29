using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendationApp.Model
{
    public partial class User
    {
        #region Functionality
        public User() { BookListID = new List<string>(); }

        [JsonIgnore]
        private Picture picture;
        public Picture GetPicture()
        {
            if (picture != null)
                return picture;

            Picture pic = new Picture(PictureFile);
            if (pic.GetImage() == null)
            {
                // Get image from database
                pic.Content = Database.LoadPicture(pic.FilePath);

                // save image to file
                pic.SaveImage();
            }
            return picture = pic;
        }

        public void AddBook(Book book)
        {
            if (!BookListID.Contains(book.Name))
            {
                BookListID.Add(book.Name);
                Database.EditUser();
            }
        }
        public void RemoveBook(Book book)
        {
            if (BookListID.Contains(book.Name))
            {
                BookListID.Remove(book.Name);
                Database.EditUser();
            }
        }
        #endregion

        #region Data
        public List<string> BookListID { get; set; }
        public List<string> LikeListID { get; set; }
        // not username
        // not password
        public Int32 Score { get; set; }
        // Score measures overall behaviour & transgressions, including
        // fake book declaration, invalid tags, toxic comment, 
        // and others.
        public string PictureFile { get; set; }
        #endregion
    }
}
