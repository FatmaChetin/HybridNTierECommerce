using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class CategoryVM
    {
        public int ID { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}
        public DateTime? DeletedDate { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; } 
        public string DataStatus { get; set; }

    }
}
