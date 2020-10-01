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
        public ObservableCollection<string> BookListID { get; }
        // not username
        // not password
        public Int32 Score { get; set; }
        // Score measures overall behaviour & transgressions, including
        // fake book declaration, invalid tags, toxic comment, 
        // and others.
    }
}
