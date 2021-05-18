using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class Screening
    {
        public Screening()
        {
            SeatReserveds = new HashSet<SeatReserved>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan Time { get; set; }
        public int MovieId { get; set; }
        public int Price { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual ICollection<SeatReserved> SeatReserveds { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
