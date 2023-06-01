using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjVideoClub.Data;
using ProjVideoClub.Models;
using System.Security.Cryptography.X509Certificates;

namespace ProjVideoClub.Controllers
{
    public class AltInfoContaController : Controller
    {
        private readonly ApplicationDbContext db;

        public AltInfoContaController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                Utilizador utilizador = new Utilizador();

                int idCli = (int)HttpContext.Session.GetInt32("IDCLI");

                utilizador = db.TUtilizadores.Find(idCli);

                //Como existe sempre um utilizador, só é enviado se o obj possui informação
                return View(utilizador);
            }
            else
                return Redirect("~/Login/Login");
        }

        public IActionResult Alterar(string Nome, string Email, string Password)
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                Utilizador utilizador = new Utilizador();
                utilizador = db.TUtilizadores.Find(HttpContext.Session.GetInt32("IDCLI"));
                utilizador.Nome = Nome;
                utilizador.Email = Email;
                utilizador.Password = Password;

                db.TUtilizadores.Update(utilizador);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
                return Redirect("~/Login/Login");
        }
    }
}
