using Microsoft.AspNetCore.Mvc;

namespace TestsStore.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			return new RedirectResult("~/swagger");
		}
    }
}