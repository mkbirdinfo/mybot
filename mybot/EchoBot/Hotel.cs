using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace EchoBot
{
    public class Hotel
    {
        public static string Hotelsearch(JObject entities)
        {
            


            string loc1 = entities.GetValue("City") != null ? entities.GetValue("City").FirstOrDefault().ToString() : "x";
            var hotal = "x";
            if (entities.GetValue("hotel") != null)
            {
                if (entities.GetValue("hotel").Count() > 1)
                {
                    hotal = entities.GetValue("hotel").FirstOrDefault().ToString() + " " + entities.GetValue("hotel").LastOrDefault().ToString();
                }
                else
                {
                    hotal = entities.GetValue("hotel").FirstOrDefault().ToString();
                }
            }
            var cost = entities.GetValue("number") != null ? entities.GetValue("number").FirstOrDefault() : "1";
            dynamic date = entities.GetValue("datetime") != null ? entities.GetValue("datetime").FirstOrDefault() : "x";




            List<HotelModel> HotelData = new List<HotelModel>
            {
                new HotelModel{Cost=1000,Hotel="Taj",City="Delhi",StoredDate=Convert.ToDateTime("21/03/2019")},
                new HotelModel{Cost=5000,Hotel="Landmark",City="Delhi",StoredDate=Convert.ToDateTime("24/03/2019")},
                new HotelModel{Cost=2500,Hotel="leela",City="Noida",StoredDate=Convert.ToDateTime("26/03/2019")},
                new HotelModel{Cost=3500,Hotel="Ambiance",City="Delhi",StoredDate=Convert.ToDateTime("11/06/2019")},
                new HotelModel{Cost=10000,Hotel="Satyma",City="paris",StoredDate=Convert.ToDateTime("21/07/2019")},
                new HotelModel{Cost=1500,Hotel="regency",City="doha",StoredDate=Convert.ToDateTime("21/03/2019")},
                new HotelModel{Cost=1700,Hotel="raj darbar",City="Delhi",StoredDate=Convert.ToDateTime("15/05/2019")},
                new HotelModel{Cost=7800,Hotel="bhola",City="Seattle",StoredDate=Convert.ToDateTime("18/03/2019")},
                new HotelModel{Cost=6700,Hotel="baba",City="Delhi",StoredDate=Convert.ToDateTime("17/04/2019")},
                new HotelModel{Cost=3900,Hotel="redmond",City="Seattle",StoredDate=Convert.ToDateTime("28/07/2019")},
                new HotelModel{Cost=2800,Hotel="Kent",City="Seattle",StoredDate=Convert.ToDateTime("21/08/2019")},
                new HotelModel{Cost=2470,Hotel="ZEE",City="Dubai",StoredDate=Convert.ToDateTime("10/03/2019")},
                new HotelModel{Cost=3290,Hotel="raja raj",City="mumbai",StoredDate=Convert.ToDateTime("18/05/2019")}
            };
            DateTime x = date.ToString() != "x" ? Convert.ToDateTime(date["timex"].First.ToString()) : Convert.ToDateTime("22/04/2018");
            var result = from s in HotelData
                         where s.City.ToLower() == loc1.ToLower() || s.Hotel.ToLower() == hotal.ToString().ToLower()
                         || s.Cost <= decimal.Parse(cost.ToString()) || s.StoredDate == x
                         select s;
            string dataresult = "Hotels => ";
            if (result.Count() > 0)
            {

                foreach (var r in result)
                {
                    dataresult += "Hotel :" + r.Hotel + "\nCity => " + r.City + "\n Date => " + r.StoredDate + "\nCost => "+r.Cost+"\n";
                }

            }
            else
            {
                foreach (var r in HotelData)
                {
                    dataresult += "Hotel :" + r.Hotel + "\nCity => " + r.City + "\n Date => " + r.StoredDate + "\nCost => " + r.Cost + "\n";
                }
            }









            return dataresult;
        }




    }
}
