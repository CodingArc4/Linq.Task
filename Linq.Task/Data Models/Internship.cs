using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Task.Data_Models
{
    internal class Internship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Details { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
