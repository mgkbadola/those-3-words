namespace those_3_words.Models
{
    public class Coordinates
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }
    //Summary
    //Base class for storing API response when calculating Coordinates From 3 Word Address
    public class CF3WA
    {
        public string country { get; set; }
        public Square square { get; set; }
        public string nearestPlace { get; set; }
        public Coordinates coordinates { get; set; }
        public string words { get; set; }
        public string language { get; set; }
        public string map { get; set; }
    }

    public class Square
    {
        public Coordinates southwest { get; set; }
        public Coordinates northeast { get; set; }
    }
}