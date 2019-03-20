using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot
{
    public class flightBookingModel
    {
        public string PNR { get; set; }
        public FlightModel flight { get; set; }
        public int Passengers { get; set; }

        public int MyProperty { get; set; }

        public string status { get; set; }

    }
}
