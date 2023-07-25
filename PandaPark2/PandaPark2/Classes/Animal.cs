using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaPark2.Classes
{
    public abstract class Animal : CSVable
    {
        public string Country { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Diet { get; set; }
        public string Gender { get; set; }
        public abstract string HijOfZij { get; set; }
        public bool IsDangerous { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public abstract string ZijnOfHaar { get; set; }

        public Animal(string country, DateTime dayOfBirth, string diet, string gender, string hijOfZij, bool isDangerous, string name, string type, string zijnOfHaar)
        {
            Country = country;
            DayOfBirth = dayOfBirth;
            Diet = diet;
            Gender = gender;
            HijOfZij = hijOfZij;
            IsDangerous = isDangerous;
            Name = name;
            Type = type;
            ZijnOfHaar = zijnOfHaar;
        }

        protected Animal(string country, DateTime dayOfBirth, string diet, string gender, bool isDangerous, string name, string type)
        {
            Country = country;
            DayOfBirth = dayOfBirth;
            Diet = diet;
            Gender = gender;
            IsDangerous = isDangerous;
            Name = name;
            Type = type;
        }
        public virtual string Describe()
        {
            throw new NotImplementedException();
        }

        public void ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}
