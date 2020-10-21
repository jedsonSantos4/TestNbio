using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Businnes.OFX;
using DataAcessMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interface.Business;
using Models.Interface.Repository;
using Models.Ofx;
using Unity;

namespace TestsUnit
{
    [TestClass]
    public class UniTest
    {
        #region variaveis injeção de dependência        

        private readonly IRepositoryOFX _repoOfx;
        private readonly IBusinessOFX _businessofx;

        #endregion


        public UniTest()
        {

            var container = new UnityContainer();
            ConsomeBusiness(container);
            DadosProducao(container);

            _repoOfx = container.Resolve<IRepositoryOFX>();
            _businessofx = container.Resolve<IBusinessOFX>();


        }

        private static void ConsomeBusiness(UnityContainer container)
        {
            container.RegisterType(typeof(IBusinessOFX), typeof(OfxBusiness));
        }
        private static void DadosProducao(UnityContainer container)
        {
            container.RegisterType(typeof(IRepositoryOFX), typeof(DataAcessOFX));
        }


        [TestMethod]
        public void Chamar_regra()
        {

            var tr = _businessofx.Get("extrato1.ofx");
            
           


            //var teste = _repoOfx.Get("../ofx_teste.xml", "teste");

            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Acesso_aos_dados()
        {
            var teste = _repoOfx.Get("extrato1.ofx", "teste");
            //var teste = _repoOfx.Get("../ofx_teste.xml", "teste");

            Assert.IsTrue(teste.Status);
        }

        [TestMethod]
        public void Unificar_lista()
        {
            var lista1 = _repoOfx.Get("extrato1.ofx", "teste");
            if (lista1.Status)
            {
                var lista2 = _repoOfx.Get("extrato2.ofx", "teste");
                if (lista2.Status)
                {
                    var show = new List<OfxDataModel>();
                    UnificarList(lista1.Value, show);
                    UnificarList(lista2.Value, show);

                    var tt = show.OrderBy(d=> d.DateCtrl);


                }
            }


            //var teste = _repoOfx.Get("../ofx_teste.xml", "teste");

            Assert.IsTrue(false);
        }


        private IEnumerable<OfxDataModel> UnificarList(List<OfxModel> ofxs, List<OfxDataModel> ofxDate){

            foreach (var item in ofxs)
            {
                if (ofxDate?.Count == 0)
                {
                    ofxDate.Add(new OfxDataModel {
                        DateCtrl = item.DTPOSTED.Date,
                        Ofxs = new List<OfxModel> { item }
                    });
                }
                else if (ofxDate.Any(c=> c.DateCtrl.Date == item.DTPOSTED.Date)) {
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
