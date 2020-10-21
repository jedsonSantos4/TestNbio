using Models.Interface.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nebioFox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessOFX _businessofx;
        
        public HomeController(IBusinessOFX businessofx)
        {
            _businessofx = businessofx;
        }
        public ActionResult Index()
        {
            Session["filePah"] = "";


            return View();
        }


        public PartialViewResult OpenFileCtrl(string teste)
        {
            
            if (teste.Contains(".ofx"))
            {

             var result =   _businessofx.Get(Server.MapPath("~/"));
                if (!result.Status)
                {
                    ViewBag.Message = result.Message;
                    return PartialView("ErroOfx" );
                }
                return PartialView("List", result.Value);
                //return Json(result.Value, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Message= $"Erro Arquivo Selecionado não e do tipo OFX";
             return PartialView("ErroOfx") ;




        }
    }
}