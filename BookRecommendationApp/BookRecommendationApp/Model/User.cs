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

        public List<string> BookListID { get; set; }
        public List<string> LikeListID { get; set; }
        // not username
        // not password
        public Int32 Score { get; set; }
        // Score measures overall behaviour & transgressions, including
        // fake book declaration, invalid tags, toxic comment, 
        // and others.
        public string PictureFile { get; set; }

        public User() { BookListID = new List<string>(); }
    }
}
