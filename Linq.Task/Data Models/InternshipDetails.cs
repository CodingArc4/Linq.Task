using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Task.Data_Models
{
    internal class InternshipDetails
    {
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public List<string>? Skills { get; set; }
        public int Duration { get; set; }
        public bool IsRemote { get; set; }
    }
}
