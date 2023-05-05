using ClassLibTeam02.Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ClassLibTeam02.Business.Entities;

namespace ClassLibTeam02.Data
{
    /// <summary>
    /// CL-06
    /// </summary>
    public class HouseData : SqlServer
    {
        public HouseData() : base()
        {

        }
        /// <summary>
        /// CL-07
        /// </summary>
        /// <returns></returns>
        public SelectResult Select()
        {
            return Select("Houses");
        }
        /// <summary>
        /// CL-08
        /// </summary>
        /// <param name="street"></param>
        /// <param name="homeNumber"></param>
        /// <param name="areaCode"></param>
        /// <param name="creationYear"></param>
        /// <param name="houseType"></param>
        /// <param name="rooms"></param>
        /// <param name="floors"></param>
        /// <param name="floor">Can be null</param>
        /// <param name="houseSurface"></param>
        /// <param name="gardenSurface"> Can be null</param>
        /// <param name="price"></param>
        /// <returns></returns>
        public InsertResult InsertHouse(string street, string homeNumber, string areaCode, int creationYear, byte houseType, short rooms, byte floors, short? floor, double houseSurface, double? gardenSurface, double price)
        {
            var result = new InsertResult();
            try
            {
                //SQL Command
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append($"Insert INTO Houses ");
                insertQuery.Append($"(Street, HomeNumber, AreaCode, CreationYear, HouseType, Rooms, Floors, Floor, HouseSurface, GardenSurface, Price) VALUES ");
                insertQuery.Append($"(@Street, @HomeNumber, @AreaCode, @CreationYear, @HouseType, @Rooms, @Floors, @Floor, @HouseSurface, @GardenSurface, @Price); ");
                using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
                {
                    insertCommand.Parameters.Add("@Street"
                    , SqlDbType.VarChar).Value =
                    street;
                    insertCommand.Parameters.Add("@HomeNumber"
                    , SqlDbType.VarChar).Value =
                    homeNumber;
                    insertCommand.Parameters.Add("@AreaCode"
                    , SqlDbType.VarChar).Value =
                    areaCode;
                    insertCommand.Parameters.Add("@CreationYear"
                    , SqlDbType.SmallInt).Value =
                    creationYear;
                    insertCommand.Parameters.Add("@HouseType"
                    , SqlDbType.TinyInt).Value =
                    houseType;
                    insertCommand.Parameters.Add("@Rooms"
                    , SqlDbType.SmallInt).Value =
                    rooms;
                    insertCommand.Parameters.Add("@Floors"
                    , SqlDbType.TinyInt).Value =
                    floors; 
                    if(floor == null)
                        insertCommand.Parameters.Add("@Floor"
                        , SqlDbType.SmallInt).Value = DBNull.Value;
                    else
                        insertCommand.Parameters.Add("@Floor"
                            ,SqlDbType.SmallInt).Value =floor;

                    insertCommand.Parameters.Add("@HouseSurface"
                    , SqlDbType.Float).Value =
                    houseSurface;
                    if (gardenSurface == null) 
                        insertCommand.Parameters.Add("@GardenSurface"
                    , SqlDbType.Float).Value =
                    DBNull.Value;
                    else insertCommand.Parameters.Add("@GardenSurface"
                        ,SqlDbType.Float).Value =gardenSurface;

                    insertCommand.Parameters.Add("@Price"
                    , SqlDbType.Float).Value =
                    price;
                    result = InsertRecord(insertCommand);
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// CL-08
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public InsertResult InsertHouse(House house)
        {
            return InsertHouse(house.HouseAdress.Street, house.HouseAdress.HouseNumber, house.HouseAdress.AreaCode, house.CreationYear, house.ConvertHouseType(), house.AmountRooms, house.Floors, house.Floor, house.HouseSurface, house.GardenSurface, house.Price);
        }

        public SelectResult GetCity(string areaCode)
        {
            return SelectCity(areaCode);
        }

        public InsertResult InsertCity(string city, string areaCode)
        {
            var result = new InsertResult();
            try
            {
                StringBuilder insertQuerry = new StringBuilder();
                insertQuerry.Append("Insert INTO AreaCodes ");
                insertQuerry.Append("(AreaCode, City) VALUES");
                insertQuerry.Append("(@AreaCode, @City)");
                using(SqlCommand insertCommand = new SqlCommand(insertQuerry.ToString()))
                {
                    insertCommand.Parameters.Add
                        ("@AreaCode", SqlDbType.VarChar)
                        .Value = areaCode;
                    insertCommand.Parameters.Add
                        ("@City", SqlDbType.VarChar)
                        .Value = city;
                    result = InsertCity(insertCommand);
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;

        }
    }
}
