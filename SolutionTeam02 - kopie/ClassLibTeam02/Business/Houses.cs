using ClassLibTeam02.Business.Entities;
using ClassLibTeam02.Data;
using ClassLibTeam02.Data.Framework;
using ClassLibTeam02.Data.Repositories;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Business
{
    /// <summary>
    /// CL-03 (Er staat Business > Entities > 'Artists' als voorbeeld, maar in de solution exploror staat artists onder enteties)
    /// </summary>
    public static class Houses
    {
        /// <summary>
        /// CL-03
        /// </summary>
        /// <returns>
        /// List of all houses in the Database
        /// </returns>
        public static IEnumerable<House> GetHouses()
        {
            HouseData data = new HouseData();
            ///CL-07
            SelectResult houses = data.Select();
            List<House> houseList = new List<House>();
            DataTable dataTable = houses.DataTable;
            foreach(DataRow row in dataTable.Rows)
            {
                object[] itemArray = row.ItemArray;
                                         //HouseiD              Street              houseNumber                areaCode        creationYear           houseType         amountRooms             floors                  floor                                                 houseSurface         gardenSurface                                                Price                   image
                houseList.Add(new House((int)itemArray[0], (string)itemArray[1], (string)itemArray[2], (string)itemArray[3], (short)itemArray[4], (byte)itemArray[5], (short)itemArray[6], (byte)itemArray[7], itemArray[8] == DBNull.Value? null : (short?)itemArray[8], (double)itemArray[9], itemArray[10] == DBNull.Value? null : (double?)itemArray[10], (double)itemArray[11], itemArray[12] == DBNull.Value ? null : (string)itemArray[12], itemArray[13] == DBNull.Value ? null : (string)itemArray[13]));
            }
            return houseList;
        }
        /// <summary>
        /// CL-04
        /// </summary>
        /// <param name="house">
        /// House that needs to be inserted in the Database
        /// </param>
        /// <returns>
        /// InsertResult --> succeed will be true if the house is added.
        /// </returns>
        public static InsertResult Addhouse(House house)
        {
            HouseData houses = new HouseData();
            ///CL-08
            return houses.InsertHouse(house);
        }

        public static string GetCity(string areaCode)
        {
            HouseData house = new HouseData();
            DataTable dt = house.GetCity(areaCode).DataTable;
            return dt.Rows[0].ItemArray[0].ToString();
        }
        public static bool HasCity(string areaCode)
        {
            HouseData house = new HouseData();
            DataTable dt = house.GetCity(areaCode).DataTable;
            return dt.Rows.Count > 0;
        }
        public static InsertResult AddCity(string city, string areaCode)
        {
            HouseData houseData = new HouseData();
            return houseData.InsertCity(city, areaCode);
        }

    }
}
