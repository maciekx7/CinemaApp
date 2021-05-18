using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class Movie
    {
        public Movie()
        {
            Screenings = new HashSet<Screening>();
        }

        public int Id { get; set; }
        public bool Dubbing { get; set; }
        public bool Subtitles { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Screening> Screenings { get; set; }
    }
}
