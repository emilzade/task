using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Feature :BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
