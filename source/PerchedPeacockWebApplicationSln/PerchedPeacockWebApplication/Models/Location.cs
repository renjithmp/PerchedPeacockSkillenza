using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerchedPeacockWebApplication.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationBuilding { get; set; }

        public string LocationLocality { get; set; }

        public string LocationCity { get; set; }

        public int LocationPinCode { get; set; }
    }
}
