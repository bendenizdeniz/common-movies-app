using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Index()
        {
            using (TCAuthService.KPSPublicSoapClient tcAuth = new TCAuthService.KPSPublicSoapClient())
            {
                bool result = tcAuth.TCKimlikNoDogrula(33683255870, "Deniz Can","Altun",1998);
                Response.Write(result);
            }
            return View();
        }
    }
}