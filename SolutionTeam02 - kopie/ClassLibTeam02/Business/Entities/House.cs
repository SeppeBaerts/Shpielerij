using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Business.Entities
{
    /// <summary>
    /// CL-01
    /// </summary>
    public class House
    {
        internal int? HouseId { get; set; }
        #region Adress
        public Adress HouseAdress { get; set; } 
        public string Adress { get
            {
                return HouseAdress.ToString();
            }
        }
        #endregion
        #region HouseProperty's
        public string HouseType { get; set; }
        public short AmountRooms { get; set; }
        /// <summary>
        /// 0 --> appartment (Floor cannot be null at this point)
        /// </summary>
        public byte Floors { get; set; }
        /// <summary>
        /// can be null
        /// </summary>
        public short? Floor { get; set; }
        public double HouseSurface { get; set; }
        /// <summary>
        /// If price is 0 --> house is sold.
        /// </summary>
        public double Price { get; set; }
        #endregion
        private string imageSource;
        private string audioSource;
        public string ImageFilePath { get; set; }
        public string AudioFilePath { get; set; }
        public double? GardenSurface { get; set; }
        public int CreationYear { get; set; }
        public House(int houseId, string street,string houseNumber
            ,string areaCode, int creationYear, byte houseType,
            short amountRooms, byte floors, short? floor, double houseSurface,
            double? gardenSurface, double price, string imageFilePath, string audioFilePath)
        {
            HouseId = houseId;
            HouseAdress = new Adress(street, houseNumber, areaCode);
            SetHouseType(houseType);
            AmountRooms = amountRooms;
            Floor = floor;
            Floors = floors;
            HouseSurface = houseSurface;
            GardenSurface = gardenSurface;
            Price = price;
            CreationYear = creationYear;
            ImageFilePath = imageFilePath;
            AudioFilePath = audioFilePath;
            //GetImageSource();
        }
        public House()
        {

        }
        async public void GetImageSource()
        {
            DropboxClient dbx = new DropboxClient("sl.Bdr2YHnmpBDxZ5g4Z-uBCt_a4aZrVhC0kckEz4bDMym1O-jt2nczYgsKM4ASPz38x8UbJNSsi71d0TtQHA8akUEEMv_wjr4k4DVngawAbHcRP8nQjC4uUO9afV9hNphw9zkPplI0e5hP");
            using (var response = await dbx.Files.DownloadAsync($"{ImageFilePath}"))
            {
                try
                {
                    imageSource = await response.GetContentAsStringAsync();
                }
                catch
                {

                }
            }
        }

        public byte ConvertHouseType()
        {
            switch (HouseType.ToLower())
            {
                case "open": return 0;
                case "half-open": return 1;
                case "gesloten": return 2;
                default: return 3;
            }
        }
        private void SetHouseType(int houseType)
        {
            switch(houseType)
            {
                case 0: HouseType = "Open"; break;
                case 1: HouseType = "Half-Open"; break;
                case 2: HouseType = "Gesloten"; break;
                default: HouseType = "Unknown"; break;

            }
        }
    }
}
