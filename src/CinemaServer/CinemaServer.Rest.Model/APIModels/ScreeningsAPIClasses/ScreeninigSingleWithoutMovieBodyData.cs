using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses
{
    public class ScreeninigSingleWithoutMovieBodyData : ScreeningBaseData
    {
        [Required]
        public int MovieId { get; set; }

        public ScreeninigSingleWithoutMovieBodyData() : base() { }

        public ScreeninigSingleWithoutMovieBodyData(int movieId, string date, string time) : base(date,time)
        {
            this.MovieId = movieId;
        }
    }
}
