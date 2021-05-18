using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CinemaServer.Rest.Model.APIModels;
using CinemaServer.Model.cinemadb;
using CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;

namespace CinemaServer.Rest.Model.Converters
{
    public static class DataConverter
    {
        public static ScreeningData ConvertScreeningToScreeningData(IQueryable<Screening> screenings)
        {
            ScreeningData model = new ScreeningData();

            if (screenings.Count() != 0)
            {
                model.Times = new List<string>();
                Screening scr = screenings.AsQueryable().First();
                foreach (Screening screening in screenings)
                {

                    if (model.Movie == null)
                    {
                        model.Movie = new MovieData(
                            screening.MovieId,
                            screening.Movie.Title,
                            screening.Movie.Producer,
                            screening.Movie.Description,
                            screening.Movie.Subtitles,
                            screening.Movie.Dubbing,
                            screening.Movie.ImageName);
                    }
                    model.Times.Add(screening.Time.ToString());
                    model.Price = screening.Price;
                }
                model.Date = scr.Date.ToString().Split(" ")[0].Replace('.', '/');
                List<string> hours = new List<string>();
                foreach (string time in model.Times)
                {
                    var trash = time.Split(new[] { ':' }, 3);
                    hours.Add(trash.ElementAt(0) + ":" + trash.ElementAt(1));
                    
                }
                model.Times = hours;

                model.Times = model.Times.OrderBy(o => o).ToList();


                
                
            }

            return model;
        }

        public static ScreeningData ConvertOneToScreeningData(this Screening screening)
        {
            //TODO
            return null;
        }

        public static MovieData ConvertMovieToMovieData(this Movie movie)
        {
            MovieData Movie = new MovieData(10, movie.Title, movie.Producer, movie.Description);
            return Movie;
        }

        public static SeatData ConvertSeatToSeatData(this Seat seat)
        {
            SeatData seatData = new SeatData(seat.Id, seat.SeatRow, seat.SeatColumn);
            return seatData;
        }

        public static ReservationTypeData ConvertReservationTypeToData(this ReservationType reservation)
        {
            ReservationTypeData resType = new ReservationTypeData(
                reservation.Id,
                reservation.Description,
                reservation.Discount);
            return resType;
        }

        


    }
}
