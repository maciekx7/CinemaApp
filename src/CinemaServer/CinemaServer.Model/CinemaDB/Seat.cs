using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class Seat
    {
        public Seat()
        {
            SeatReserveds = new HashSet<SeatReserved>();
        }

        public int Id { get; set; }
        public string SeatRow { get; set; }
        public int SeatColumn { get; set; }

        public virtual ICollection<SeatReserved> SeatReserveds { get; set; }
    }
}
