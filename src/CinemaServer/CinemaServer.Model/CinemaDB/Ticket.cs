using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class Ticket
    {
        public Ticket()
        {
            SeatReserveds = new HashSet<SeatReserved>();
        }

        public int Id { get; set; }
        public string Price { get; set; }
        public int ScreeningId { get; set; }
        public int ReservationTypeId { get; set; }
        //public int ClientId { get; set; }
        public string ClientId { get; set; }
        public string QrCode { get; set; }

        public virtual Client Client { get; set; }
        public virtual ReservationType ReservationType { get; set; }
        public virtual Screening Screening { get; set; }
        public virtual ICollection<SeatReserved> SeatReserveds { get; set; }
    }
}
