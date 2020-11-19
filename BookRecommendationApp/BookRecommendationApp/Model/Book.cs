using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendationApp.Model
{
    public partial class Book
    {
        public long GetScore()
        {
            return Database.Setting.AddToListCoefficient * AddToList
                    + Database.Setting.LikeCoefficient * Likes
                    + Database.Setting.ViewCoefficient * Views
                    + InitialScore;
        }
        // Score measures how often the book gets seen, put in 
        // read lists, and likes. Score starts out equal to the
        // score of the user who posted it.
        public Int32 Likes { get; set; }
        public Int32 Views { get; set; }
        public Int32 AddToList { get; set; }
        public Int32 InitialScore { get; set; }

        public string Author { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureFile { get; set; }

        public string Link { get; set; }
        public List<string> Tags { get; set; }
    }
}
