using System;
namespace CinemaServer.Rest.Model.APIModels
{
    public class MovieData
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public string Producer { set; get; }
        public string Description { set; get; }
        public bool Subtitles { set; get; }
        public bool Dubbing { set; get; }
        public string ImageName { get; set; }
        public MovieData()
        {

        }

        public MovieData(int id, string title, string producer, string description) : this ()
        {
            Id = id;
            Title = title;
            Producer = producer;
            Description = description;
        }

        public MovieData(int id, string title, string producer, string description, bool subtitles, bool dubbing) : this(id, title, producer, description)
        {
            Subtitles = subtitles;
            Dubbing = dubbing;
        }

        public MovieData(int id, string title, string producer, string description, bool subtitles, bool dubbing, string imageName) : this(id, title, producer, description, subtitles, dubbing)
        {
            ImageName = imageName;
        }

    }
}
