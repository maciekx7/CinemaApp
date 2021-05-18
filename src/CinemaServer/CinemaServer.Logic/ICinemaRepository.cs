using CinemaServer.Model;
using CinemaServer.Model.cinemadb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaServer.Logic
{
    public interface ICinemaRepository
    {
        public List<IQueryable<Screening>> GetScreenings(string date);

        public List<Movie> GetMovies();
        public List<Ticket> GetTickets(string email);

        //public string RegisterClient(string name, string lastName, string phone, string email, string password);

        public string UpdateClient();

        public List<Seat> getAvaliableSeats(int screeningID);

        public int BuyTicket(int screeningId, int reservationTypeId, string clientId, int seatId);

        public List<ReservationType> GetReservationTypes();
        //public string getClientId2(string email);

        #region pomocnicze
        public Seat GetSeat(int seatID);

        public bool isEmailUsed(string email);

        public bool isScreeningExisting(int screeningId);

        /// <summary>
        /// Get ID of scerening 
        /// </summary>
        /// <param name="movieId">Id of the movie</param>
        /// <param name="date">Date in format 'mm/dd/yyyy'</param>
        /// <param name="time">Time in format 'hh:mm'</param>
        /// <returns></returns>
        public int getScreeningId(int movieId, string date, string time);

        public string getClientId(string email);

        public string getClientEmail(string clientID);

        public bool isSeatInScreeningAvaliable(int screeningId, int seatId);

        public Client GetClientInfo(string email);

        public List<Ticket> GetFilteredTickets(string sql);


        #endregion



        #region OCCASIONS
        public List<Occasion> getOccasions();
        #endregion
    }
}
