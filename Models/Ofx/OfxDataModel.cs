using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ofx
{
    public class OfxDataModel
    {
        public DateTime DateCtrl { get; set; }
        public List<OfxModel> Ofxs { get; set; }
    }
}
