using Microsoft.AspNetCore.Mvc;

namespace ProjVideoClub.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UTILIZADOR", "");
            HttpContext.Session.SetInt32("IDCLI", 0);
            return Redirect("~/Home/Index");
        }
    }
}
