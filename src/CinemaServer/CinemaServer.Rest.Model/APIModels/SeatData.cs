using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaServer.Rest.Model.APIModels
{
    public class SeatData
    {
        public int Id { get; set; }
        public string SeatRow { get; set; }
        public int SeatColumn { get; set; }

        public SeatData(int id, string seatRow, int seatColumn)
        {
            Id = id;
            SeatRow = seatRow;
            SeatColumn = seatColumn;
        }

        public SeatData()
        {
        }
    }
}
