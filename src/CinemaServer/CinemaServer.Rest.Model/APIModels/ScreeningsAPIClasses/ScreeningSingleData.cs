using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses
{
    public class ScreeningSingleData : ScreeningBaseData
    {
        [Required]
        public MovieData Movie { get; set; }

        public ScreeningSingleData(MovieData movie, string date, string time) :base(date,time)
        {
            Movie = movie;
        }
    }
}
