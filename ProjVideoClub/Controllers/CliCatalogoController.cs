using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class CliCatalogoController : Controller
    {
        private readonly ApplicationDbContext db;

        public CliCatalogoController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Catalogo(int? idCat, string? titulo)
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                ViewBag.CAT = new SelectList(db.TCategorias, "Id", "NomeCat");

                List <Filme> listaFilmes = new List<Filme>();

                if (idCat == null && titulo == null)
                {
                    //Mostra tudo (sem filtro definidos)
                    listaFilmes = db.TFilmes.Include(r => r.Categoria).ToList();
                }
                else if (titulo == null)
                {
                    //Filtrado por categoria (todos os filmes)
                    listaFilmes = db.TFilmes.Where(m => m.CategoriaId == idCat).Include(r => r.Categoria).ToList();
                }
                else if (idCat == null)
                {
                    //pesquisa filme especifico em todas as categorias
                    listaFilmes = db.TFilmes.Where(m => m.Titulo.Contains(titulo)).Include(r => r.Categoria).ToList();
                }
                else
                {
                    //pesquisa um filme dentro da categoria
                    listaFilmes = db.TFilmes.Where(m => (m.CategoriaId == idCat && m.Titulo.Contains(titulo))).Include(r => r.Categoria).ToList();
                }

                return View(listaFilmes);
            }
            else
                return Redirect("~/Login/Login");
        }
    }
}
