using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Business.Entities
{
    public class Adress
    {
        public string Street { get; set; }
        /// <summary>
        /// Could be 4b or 5a, so an int would crash it.
        /// </summary>
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string AreaCode { get; set; }
        /// <summary>
        /// if AreaCode is already in use, you can use this one.
        /// </summary>
        public Adress(string street, string houseNumber, string areaCode, string city = null)
        {
            Street = street;
            HouseNumber = houseNumber;
            City = city ?? Houses.GetCity(areaCode);
            AreaCode = areaCode;
        }
        public override string ToString()
        {
            return $"{Street} {HouseNumber} {AreaCode} {City}";
        }

    }
}
