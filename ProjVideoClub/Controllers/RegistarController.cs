using Microsoft.AspNetCore.Mvc;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class RegistarController : Controller
    {
        private readonly ApplicationDbContext db;

        public RegistarController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Registar()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Registar(string nome, string email, string password)
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                bool checkAccount;

                checkAccount = db.TUtilizadores.Where(r => r.Email == email).Any();

                if (checkAccount)
                {
                    ViewBag.INVALID = "Esse email já está em uso!";

                    return View();
                }
                else
                {
                    Utilizador novaConta = new Utilizador();
                    novaConta.Email = email;
                    novaConta.Nome = nome;
                    novaConta.Password = password;
                    db.TUtilizadores.Add(novaConta);
                    db.SaveChanges();

                    return Redirect("~/Login/Login");
                }
            }
        }
    }
}
