using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses
{
    public abstract class ScreeningBaseData : ScreeningBase
    {

        [Required]
        public string Time { get; set; }

        public ScreeningBaseData() : base() { }

        public ScreeningBaseData(string date, string time) : base(date)
        {
            this.Time = time;
        }
    }
}
