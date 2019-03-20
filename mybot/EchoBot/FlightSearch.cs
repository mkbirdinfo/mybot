using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace EchoBot
{
    public class FlightSearch
    {
     public static string FlightSearchResult(JObject entities)
        {
            

       string   loc1 = entities.GetValue("Location")!=null ? entities.GetValue("Location").FirstOrDefault().ToString():"x";
            var loc2 = entities.GetValue("Location")!=null? entities.GetValue("Location").LastOrDefault():"x";
            var ftype = "x";
            if (entities.GetValue("FlightType") != null)
            {
                if(entities.GetValue("FlightType").Count() > 1)
                {
                    ftype= entities.GetValue("FlightType").FirstOrDefault().ToString() + " " +  entities.GetValue("FlightType").LastOrDefault().ToString();
                }
                else
                {
                    ftype = entities.GetValue("FlightType").FirstOrDefault().ToString();
                }
            }
                   
            var city1 = entities.GetValue("City")!=null? entities.GetValue("City").FirstOrDefault():"x";
            dynamic date=  entities.GetValue("datetime")!=null? entities.GetValue("datetime").FirstOrDefault(): "x";
            List <FlightModel> FlightData = new List<FlightModel>
            {
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="Delhi",Storeddate=Convert.ToDateTime("21/03/2019")},
                new FlightModel{Location="delhi",FlightType="Jet",City="Delhi",Storeddate=Convert.ToDateTime("24/03/2019")},
                new FlightModel{Location="London",FlightType="Air india",City="Noida",Storeddate=Convert.ToDateTime("26/03/2019")},
                new FlightModel{Location="Mumbai",FlightType="Jet",City="Delhi",Storeddate=Convert.ToDateTime("11/06/2019")},
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="paris",Storeddate=Convert.ToDateTime("21/07/2019")},
                new FlightModel{Location="Dubai",FlightType="Indigo",City="doha",Storeddate=Convert.ToDateTime("21/03/2019")},
                new FlightModel{Location="Seattle",FlightType="air asia",City="Delhi",Storeddate=Convert.ToDateTime("15/03/2019")},
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="Seattle",Storeddate=Convert.ToDateTime("18/03/2019")},
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="Delhi",Storeddate=Convert.ToDateTime("17/04/2019")},
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="Seattle",Storeddate=Convert.ToDateTime("28/07/2019")},
                new FlightModel{Location="Delhi",FlightType="Indigo",City="Seattle",Storeddate=Convert.ToDateTime("21/08/2019")},
                new FlightModel{Location="Mumbai",FlightType="Indigo",City="Dubai",Storeddate=Convert.ToDateTime("10/03/2019")}
            };
            DateTime x = date.ToString() != "x" ? Convert.ToDateTime(date["timex"].First.ToString()): Convert.ToDateTime("22/04/2018");
            //var result = FlightData.Where(x => x.Location.ToLower() == loc1.ToLower() || x.FlightType == ftype.ToString()
            //             || x.City == city1.ToString()).ToList();

            var result = from s in FlightData
                         where s.Location.ToLower() == loc1.ToLower() ||s.City.ToLower()==city1.ToString().ToLower()
                         ||s.FlightType.ToLower()==ftype.ToString().ToLower()|| s.Storeddate == x
                         select s;
            string dataresult = "Flights => ";
            if (result.Count() > 0)
            {
               
                foreach(var r in result)
                {
                    dataresult += "flight :" + r.FlightType + "\nFrom => " + r.Location + "\nTo => " + r.City + "\n Date => " + r.Storeddate + "\n";
                }

            }
            else
            {
                foreach (var r in FlightData)
                {
                    dataresult += "flight :" + r.FlightType + "\nFrom => " + r.Location + "\nTo => " + r.City + "\n Date => " + r.Storeddate + "\n";
                }
            }



             return dataresult;
        }

    }
}
