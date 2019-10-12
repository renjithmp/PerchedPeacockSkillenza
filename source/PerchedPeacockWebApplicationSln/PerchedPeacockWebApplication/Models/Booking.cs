using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerchedPeacockWebApplication.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public bool IsOccupied { get; set; }
        public int ParkingLotId { get; set; }
    }
}
