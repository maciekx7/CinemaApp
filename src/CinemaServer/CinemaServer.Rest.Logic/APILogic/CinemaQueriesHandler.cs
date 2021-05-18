using CinemaServer.Model.cinemadb;
using CinemaServer.Rest.Model.APIModels;
using CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses;
using System;
using System.Collections.Generic;
using System.Text;
using CinemaServer.Rest.Model.Converters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CinemaServer.Logic;
using CinemaServer.Model;
using System.IO;
using CinemaServer.FileReader;

namespace CinemaServer.Rest.Logic.APILogic
{
    public class CinemaQueriesHandler : ICinemaQueriesHandler
    {
        private readonly ICinemaRepository cinemaRepository;
        public CinemaQueriesHandler(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        

        public List<ScreeningData> GetScreenings(string date)
        {
            //var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            //var mysqlDate = DateTime.Parse(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            //Console.WriteLine($"DATE:{mysqlDate}");

            //var screenTime = new DateTime($"{date}");

            List<ScreeningData> list = new List<ScreeningData>();
            List<IQueryable<Screening>> ScreeningList = cinemaRepository.GetScreenings(date);
            foreach(IQueryable<Screening> movies_list in ScreeningList)
            {
                
                list.Add(DataConverter.ConvertScreeningToScreeningData(movies_list));
            }
            //list.OrderBy(o => o.Times).ToList();

            return list;
        }

        public List<MovieData> GetMovies()
        {
            List<MovieData> list = new List<MovieData>();
            List<Movie> movies = cinemaRepository.GetMovies();
            foreach(Movie movie in movies)
            {
                list.Add(movie.ConvertMovieToMovieData());
            }
            return list;
        }

        public (byte[],string) GetImage(string file)
        {
            try
            {
                Byte[] b = System.IO.File.ReadAllBytes(DirectoryPath.IMAGES_DIRECTORY(file));
                return (b, $"image/{EImage.FileExtension(file)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (new byte[0], "");
            }
        }

        public List<TicketData> GetTickets(string email)
        {
            List<TicketData> list = new List<TicketData>();
            List<Ticket> tickets = cinemaRepository.GetTickets(email);

            if(tickets.Count() == 0)
            {
                Console.WriteLine("DOSTALES NULL");
                return list;
            }
            
            foreach(Ticket ticket in tickets)
            {
                MovieData movie = new MovieData(ticket.Screening.Movie.Id,
                    ticket.Screening.Movie.Title,
                    ticket.Screening.Movie.Producer,
                    ticket.Screening.Movie.Description,
                    ticket.Screening.Movie.Subtitles,
                    ticket.Screening.Movie.Dubbing,
                    ticket.Screening.Movie.ImageName);
                ScreeningSingleData screening = new ScreeningSingleData(movie, ticket.Screening.Date.ToString().ToString().Split(" ")[0], ticket.Screening.Time.ToString());
                ReservationTypeData reservationTypeData = new ReservationTypeData(ticket.ReservationType.Id, ticket.ReservationType.Description, ticket.ReservationType.Discount);
                string QRCode = ticket.Id + ".png";
                SeatData seat = new SeatData(ticket.SeatReserveds.First().Seat.Id, ticket.SeatReserveds.First().Seat.SeatRow, ticket.SeatReserveds.First().Seat.SeatColumn);
                TicketData ticketData = new TicketData(ticket.Price, screening, reservationTypeData, seat, QRCode);
                ticketData.Email = email;
                list.Add(ticketData);
            }
            return list;
        }

        public List<TicketData> GetFilteredTickets(string sql)
        {
            List<TicketData> tickets = new List<TicketData>();
            try
            {
                var ticket_rep = cinemaRepository.GetFilteredTickets(sql);

                foreach (Ticket ticket in ticket_rep)
                {
                    MovieData movie = new MovieData(ticket.Screening.Movie.Id,
                    ticket.Screening.Movie.Title,
                    ticket.Screening.Movie.Producer,
                    ticket.Screening.Movie.Description,
                    ticket.Screening.Movie.Subtitles,
                    ticket.Screening.Movie.Dubbing,
                    ticket.Screening.Movie.ImageName);
                    ScreeningSingleData screening = new ScreeningSingleData(movie, ticket.Screening.Date.ToString().ToString().Split(" ")[0], ticket.Screening.Time.ToString());
                    ReservationTypeData reservationTypeData = new ReservationTypeData(ticket.ReservationType.Id, ticket.ReservationType.Description, ticket.ReservationType.Discount);
                    
                    SeatData seat = new SeatData(ticket.SeatReserveds.First().Seat.Id, ticket.SeatReserveds.First().Seat.SeatRow, ticket.SeatReserveds.First().Seat.SeatColumn);
                    //SeatData seat = new SeatData();
                    TicketData ticketData = new TicketData(ticket.Price, screening, reservationTypeData, seat, ticket.QrCode);
                    ticketData.Email = cinemaRepository.getClientEmail(ticket.ClientId);

                    tickets.Add(ticketData);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return tickets;

        }

        /*
        public string RegisterClient(ClientRegisterData clientData)
        {
            List<string> test = new List<string>();
            string status = cinemaRepository.RegisterClient(
                clientData.Name,
                clientData.Lastname,
                clientData.Phone,
                clientData.Email,
                clientData.Password);
            ///FIXME usunąć zwracanie TEST. Powinniśmy zwrócić nic?
            ///No ogólnie nie trzeba nic w API zwracać
            test.Add(clientData.Name);
            test.Add(clientData.Lastname);
            test.Add(clientData.Phone);
            test.Add(clientData.Email);
            test.Add(clientData.Password);
            return status;
        }
        */


        public string UpdateClient()
        {
            List<string> test = new List<string>();
            string status = cinemaRepository.UpdateClient();
            ///FIXME usunąć zwracanie test. Powinniśmy zwrócić nic?
            ///No ogólnie nie trzeba nic w API zwracać
            /*
            test.Add(clientData.Name);
            test.Add(clientData.Lastname);
            test.Add(clientData.Phone);
            test.Add(clientData.Email);
            test.Add(clientData.Password);
            */
            return "";
        }
        

        public List<SeatData> GetAvaliableSeats(ScreeninigSingleWithoutMovieBodyData screeningData)
        {
            
            List<SeatData> seats = new List<SeatData>();
            var screeningId = cinemaRepository.getScreeningId(
                screeningData.MovieId,
                screeningData.Date,
                screeningData.Time);
            var seatsList = cinemaRepository.getAvaliableSeats(screeningId).ToList();

            foreach(Seat seat in seatsList)
            {
                seats.Add(seat.ConvertSeatToSeatData());
            }

            return seats;
        }

        public int BuyTicket(BuyTicketData ticketData, string email)
        {
            var screeningID = cinemaRepository.getScreeningId(
                ticketData.MovieId,
                ticketData.Date,
                ticketData.Time
                );

            var clientId = cinemaRepository.getClientId(
                email
                );

            var status = cinemaRepository.BuyTicket(
                screeningID,
                ticketData.ReservationTypeId,
                clientId,
                ticketData.SeatId
                );

            return status;
        }

        public (byte[], string) GetQr(string file)
        {
            try
            {
                Byte[] b = System.IO.File.ReadAllBytes(DirectoryPath.QRCODE_DIRECTORY(file));
                return (b, $"image/{EImage.FileExtension(file)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (new byte[0], "");
            }
        }

        public (byte[], string) getFile(string file, EFileType fileType)
        {
            try
            {
                byte[] b = new byte[0];
                switch (fileType)
                {
                    case EFileType.QR:
                        b = System.IO.File.ReadAllBytes(DirectoryPath.QRCODE_DIRECTORY(file));
                        break;
                    case EFileType.Image:
                        b = System.IO.File.ReadAllBytes(DirectoryPath.IMAGES_DIRECTORY(file));
                        break;
                    case EFileType.Occasion:
                        b = System.IO.File.ReadAllBytes(DirectoryPath.OCCASION_DIRECTORY(file));
                        break;
                }
                return (b, $"image/{EImage.FileExtension(file)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (new byte[0], "");
            }
        }


        public List<ReservationTypeData> GetReservationTypes()
        {
            try
            {
                List<ReservationTypeData> convertedData = new List<ReservationTypeData>();
                List<ReservationType> DBdata = cinemaRepository.GetReservationTypes();
                foreach (ReservationType res in DBdata)
                {
                    convertedData.Add(res.ConvertReservationTypeToData());
                }
                return convertedData;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<ReservationTypeData>();
            }
        }

        public List<Occasion> GetOccasions()
        {
            List<Occasion> occasions = new List<Occasion>();
            try
            {
                occasions = cinemaRepository.getOccasions();
            } catch(Exception e)
            {
                Console.WriteLine(occasions);
            }

            return occasions;
        }

        public ClientData GetClientInfo(string email)
        {
            ClientData clientData = new ClientData();
            try
            {
                var result = cinemaRepository.GetClientInfo(email);
                clientData.Name = result.Name;
                clientData.Lastname = result.Lastname;
                clientData.Phone = result.Phone;
                clientData.Email = result.Email;
                clientData.Password = "";

                if (result == null) { return null; }

                return clientData;
            } catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        

        
    }
}
