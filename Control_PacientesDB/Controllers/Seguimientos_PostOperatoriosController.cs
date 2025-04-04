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
    public class Seguimientos_PostOperatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Seguimientos_PostOperatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seguimientos_PostOperatorios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Seguimientos_PostOperatorios.Include(s => s.Medicamentos).Include(s => s.Medico).Include(s => s.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Seguimientos_PostOperatorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientos_PostOperatorios = await _context.Seguimientos_PostOperatorios
                .Include(s => s.Medicamentos)
                .Include(s => s.Medico)
                .Include(s => s.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_seguimiento == id);
            if (seguimientos_PostOperatorios == null)
            {
                return NotFound();
            }

            return View(seguimientos_PostOperatorios);
        }

        // GET: Seguimientos_PostOperatorios/Create
        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        // POST: Seguimientos_PostOperatorios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seguimientos_PostOperatoriosViewModel model)
        {
            if (ModelState.IsValid)
            {
                CargarListas();
                return View(model);
            }
            try
            {
                var seguimiento = new Seguimientos_PostOperatorios
                {
                    Codigo_medico = model.Codigo_medico,
                    Codigo_paciente = model.Codigo_paciente,
                    Codigo_Medicamento = model.Codigo_Medicamento,
                    Fecha_Control = model.Fecha_Control,
                    Programacion_ProximaCita = model.Programacion_ProximaCita,
                    Observaciones = model.Observaciones
                };

                _context.Add(seguimiento);
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

        // GET: Seguimientos_PostOperatorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientos_PostOperatorios = await _context.Seguimientos_PostOperatorios.FindAsync(id);
            if (seguimientos_PostOperatorios == null)
            {
                return NotFound();
            }
            var viewModel = new Seguimientos_PostOperatoriosViewModel
            {
                Codigo_seguimiento = seguimientos_PostOperatorios.Codigo_seguimiento,
                Codigo_medico = seguimientos_PostOperatorios.Codigo_medico,
                Codigo_paciente = seguimientos_PostOperatorios.Codigo_paciente,
                Codigo_Medicamento = seguimientos_PostOperatorios.Codigo_Medicamento,
                Fecha_Control = seguimientos_PostOperatorios.Fecha_Control,
                Programacion_ProximaCita = seguimientos_PostOperatorios.Programacion_ProximaCita,
                Observaciones = seguimientos_PostOperatorios.Observaciones
            };

            CargarListas();
            return View(viewModel);
        }

        // POST: Seguimientos_PostOperatorios/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seguimientos_PostOperatoriosViewModel model)
        {
            if (id != model.Codigo_seguimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var seguimientoExistente = await _context.Seguimientos_PostOperatorios
                         .AsNoTracking()
                         .FirstOrDefaultAsync(s => s.Codigo_seguimiento == id);

                    if (seguimientoExistente == null) return NotFound();

                    var seguimientoActualizado = new Seguimientos_PostOperatorios
                    {
                        Codigo_seguimiento = id,
                        Codigo_medico = model.Codigo_medico,
                        Codigo_paciente = model.Codigo_paciente,
                        Codigo_Medicamento = model.Codigo_Medicamento,
                        Fecha_Control = model.Fecha_Control,
                        Programacion_ProximaCita = model.Programacion_ProximaCita,
                        Observaciones = model.Observaciones
                    };

                    _context.Attach(seguimientoActualizado);
                    _context.Entry(seguimientoActualizado).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Seguimientos_PostOperatoriosExists(model.Codigo_seguimiento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            CargarListas();
            return View(model);
        }

        // GET: Seguimientos_PostOperatorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientos_PostOperatorios = await _context.Seguimientos_PostOperatorios
                .Include(s => s.Medicamentos)
                .Include(s => s.Medico)
                .Include(s => s.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_seguimiento == id);
            if (seguimientos_PostOperatorios == null)
            {
                return NotFound();
            }

            return View(seguimientos_PostOperatorios);
        }

        // POST: Seguimientos_PostOperatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguimientos_PostOperatorios = await _context.Seguimientos_PostOperatorios.FindAsync(id);
            if (seguimientos_PostOperatorios != null)
            {
                _context.Seguimientos_PostOperatorios.Remove(seguimientos_PostOperatorios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Seguimientos_PostOperatoriosExists(int id)
        {
            return _context.Seguimientos_PostOperatorios.Any(e => e.Codigo_seguimiento == id);
        }

        private void CargarListas()
        {
            ViewBag.Codigo_medico = new SelectList(_context.Medico, "Codigo_medico", "NombreCompleto");
            ViewBag.Codigo_paciente = new SelectList(_context.Paciente, "Codigo_paciente", "NombreCompleto");
            ViewBag.Codigo_Medicamento = new SelectList(_context.Medicamentos, "Codigo_Medicamento", "Nombre_Medicamento");
        }
    }
}
