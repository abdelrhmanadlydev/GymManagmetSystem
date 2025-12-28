using GymManagmetBLL.Service.Interfasces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmetPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalutiysService _analutiysService;

        public HomeController(IAnalutiysService analutiysService)
        {
            _analutiysService = analutiysService;
        }
        public ActionResult Index()
        {
            var data = _analutiysService.GetAnalutiysData();
            return View(data);
        }
    }
}
