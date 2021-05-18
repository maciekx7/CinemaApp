using System;
using System.Collections.Generic;
using System.Text;
using CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses;
namespace CinemaServer.Rest.Model.APIModels
{
    public class TicketData
    {
        public string Price { get; set; }
        public ScreeningSingleData Screening { get; set; }
        public ReservationTypeData ReservationType { get; set; }
        public SeatData Seat { get; set; }
        public string QRCode { get; set; }
        public string Email { get; set; }
        public TicketData(string price, ScreeningSingleData screening, ReservationTypeData reservationType, SeatData seat, string qrCode)
        {
            Price = price;
            Screening = screening;
            ReservationType = reservationType;
            Seat = seat;
            QRCode = qrCode;
        }

        public TicketData()
        {
        }
    }
}
