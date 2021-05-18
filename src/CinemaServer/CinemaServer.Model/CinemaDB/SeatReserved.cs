using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class SeatReserved
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int ScreeningId { get; set; }
        public int TicketId { get; set; }

        public virtual Screening Screening { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
