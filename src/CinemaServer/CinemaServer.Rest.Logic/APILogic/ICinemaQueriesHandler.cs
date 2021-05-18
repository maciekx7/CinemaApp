using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CinemaServer.Model;
using CinemaServer.Model.cinemadb;
using CinemaServer.Rest.Model.APIModels;
using CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses;

namespace CinemaServer.Rest.Logic.APILogic
{
    public interface ICinemaQueriesHandler
    {
        List<ScreeningData> GetScreenings(string date);

        List<MovieData> GetMovies();

        List<TicketData> GetTickets(string email);

        (byte[], string) GetImage(string file);

        (byte[], string) GetQr(string file);

        (byte[], string) getFile(string file, EFileType fileType);

        //string RegisterClient(ClientRegisterData clientData);

        string UpdateClient();

        public List<SeatData> GetAvaliableSeats(ScreeninigSingleWithoutMovieBodyData screeningData);

        public int BuyTicket(BuyTicketData ticketData, string email);

        public List<ReservationTypeData> GetReservationTypes();

        public List<Occasion> GetOccasions();

        public ClientData GetClientInfo(string email);

        List<TicketData> GetFilteredTickets(string sql);
    }
}
