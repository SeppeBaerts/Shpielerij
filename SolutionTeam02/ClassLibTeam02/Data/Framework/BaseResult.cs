using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Data.Framework
{
    /// <summary>
    /// CL-05
    /// </summary>
    public abstract class BaseResult
    {
        private List<string> errors = new List<string>();
        public string ConnectionString { get; set; }
        public DataTable DataTable { get; set; }
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors => errors;
        public void AddError(string error)
        {
            errors.Add(error);
        }
    }
}
