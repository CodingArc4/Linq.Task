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
        public Company Company { get; set; }
        public InternshipDetails Details { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
