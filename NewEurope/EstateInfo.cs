using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEurope
{
    class EstateInfo
    {
        public int PropertyID { get; set; }
        public int CategoryID { get; set; }
        public int Rooms { get; set; }
        public double Area { get; set; }
        public double RentPrice { get; set; }
        public double SalePrice { get; set; }
        public string Address { get; set; }
        public bool IsRepaired { get; set; }


    }
}
