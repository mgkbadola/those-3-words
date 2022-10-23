using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace those_3_words.Models
{
    //Summary
    //Base class for storing API response when calculating Suggestions From 3 Word Address
    public class SF3WA
    {
        public List<Suggestion> suggestions { get; set; }
    }
    public class Suggestion
    {
        public string country { get; set; }
        public string nearestPlace { get; set; }
        public string words { get; set; }
        public int rank { get; set; }
        public string language { get; set; }
    }
}