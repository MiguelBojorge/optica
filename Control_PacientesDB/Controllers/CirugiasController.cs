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
            CargarListas();
            return View();
        }

        // POST: Cirugias/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CirugiasViewModel model)
        {
            if (ModelState.IsValid)
            {
                CargarListas();
                return View(model);
            }
            try
            {
                var cirugia = new Cirugias
                {
                    Codigo_diagnostico = model.Codigo_diagnostico,
                    Codigo_medico = model.Codigo_medico,
                    Fecha_cirugia = model.Fecha_cirugia,
                    Hora_inicio = model.Hora_inicio,
                    Hora_fin = model.Hora_fin
                };

                _context.Add(cirugia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Error al guardar: {ex.InnerException?.Message}");
                CargarListas();
                return View(model);
            }
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
            var viewModel = new CirugiasViewModel
            {
                Codigo_cirugia = cirugias.Codigo_cirugia,
                Codigo_diagnostico = cirugias.Codigo_diagnostico,
                Codigo_medico = cirugias.Codigo_medico,
                Fecha_cirugia = cirugias.Fecha_cirugia,
                Hora_inicio = cirugias.Hora_inicio,
                Hora_fin = cirugias.Hora_fin
            };
            CargarListas();
            return View(viewModel);
        }

        // POST: Cirugias/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CirugiasViewModel model)
        {
            if (id != model.Codigo_cirugia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cirugiaExistente = await _context.Cirugias
                       .AsNoTracking()
                       .FirstOrDefaultAsync(c => c.Codigo_cirugia == id);

                    if (cirugiaExistente == null) return NotFound();

                    var cirugiaActualizada = new Cirugias
                    {
                        Codigo_cirugia = id,
                        Codigo_diagnostico = model.Codigo_diagnostico,
                        Codigo_medico = model.Codigo_medico,
                        Fecha_cirugia = model.Fecha_cirugia,
                        Hora_inicio = model.Hora_inicio,
                        Hora_fin = model.Hora_fin
                    };

                    _context.Attach(cirugiaActualizada);
                    _context.Entry(cirugiaActualizada).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CirugiaExists(id)) return NotFound();
                    throw;
                }
            }

            CargarListas();
            return View(model);
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

        private bool CirugiaExists(int id)
        {
            return _context.Cirugias.Any(e => e.Codigo_cirugia == id);
        }

        private void CargarListas()
        {
            ViewBag.Codigo_diagnostico = new SelectList(_context.Diagnostico, "Codigo_diagnostico", "Codigo_diagnostico");
            ViewBag.Codigo_medico = new SelectList(_context.Medico, "Codigo_medico", "NombreCompleto");
        }
    }
}
