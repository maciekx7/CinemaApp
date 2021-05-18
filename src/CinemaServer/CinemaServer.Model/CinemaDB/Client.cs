using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class Client : IdentityUser
    {
        public Client()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Client(string name, string lastName, string phone) : this()
        {
            Name = name;
            Lastname = lastName;
            Phone = phone;
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
