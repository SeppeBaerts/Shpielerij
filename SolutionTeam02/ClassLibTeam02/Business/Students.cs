using ClassLibTeam02.Business.Entities;
using ClassLibTeam02.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Business
{
    public static class Students
    {
        public static IEnumerable<Student> List()
        {
            return StudentRepository.StudentList;
        }
        public static void Add(string firstName, string lastName)
        {
            StudentRepository.Add(firstName, lastName);
        }
    }
}
