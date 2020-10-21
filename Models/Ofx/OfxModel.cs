using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ofx
{
    public class OfxModel
    {
        public string TRNTYPE { get; set; }
        public DateTime DTPOSTED { get; set; }
        public decimal TRNAMT { get; set; }
        public string MEMO { get; set; }
    }


   
}
