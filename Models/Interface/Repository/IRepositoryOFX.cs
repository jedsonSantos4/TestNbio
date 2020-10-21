using Models.Ofx;
using Models.Result;
using System.Collections.Generic;

namespace Models.Interface.Repository
{
    public interface IRepositoryOFX
    {
        ValidResult<List<OfxModel>> Get(string entyti, string origem);
    }
}
