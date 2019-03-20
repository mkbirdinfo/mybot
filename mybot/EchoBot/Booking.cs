using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot
{
    public class Booking
    {

        public static string GiveFeedBack(JObject entities)
        {
            string result=string.Empty;

           // string PNR = entities.GetValue("PNR") != null ? entities.GetValue("City").FirstOrDefault().ToString() : "x";

            if(entities.GetValue("PNR") != null)
            {
                string PNR = entities.GetValue("PNR").FirstOrDefault().ToString();

                result = GiveStatus(PNR);

            }

            if (entities.GetValue("Hotelnumber") != null)
            {
                result = GiveHotelStatus(entities.GetValue("Hotelnumber").FirstOrDefault().ToString());

            }




            return result;
        }


        public static string GiveStatus(string PNR)
        {
            string DataResult = string.Empty;
            List<flightBookingModel> PNRStatus = new List<flightBookingModel>
            {
                new flightBookingModel
                {
                    PNR="AIR1235",
                     flight= new FlightModel{Location="Mumbai",FlightType="Indigo",City="Delhi",Storeddate=Convert.ToDateTime("21/03/2019")},
                     Passengers=4,
                     status="cancled"
                     
                },
               new flightBookingModel
                {
                    PNR="Indigo1235",
                     flight=  new FlightModel{Location="Mumbai",FlightType="Jet",City="Delhi",Storeddate=Convert.ToDateTime("17/04/2019")},
                     Passengers=3,
                     status="about to go"

                },
                new flightBookingModel
                {
                    PNR="Jet1234",
                     flight=  new FlightModel{Location="Katar",FlightType="Jet",City="Delhi",Storeddate=Convert.ToDateTime("17/04/2019")},
                     Passengers=2,
                     status="In transit"

                }

            };

            var result = from s in PNRStatus
                         where s.PNR.ToLower() == PNR.ToLower()
                         select s;

            if (result.Count() >= 1)
            {
                foreach(var r in result)
                {
                    DataResult += "details :\n" + "\nPNR => " + r.PNR + "\nFlight : " + r.flight.FlightType + "\nFrom => " + r.flight.Location + "\nTo => " + r.flight.City + "\nDate => " + r.flight.Storeddate + "PassengerCount => " + r.Passengers + "\nStatus => " + r.status;
                }
             
            }
            else
            {
                DataResult += "Sorry wrong PNR Number";
            }

            return DataResult;

        }



        //--------------------------------------
        public static string GiveHotelStatus(string HotelNumber)
        {
            string DataResult = string.Empty;
            List<HotelBookingModel> HotelStatus = new List<HotelBookingModel>
            {
                new HotelBookingModel
                {
                   HotelNumber="KENt123",
                     hotel=  new HotelModel{Cost=6754,Hotel="Raj",City="Delhi",StoredDate=Convert.ToDateTime("18/04/2019")},
                     Person=2,
                     duration=6

                },
               new HotelBookingModel
                {
                    HotelNumber="FGT123",
                     hotel=  new HotelModel{Cost=9999,Hotel="Leela",City="Mumbai",StoredDate=Convert.ToDateTime("17/06/2019")},
                     Person=5,
                     duration=12

                },
                new HotelBookingModel
                {
                HotelNumber="Taj154",
                     hotel=  new HotelModel{Cost=5998,Hotel="Hindu",City="dehradun",StoredDate=Convert.ToDateTime("30/03/2019")},
                     Person=3,
                     duration=24
                }

            };

            var result = from s in HotelStatus
                         where s.HotelNumber.ToLower() == HotelNumber.ToLower()
                         select s;

            if (result.Count() >= 1)
            {
                foreach (var r in result)
                {
                    DataResult += "details :\n" + "\nHotelNumber => " + r.HotelNumber + "\nHotel : " + r.hotel.Hotel + "\nCity => " + r.hotel.City + "\nDate => " + r.hotel.StoredDate+ "\nCost => " + r.hotel.Cost + "\nPersons => " + r.Person + "\nDuration => " + r.duration;
                }

            }
            else
            {
                DataResult += "Sorry wrong Hotel Number";
            }

            return DataResult;

        }



    }
}
