using Microsoft.AspNetCore.Mvc;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext db;

        public LoginController(ApplicationDbContext context)
        {
            db = context;
        }

		public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0)
            {
                if (HttpContext.Session.GetString("UTILIZADOR") == "funcionario")
                {
                    return Redirect("~/FuncDashboard/Dashboard");
                }
                else
                {
                    return Redirect("~/CliCatalogo/Catalogo");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
		public IActionResult Login(string email, string password)
        {
            Utilizador conta = new Utilizador();

            conta = db.TUtilizadores.FirstOrDefault(t => t.Email == email && t.Password == password);
            
            if (conta == null)
            {
                ViewBag.INVALID = "Email/Password incorretos!";

                return View();
            }
            else
            {
                HttpContext.Session.SetString("UTILIZADOR", conta.Nome.ToString());
                HttpContext.Session.SetInt32("IDCLI", conta.Id);

                if (conta.Nome == "funcionario")
                {
                    return Redirect("~/FuncDashboard/Dashboard");
                }
                else
                {
                    return Redirect("~/CliCatalogo/Catalogo");
                }
            }
        }
    }
}
