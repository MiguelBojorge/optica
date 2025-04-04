using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Control_PacientesDB.Models;

namespace Control_PacientesDB.Controllers
{
    public class CirugiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CirugiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cirugias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cirugias.Include(c => c.Diagnostico).Include(c => c.Medico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cirugias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias
                .Include(c => c.Diagnostico)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(m => m.Codigo_cirugia == id);
            if (cirugias == null)
            {
                return NotFound();
            }

            return View(cirugias);
        }

        // GET: Cirugias/Create
        public IActionResult Create()
        {
            ViewData["Codigo_diagnostico"] = new SelectList(_context.Diagnostico, "Codigo_diagnostico", "Codigo_diagnostico");
            ViewData["Codigo_medico"] = new SelectList(_context.Medico.Select(m => new {
                m.Codigo_medico,
                NombreCompleto = m.Nombres + " " + m.Apellidos
            }), "Codigo_medico", "NombreCompleto");
            return View();
        }

        // POST: Cirugias/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo_cirugia,Codigo_diagnostico,Codigo_medico,Fecha_cirugia,Hora_inicio,Hora_fin")] Cirugias cirugias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cirugias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigo_diagnostico"] = new SelectList(_context.Diagnostico, "Codigo_diagnostico", "Codigo_diagnostico", cirugias.Codigo_diagnostico);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico.Select(m => new
            {
                m.Codigo_medico,
                NombreCompleto = m.Nombres + " " + m.Apellidos
            }), "Codigo_medico", "NombreCompleto");
            return View(cirugias);
        }

        // GET: Cirugias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias.FindAsync(id);
            if (cirugias == null)
            {
                return NotFound();
            }
            ViewData["Codigo_diagnostico"] = new SelectList(_context.Diagnostico, "Codigo_diagnostico", "Codigo_diagnostico", cirugias.Codigo_diagnostico);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico.Select(m => new
            {
                m.Codigo_medico,
                NombreCompleto = m.Nombres + " " + m.Apellidos
            }), "Codigo_medico", "NombreCompleto");
            return View(cirugias);
        }

        // POST: Cirugias/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo_cirugia,Codigo_diagnostico,Codigo_medico,Fecha_cirugia,Hora_inicio,Hora_fin")] Cirugias cirugias)
        {
            if (id != cirugias.Codigo_cirugia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cirugias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CirugiasExists(cirugias.Codigo_cirugia))
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
            ViewData["Codigo_diagnostico"] = new SelectList(_context.Diagnostico, "Codigo_diagnostico", "Codigo_diagnostico", cirugias.Codigo_diagnostico);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico.Select(m => new { m.Codigo_medico, NombreCompleto = m.Nombres + " " + m.Apellidos }), "Codigo_medico", "NombreCompleto");
            return View(cirugias);
        }

        // GET: Cirugias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias
                .Include(c => c.Diagnostico)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(m => m.Codigo_cirugia == id);
            if (cirugias == null)
            {
                return NotFound();
            }

            return View(cirugias);
        }

        // POST: Cirugias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cirugias = await _context.Cirugias.FindAsync(id);
            if (cirugias != null)
            {
                _context.Cirugias.Remove(cirugias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CirugiasExists(int id)
        {
            return _context.Cirugias.Any(e => e.Codigo_cirugia == id);
        }
    }
}
