using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot
{
    public class HotelBookingModel
    {
        public string HotelNumber { get; set; }
        public HotelModel hotel { get; set; }
        public decimal duration { get; set; }
        public int Person { get; set; }

    }
}
