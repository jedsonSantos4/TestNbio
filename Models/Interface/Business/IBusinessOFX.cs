using Models.Ofx;
using Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interface.Business
{
    public interface IBusinessOFX
    {
        ValidResult<List<OfxDataModel>> Get(string entyti);
    }
}
