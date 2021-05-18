using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class ReservationType
    {
        public ReservationType()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
