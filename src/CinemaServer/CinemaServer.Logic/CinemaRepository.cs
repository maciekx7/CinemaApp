using CinemaServer.FileReader;
using CinemaServer.Model;
using CinemaServer.Model.cinemadb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CinemaServer.DBConnector.DBConnect;
using System.Data;

namespace CinemaServer.Logic
{
    public class CinemaRepository : ICinemaRepository
    {
        public List<IQueryable<Screening>> GetScreenings(string date)
        {
            List<IQueryable<Screening>> list = new List<IQueryable<Screening>>();
            

            var cinemadb = new cinemadbContext();

            //var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            //var mysqlDate = DateTime.Parse(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            //Console.WriteLine($"DATE:{mysqlDate}");

            //var screenTime = new DateTime($"{date}");

            DateTime termin = DateTime.Now;
            if (date != null)
            {
                termin = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                if(termin == null)
                {
                    termin = DateTime.Now;
                }
            }

            var screenings = cinemadb.Screenings.Where(s => s.Date == termin);

            //screenings = cinemadb.Screenings.Include(item => item.Movie);//.Where(s => s.Date == mysqlDate);
            screenings = screenings.Include(item => item.Movie);

            var movies_id = screenings.Where(s => s.Movie.Id == s.MovieId).Select(x => x.MovieId).ToList();
            var x = movies_id.GroupBy(x => x)
                           .Select(grp => grp.Key)
                           .ToList();
            
            foreach (int id in x)
            {
                //Console.WriteLine($"MOVIE ID: {id}");
                var movies_list = screenings.Include(m => m.Movie).Where(s => s.Movie.Id == id);
                list.Add(movies_list);
            }

            return list;
        }


        public List<Movie> GetMovies()
        {
            using (var cinemadb = new cinemadbContext())
            {
                List<Movie> movies = new List<Movie>();
                var titles = cinemadb.Movies.Select(m => m.Title).Distinct().ToList();
                
                foreach (string title in titles)
                {
                    movies.Add(cinemadb.Movies.Where(m => m.Title == title).First());
                }
                
                return movies;
            }
            
        }

        List<Ticket> ICinemaRepository.GetTickets(string email)
        {
            using (var cinemadb = new cinemadbContext())
            {
                List<Ticket> tickets = new List<Ticket>();
                //var client = cinemadb.Clients.Select(c => c.Email == email);
                //var tickets = cinemadb.Tickets.Where(t => t.ClientId == client)


                var data = cinemadb.Tickets.Where(m => m.Client.Email == email).Include(l => l.Screening).Include(l => l.ReservationType).Include(l => l.SeatReserveds).Include(l => l.Screening.Movie).Include("SeatReserveds.Seat").ToList();
                List<Seat> Seats = new List<Seat>();
                

                foreach (Ticket ticket in data)
                {
                    tickets.Add(ticket);
                    //Seats.Add(ICinemaRepository.GetSeat(ticket.SeatReserveds.First().))
                }

                return tickets;
            }
        }

        Seat ICinemaRepository.GetSeat(int seatID)
        {
            using (var cinemadb = new cinemadbContext())
            {
                var seatData = cinemadb.Seats.Where(s => s.Id == seatID).First();
                return seatData;
            }
        }

        public bool isEmailUsed(string email)
        {
            using (var cinemadb = new cinemadbContext())
            {
                var count = cinemadb.Clients.Where(c => c.Email == email).Count();
                if(count > 0) { return true; }
                else { return false; }
            }
        }

        /*
        public string RegisterClient(string name, string lastName, string phone, string email, string password)
        {
            using (var cinemadb = new cinemadbContext())
            {
                try
                {
                    Client client = new Client();
                    client.Name = name;
                    client.Lastname = lastName;
                    client.Phone = phone;
                    client.Email = email;
                    client.Password = password;
                    cinemadb.Clients.Add(client);
                    cinemadb.SaveChanges();
                    return "SUCCESS";
                } catch(Exception e)
                {
                    Console.WriteLine(e);
                    return "FAIL";
                }

            }

        }
        */

        
        public string UpdateClient()
        {
            using (var cinemadb = new cinemadbContext())
            {
                try
                {
                    /*
                    Client client = new Client();
                    var beforeUpdateClient = cinemadb.Clients.Where(c => c.Email == email).First();
                    client.Name = name;
                    client.Lastname = lastName;
                    client.Phone = phone;
                    client.Email = email;
                    //client.Password = password;
                    client.Id = beforeUpdateClient.Id;

                    cinemadb.Entry(beforeUpdateClient).CurrentValues.SetValues(client);
                    */
                    cinemadb.SaveChanges();

                    return "SUCCESS";

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "FAIL";
                }

            }
        }
        
        
        public int getScreeningId(int movieId, string date, string time)
        {
            Console.WriteLine($"### ScreeningData:{movieId},{date},{time}");
            using (var cinemadb = new cinemadbContext())
            {
                try {
                    var dateFormat = "dd/MM/yyyy";
                    var scrId = cinemadb.Screenings.
                        Where(s1 => s1.MovieId == movieId).
                        Where(s2 => s2.Date == DateTime.ParseExact(date, dateFormat, null)).
                        Where(s3 => s3.Time == TimeSpan.Parse(time) ).
                        Select(s => s.Id).FirstOrDefault();

                    return scrId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            }
        }

        public List<Seat> getAvaliableSeats(int screeningID)
        {
            using (var cinemadb = new cinemadbContext())
            {
                try
                {
                    if(screeningID > 0 && this.isScreeningExisting(screeningID))
                    {
                        var all_seats = cinemadb.Seats;
                        var seats_reserved = cinemadb.SeatReserveds.Where(s => s.ScreeningId == screeningID);
                        var avaliable_seats = all_seats.Where(s => !seats_reserved.Select(sr => sr.SeatId).Contains(s.Id));

                        return avaliable_seats.ToList();
                    } else
                    {
                        return new List<Seat>();
                    }

                } catch(Exception e)
                {
                    Console.WriteLine(e);
                    return new List<Seat>();
                }
            }
        }

        public bool isScreeningExisting(int screeningId)
        {
            using (var cinemadb = new cinemadbContext())
            {
                var cout = cinemadb.Screenings.Where(s => s.Id == screeningId).Count();
                if(cout <= 0) { return false; }
                else {return true; }
            }

                
        }

        public string getClientId(string email)
        {
            using (var cinemadb = new cinemadbContext())
            {
                try
                {
                    var clientId = cinemadb.Clients.Where(c => c.Email == email).Select(c => c.Id).First();
                    return clientId;
                    //return int.Parse(clientId);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }

            }
        }

        public int BuyTicket(int screeningId, int reservationTypeId, string clientId, int seatId)
        {
            if(!this.isSeatInScreeningAvaliable(screeningId, seatId)) { return -1; }

            using (var cinemadb = new cinemadbContext())
            {
                using (var transaction = cinemadb.Database.BeginTransaction())
                {
                    try
                    {
                        #region create Ticket WITHOUT qrCode
                        Ticket ticket = new Ticket();
                        //ticket.ClientId = clientId;
                        ticket.ClientId = clientId.ToString();
                        ticket.ReservationTypeId = reservationTypeId;
                        ticket.ScreeningId = screeningId;
                        var screeningPrice = cinemadb.Screenings
                            .Where(s => s.Id == screeningId)
                            .Select(s => s.Price)
                            .First();
                        var ticketDiscount = cinemadb.ReservationTypes
                            .Where(r => r.Id == reservationTypeId)
                            .Select(s => s.Discount)
                            .First();

                        ticket.Price = (0.01 * (100 - ticketDiscount) * screeningPrice).ToString();

                        cinemadb.Tickets.Add(ticket);
                        cinemadb.SaveChanges();
                        #endregion


                        #region create seatReserved record
                        SeatReserved seatReserved = new SeatReserved();
                        
                        seatReserved.TicketId = ticket.Id;
                        seatReserved.SeatId = seatId;
                        seatReserved.ScreeningId = screeningId;

                        cinemadb.SeatReserveds.Add(seatReserved);
                        cinemadb.SaveChanges();
                        #endregion

                        transaction.Commit();

                        var screening = cinemadb.Screenings.
                            Where(s => s.Id == screeningId)
                            .Include(o => o.Movie)
                            .First();
                        var seat = cinemadb.Seats.
                            Where(s => s.Id == seatId).
                            First();
                        var email = cinemadb.Clients
                            //.Where(c => c.Id == clientId)
                            .Where(c => c.Id == clientId.ToString())
                            .Select(s => s.Email)
                            .First();
                        #region create QRCore and save to ticket
                        var movieType = "";
                        if (screening.Movie.Dubbing == false)
                        {
                            movieType = "Subtitles";
                        } else
                        {
                            movieType = "Dubbing";
                        }
                        //var qrData = Guid.NewGuid().ToString();
                        var qrData = $"Title:'{screening.Movie.Title}' " +
                            $"Type:{movieType} " +
                            $"Date:{screening.Date.ToString().Split(" ")[0].Replace('.','/')} " +
                            $"{screening.Time} " +
                            $"Seat:'{seat.SeatRow}{seat.SeatColumn}' " +
                            $"Nick:'{email}'";
                        QRCodeImageGenerator.saveQRToDirectory(qrData, $"{ticket.Id}.png");
                        ticket.QrCode = $"{ticket.Id}.png";


                        cinemadb.SaveChanges();
                        #endregion


                        return 1;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        return -1;
                    } 
                }
            } 
        }

        public bool isSeatInScreeningAvaliable(int screeningId, int seatId)
        {
            using (var cinemadb = new cinemadbContext())
            {
                var availability = cinemadb.SeatReserveds
                    .Where(s1 => s1.ScreeningId == screeningId)
                    .Where(s2 => s2.SeatId == seatId)
                    .Count();

                if(availability > 0) { return false; }
                else { return true; }
            }
        }

        public List<ReservationType> GetReservationTypes()
        {
            try
            {
                using (var cinemadb = new cinemadbContext())
                {
                    List<ReservationType> res = new List<ReservationType>();
                    var resTypes = cinemadb.ReservationTypes.ToList();

                    return resTypes;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<ReservationType>();
            }
        }

        public List<Occasion> getOccasions()
        {
            List<Occasion> occasions = new List<Occasion>();
            try
            {
                var filename = "json/occasions.json";
                var directory = FileReader.DirectoryPath.CONFIG_DIRECTORY(filename);
                var jsonFile = File.ReadAllText(directory);

                occasions = JsonSerializer.Deserialize<List<Occasion>>(jsonFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return occasions;
        }

        public Client GetClientInfo(string email)
        {
            try
            {
                using (var cinemadb = new cinemadbContext())
                {
                    var client = cinemadb.Clients.Where(c => c.Email == email).First();

                    if(client == null) { return null; }

                    return client;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Ticket> GetFilteredTickets(string sql)
        {
            List<Ticket> tickets = new List<Ticket>();
            try
            {
                using (var cinemadb = new cinemadbContext())
                {
                    DBConnectorAPI connector = DBConnectorAPI.GetInstance();
                    var data = connector.SELECT(sql);
                    foreach (DataRow row in data.Rows)
                    {
                        Ticket ticket = new Ticket();
                        ticket.Price = row["price"].ToString();
                        ticket.QrCode = row["qr_code"].ToString();


                        var seatReserved = cinemadb.SeatReserveds.Where(s => s.TicketId == int.Parse(row["id"].ToString())).First();
                        var seat = cinemadb.Seats.Find(seatReserved.SeatId);
                        SeatReserved sr = new SeatReserved();
                        Seat seat_ = new Seat();
                        ICollection<SeatReserved> srCollection = new List<SeatReserved>();
                        //var list = new List<SeatReserved>();
                        sr.SeatId = seat.Id;
                        seat_.SeatColumn = seat.SeatColumn;
                        seat_.SeatRow = seat.SeatRow;
                        seat_.Id = seat.Id;
                        sr.Seat = seat_;
                        
                        //sr.Seat.SeatColumn = seat.SeatColumn;
                        //sr.Seat.SeatRow = seat.SeatRow;

                        //list.Add(sr);
                        srCollection.Add(sr);
                        ticket.SeatReserveds = srCollection;
                        //var res_type = cinemadb.ReservationTypes.Where(s => s.Id == int.Parse(row["reservation_type_id"].ToString())).First();

                        var res_type = cinemadb.ReservationTypes.Find(int.Parse(row["reservation_type_id"].ToString()));
                        ticket.ReservationType = new ReservationType();
                        ticket.ReservationType.Id = res_type.Id;
                        ticket.ReservationType.Discount = res_type.Discount;
                        ticket.ReservationType.Description = res_type.Description;

                        var screening = cinemadb.Screenings.Where(s => s.Id == int.Parse(row["screening_id"].ToString())).Include(c => c.Movie).First();

                        ticket.Screening = new Screening();
                        ticket.Screening.Movie = new Movie();
                        ticket.Screening.Time = screening.Time;
                        ticket.Screening.Date = screening.Date;
                        ticket.Screening.Movie.Id = screening.MovieId;
                        ticket.Screening.Movie.Title = screening.Movie.Title;
                        ticket.Screening.Movie.Producer = screening.Movie.Producer;
                        ticket.Screening.Movie.Dubbing = screening.Movie.Dubbing;
                        ticket.Screening.Movie.Subtitles = screening.Movie.Subtitles;
                        ticket.Screening.Movie.ImageName = screening.Movie.ImageName;
                        ticket.Screening.Movie.Description = screening.Movie.Description;

                        var clientId = row["Client_id"].ToString();
                        ticket.ClientId = clientId;

                        tickets.Add(ticket);
                    }



                }
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return tickets;
        }

        public string getClientEmail(string clientID)
        {
            try
            {
                using (var cinemadb = new cinemadbContext())
                {
                    var clientEmail = cinemadb.Clients.Where(s => s.Id == clientID).Select(o => o.Email).First();
                    return clientEmail;
                }
            } catch(Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }

    
}
