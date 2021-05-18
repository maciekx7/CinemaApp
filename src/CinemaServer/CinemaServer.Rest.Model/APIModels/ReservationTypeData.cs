using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaServer.Rest.Model.APIModels
{
    public class ReservationTypeData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }

        public ReservationTypeData(int id, string description, int discount)
        {
            Id = id;
            Description = description;
            Discount = discount;
        }
    }
}
