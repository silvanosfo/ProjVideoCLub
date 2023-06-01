using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class CliAlugueresController : Controller
    {
        private readonly ApplicationDbContext db;

        public CliAlugueresController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult ConsultaFilmesAlug()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                int idCli = (int)HttpContext.Session.GetInt32("IDCLI");
                List <RegistoAluguer> filmesAlugados = new List<RegistoAluguer>();
                filmesAlugados = db.TRegistoAlugueres.Where(r => (r.UtilizadorId == idCli && r.DataFim >= DateTime.UtcNow.AddHours(1))).Include(r => r.Filme).ThenInclude(r => r.Categoria).ToList();

                return View(filmesAlugados);
            }
            else
                return Redirect("~/Login/Login");
        }

        public IActionResult HistoricoAlugueres()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                int idCli = (int)HttpContext.Session.GetInt32("IDCLI");
                List<RegistoAluguer> historico = new List<RegistoAluguer>();
                historico = db.TRegistoAlugueres.Where(r => (r.UtilizadorId == idCli && r.DataFim <= DateTime.UtcNow.AddHours(1))).Include(r => r.Filme).ThenInclude(r => r.Categoria).ToList();

                return View(historico);
            }
            else
                return Redirect("~/Login/Login");
        }

        public IActionResult AlugarFilmes(int idFilme, DateTime dataFim)
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                int idCli = (int)HttpContext.Session.GetInt32("IDCLI");

                ViewBag.FILMES = new SelectList(db.TFilmes.ToList(), "Id", "Titulo");

                if (idFilme > 0)
                {
                    if (db.TRegistoAlugueres.Where(r => (r.FilmeId == idFilme && r.UtilizadorId == idCli && r.DataFim >= DateTime.UtcNow.AddHours(1))).Any())
                    {
                        ViewBag.ALERTA = "Já tem esse filme alugado, escolha outro!";

                        return View();
                    }

                    else if (dataFim <= DateTime.UtcNow.AddHours(1))
                    {
                        ViewBag.ALERTA = "Data de fim de aluguer inválida!";

                        return View();
                    }

                    else
                    {
				        RegistoAluguer novoAluguer = new RegistoAluguer();

				        novoAluguer.DataFim = dataFim;
				        novoAluguer.DataInicio = DateTime.UtcNow.AddHours(1);
				        novoAluguer.UtilizadorId = idCli;
				        novoAluguer.FilmeId = idFilme;
                        db.TRegistoAlugueres.Add(novoAluguer);
                        db.SaveChanges();

                        return RedirectToAction(nameof(ConsultaFilmesAlug));
				    }
			    }
                // Executa 1º view
                return View();
            }
            else
                return Redirect("~/Login/Login");
        }
    }
}
