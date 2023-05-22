using ClassLibTeam02.Business.Entities;

namespace WebApiTeam02.Models
{
    public class HouseModel
    {
        public int? HouseId { get; set; }
        #region Adress
        public Adress HouseAdress { get; set; }
        public string Adress
        {get{return HouseAdress.ToString();}}
        #endregion
        #region HouseProperty's
        public string? HouseType { get; set; }
        public short AmountRooms { get; set; }
        public byte Floors { get; set; }
        public short? Floor { get; set; }
        public double HouseSurface { get; set; }
        public double Price { get; set; }
        #endregion
        public string ImageFilePath{ get; set; }
        public string AudioFilePath{get; set;}
        public double? GardenSurface { get; set; }
        public int CreationYear { get; set; }
    }
}
