using System;
namespace CinemaServer.Model
{
    public class Occasion
    {
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string discount { get; set; }
        public string price { get; set; }

        public Occasion()
        {
        }
    }
}
