using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjVideoClub.Data;
using ProjVideoClub.Models;

namespace ProjVideoClub.Controllers
{
    public class RegistoAlugueresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistoAlugueresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistoAlugueres
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IDCLI") > 0 && HttpContext.Session.GetString("UTILIZADOR") != "funcionario")
            {
                var applicationDbContext = _context.TRegistoAlugueres.Include(r => r.Filme).Include(r => r.Utilizador);
                return View(await applicationDbContext.ToListAsync());
            }
            else
                return Redirect("~/Login/Login");
        }

        // GET: RegistoAlugueres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TRegistoAlugueres == null)
            {
                return NotFound();
            }

            var registoAluguer = await _context.TRegistoAlugueres
                .Include(r => r.Filme)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registoAluguer == null)
            {
                return NotFound();
            }

            return View(registoAluguer);
        }

        // GET: RegistoAlugueres/Create
        public IActionResult Create()
        {
            ViewData["FilmeId"] = new SelectList(_context.TFilmes, "Id", "Titulo");
            ViewData["UtilizadorId"] = new SelectList(_context.TUtilizadores, "Id", "Email");
            return View();
        }

        // POST: RegistoAlugueres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmeId,UtilizadorId,DataInicio,DataFim")] RegistoAluguer registoAluguer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registoAluguer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeId"] = new SelectList(_context.TFilmes, "Id", "Titulo", registoAluguer.FilmeId);
            ViewData["UtilizadorId"] = new SelectList(_context.TUtilizadores, "Id", "Email", registoAluguer.UtilizadorId);
            return View(registoAluguer);
        }

        // GET: RegistoAlugueres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TRegistoAlugueres == null)
            {
                return NotFound();
            }

            var registoAluguer = await _context.TRegistoAlugueres.FindAsync(id);
            if (registoAluguer == null)
            {
                return NotFound();
            }
            ViewData["FilmeId"] = new SelectList(_context.TFilmes, "Id", "Titulo", registoAluguer.FilmeId);
            ViewData["UtilizadorId"] = new SelectList(_context.TUtilizadores, "Id", "Email", registoAluguer.UtilizadorId);
            return View(registoAluguer);
        }

        // POST: RegistoAlugueres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilmeId,UtilizadorId,DataInicio,DataFim")] RegistoAluguer registoAluguer)
        {
            if (id != registoAluguer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registoAluguer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistoAluguerExists(registoAluguer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeId"] = new SelectList(_context.TFilmes, "Id", "Titulo", registoAluguer.FilmeId);
            ViewData["UtilizadorId"] = new SelectList(_context.TUtilizadores, "Id", "Email", registoAluguer.UtilizadorId);
            return View(registoAluguer);
        }

        // GET: RegistoAlugueres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TRegistoAlugueres == null)
            {
                return NotFound();
            }

            var registoAluguer = await _context.TRegistoAlugueres
                .Include(r => r.Filme)
                .Include(r => r.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registoAluguer == null)
            {
                return NotFound();
            }

            return View(registoAluguer);
        }

        // POST: RegistoAlugueres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TRegistoAlugueres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TRegistoAlugueres'  is null.");
            }
            var registoAluguer = await _context.TRegistoAlugueres.FindAsync(id);
            if (registoAluguer != null)
            {
                _context.TRegistoAlugueres.Remove(registoAluguer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistoAluguerExists(int id)
        {
          return (_context.TRegistoAlugueres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
