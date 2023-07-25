using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETPlayGround
{
    internal class Students
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Richting { get; set; }
        public Students(string voornaam, string naam, string richting)
        {
            Naam = naam;
            Voornaam = voornaam;
            Richting = richting;
        }
        public object[] GetProperties()
        {
            return new object[] { Naam, Voornaam, Richting };
        }
    }
}
