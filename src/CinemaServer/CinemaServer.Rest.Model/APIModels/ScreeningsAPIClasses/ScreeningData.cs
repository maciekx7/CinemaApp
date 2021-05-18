using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CinemaServer.Model.cinemadb;

namespace CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses
{
    public class ScreeningData : ScreeningBase
    {
        public List<string> Times { get; set; }

        [Required]
        public MovieData Movie { get; set; }

        public int Price { get; set; }

        public ScreeningData() : base() { } 

        public ScreeningData(MovieData movie, string date, List<string> times) :base(date)
        {
            Movie = movie;
            Date = date;
            Times = times;
        }
    }
}
