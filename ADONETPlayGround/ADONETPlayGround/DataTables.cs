using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETPlayGround
{
    internal static class DataTables
    {
        private static DataSet ds = new DataSet();
        public static DataTable Dtstudents { get; set; } = new DataTable();
        public static void Initialise()
        {
            ds.Tables.Add(Dtstudents);
        }
    }
}
