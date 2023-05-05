using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Data.Framework
{
    /// <summary>
    /// CL-05
    /// </summary>
    public static class LocalSettings
    {
        public static bool hasconnectionFailed;
        /// <summary>
        /// Get the string to connect to the database
        /// </summary>
        /// <returns>
        /// If connection hasn't failed, it will return the pxl connection string, otherwise it will return Local connection string
        /// </returns>
        public static string GetConnectionString()
        {
            string connectionString = "Trusted_Connection=True;";
            connectionString += @"Server=5CG21507P8\SQLEXPRESS;";
            connectionString += $"Database=DB_Seppe_Baerts;";
            return connectionString;

        }
    }
}
