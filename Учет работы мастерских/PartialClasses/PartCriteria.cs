using System.Collections.Generic;

namespace Учет_работы_мастерских
{
    partial class criteria
    {
        private int rating;

        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }
        public List<int> ratingList { get; set; } = new List<int>();



    }
}
