using Models.Interface.Business;
using Models.Interface.Repository;
using Models.Ofx;
using Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businnes.OFX
{
    public class OfxBusiness : IBusinessOFX
    {
        #region variaveis injeção de dependência        

        private readonly IRepositoryOFX _repoOfx;

        #endregion

        public OfxBusiness(IRepositoryOFX repoOfx)
        {
            _repoOfx = repoOfx;
        }


        public ValidResult<List<OfxModel>> Get(string entyti, string origem)=> _repoOfx.Get(entyti, origem);

        public ValidResult<List<OfxDataModel>> Get(string entyti)
        {
            var valid = new ValidResult<List<OfxDataModel>> { Value = new List<OfxDataModel>(),Status=true} ;

            var arquivo = "extrato2.ofx;extrato1.ofx";

            foreach (var item in arquivo.Split(';'))
            {
                var ofxs = Get($"{entyti}Content\\FileOFX\\OFX\\{item}", "cod");

                if (!ofxs.Status)
                    return new ValidResult<List<OfxDataModel>> { Message = ofxs.Message, Status=false };

                 UnificarList(ofxs.Value, valid.Value).ToList();
            }
           

            return valid;

        }

        private IEnumerable<OfxDataModel> UnificarList(List<OfxModel> ofxs, List<OfxDataModel> ofxDate)
        {

            foreach (var item in ofxs)
            {
                if (ofxDate == null)
                {
                    ofxDate = new List<OfxDataModel> { new OfxDataModel
                    {
                        DateCtrl = item.DTPOSTED.Date,
                        Ofxs = new List<OfxModel> { item }
                    }};
                }
                else if (ofxDate.Any(c => c.DateCtrl.Date == item.DTPOSTED.Date))
                {
                    ofxDate.FirstOrDefault(c => c.DateCtrl.Date == item.DTPOSTED.Date).Ofxs.Add(item);
                }
                else
                {
                    ofxDate.Add(new OfxDataModel
                    {
                        DateCtrl = item.DTPOSTED.Date,
                        Ofxs = new List<OfxModel> { item }
                    });

                }

            }
            return ofxDate;

        }
    }
}
