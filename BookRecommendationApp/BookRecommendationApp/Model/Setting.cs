using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendationApp.Model
{
    public partial class Setting
    {
        public Int32 LikeCoefficient { get; set; }
        public Int32 ViewCoefficient { get; set; }
        public Int32 AddToListCoefficient { get; set; }
    }
}
