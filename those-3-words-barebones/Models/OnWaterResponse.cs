using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace those_3_words.Models
{
    public class OnWaterResponse
    {
        public string query { get; set; }
        public string request_id { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public bool water { get; set; }
    }
}