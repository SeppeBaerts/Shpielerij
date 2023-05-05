using ClassLibTeam02.Business;
using ClassLibTeam02.Business.Entities;
using ClassLibTeam02.Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Data.Repositories
{
    public static class HouseRepository
    {
        public static List<House> GetHouses()
        {
            return (List<House>)Houses.GetHouses();
        }
        public static InsertResult AddHouse(House house)
        {
            return Houses.Addhouse(house);
        }
        public static bool AreaCodeExists(string areaCode)
        {
            return Houses.HasCity(areaCode);
        }
        public static InsertResult AddHouseAndCity(House house)
        {
            InsertResult result = Houses.AddCity(house.HouseAdress.City, house.HouseAdress.AreaCode);
            if (result.Succeeded && result.Errors.Count() == 0)
                result = Houses.Addhouse(house);
            return result;            
        }
    }
}
