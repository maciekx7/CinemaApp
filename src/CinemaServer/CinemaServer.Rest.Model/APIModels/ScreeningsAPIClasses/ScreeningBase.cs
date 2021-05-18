using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses
{
    public abstract class ScreeningBase
    {
        [Required]
        public string Date { get; set; }

        public ScreeningBase() {  }

        public ScreeningBase(string date)
        {
            Date = date;
        }
    }
}
