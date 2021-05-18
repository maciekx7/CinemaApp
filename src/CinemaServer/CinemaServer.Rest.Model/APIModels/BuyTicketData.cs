using System;
namespace CinemaServer.Rest.Model.APIModels
{
    public class BuyTicketData
    {
        //TODO I don't know if it's enough (BuyTicketData atributes)
        public int MovieId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int SeatId { get; set; }
        public int ReservationTypeId { get; set; }

        public BuyTicketData()
        {
        }
    }
}
