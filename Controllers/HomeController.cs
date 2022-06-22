using Microsoft.AspNetCore.Mvc;

namespace AppMercadoBasico.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
