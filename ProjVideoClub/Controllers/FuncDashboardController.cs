using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class FuncDashboardController : Controller
    {
        private readonly ApplicationDbContext db;

        public FuncDashboardController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") == "funcionario")
            {
                //Lista do tipo RegistoAluguer para alojar registos da DB
                List<RegistoAluguer> listaFilmes = new List<RegistoAluguer>();
                listaFilmes = db.TRegistoAlugueres.Include(r => r.Utilizador).Include(r => r.Filme).ThenInclude(r => r.Categoria).ToList();

                return View(listaFilmes);
            }
            else
                return Redirect("~/Login/Login");
        }

        public IActionResult Terminar(int id)
        {
            RegistoAluguer registo = new RegistoAluguer();
            registo = db.TRegistoAlugueres.Find(id);
            registo.DataFim = DateTime.UtcNow.AddHours(1);
            db.TRegistoAlugueres.Update(registo);
            db.SaveChanges();

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
