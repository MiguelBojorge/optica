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
    public class DiagnosticoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diagnostico
        public async Task<IActionResult> Index()
        {
            var diagnosticos = await _context.Diagnostico
                .Include(d => d.Medico)
                .Include(d => d.Paciente)
                .ToListAsync();
            return View(diagnosticos);
        }

        // GET: Diagnostico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Medico)
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_diagnostico == id);

            if (diagnostico == null) return NotFound();

            var viewModel = new DiagnosticoViewModel
            {
                Codigo_diagnostico = diagnostico.Codigo_diagnostico,
                Codigo_medico = diagnostico.Codigo_medico,
                Codigo_paciente = diagnostico.Codigo_paciente,
                Valoracion_oftalmologica = diagnostico.Valoracion_oftalmologica,
                ResultadosExa_Glucosa = diagnostico.ResultadosExa_Glucosa,
                Valoracion_MedInterna = diagnostico.Valoracion_MedInterna,
                Valoracion_Anestesia = diagnostico.Valoracion_Anestesia,
                Fecha_diagnostico = diagnostico.Fecha_diagnostico,
                Notas_diagnostico = diagnostico.Notas_diagnostico,
                MedicoNombre = diagnostico.Medico?.NombreCompleto,
                PacienteNombre = diagnostico.Paciente?.NombreCompleto
            };

            return View(viewModel);
        }

        // GET: Diagnostico/Create
        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        // POST: Diagnostico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiagnosticoViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                CargarListas();
                return View(model);
            }
            try
            {
                var diagnostico = new Diagnostico
                {
                    Codigo_medico = model.Codigo_medico,
                    Codigo_paciente = model.Codigo_paciente,
                    Valoracion_oftalmologica = model.Valoracion_oftalmologica,
                    ResultadosExa_Glucosa = model.ResultadosExa_Glucosa,
                    Valoracion_MedInterna = model.Valoracion_MedInterna,
                    Valoracion_Anestesia = model.Valoracion_Anestesia,
                    Fecha_diagnostico = model.Fecha_diagnostico,
                    Notas_diagnostico = model.Notas_diagnostico
                };

                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.InnerException?.Message}");
                ModelState.AddModelError("", "No se pudo guardar. Verifica los datos.");
                CargarListas();
                return View(model);
            }
        }

        // GET: Diagnostico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico == null) return NotFound();

            var viewModel = new DiagnosticoViewModel
            {
                Codigo_diagnostico = diagnostico.Codigo_diagnostico,
                Codigo_medico = diagnostico.Codigo_medico,
                Codigo_paciente = diagnostico.Codigo_paciente,
                Valoracion_oftalmologica = diagnostico.Valoracion_oftalmologica,
                ResultadosExa_Glucosa = diagnostico.ResultadosExa_Glucosa,
                Valoracion_MedInterna = diagnostico.Valoracion_MedInterna,
                Valoracion_Anestesia = diagnostico.Valoracion_Anestesia,
                Fecha_diagnostico = diagnostico.Fecha_diagnostico,
                Notas_diagnostico = diagnostico.Notas_diagnostico
            };

            CargarListas();
            return View(viewModel);
        }

        // POST: Diagnostico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiagnosticoViewModel model)
        {
            if (id != model.Codigo_diagnostico) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var diagnostico = await _context.Diagnostico.FindAsync(id);
                    if (diagnostico == null) return NotFound();

                    diagnostico.Codigo_medico = model.Codigo_medico;
                    diagnostico.Codigo_paciente = model.Codigo_paciente;
                    diagnostico.Valoracion_oftalmologica = model.Valoracion_oftalmologica;
                    diagnostico.ResultadosExa_Glucosa = model.ResultadosExa_Glucosa;
                    diagnostico.Valoracion_MedInterna = model.Valoracion_MedInterna;
                    diagnostico.Valoracion_Anestesia = model.Valoracion_Anestesia;
                    diagnostico.Fecha_diagnostico = model.Fecha_diagnostico;
                    diagnostico.Notas_diagnostico = model.Notas_diagnostico;

                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(model.Codigo_diagnostico)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            CargarListas();
            return View(model);
        }

        // GET: Diagnostico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Medico)
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_diagnostico == id);

            if (diagnostico == null) return NotFound();

            return View(diagnostico);
        }

        // POST: Diagnostico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico != null) _context.Diagnostico.Remove(diagnostico);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnostico.Any(e => e.Codigo_diagnostico == id);
        }

        private void CargarListas()
        {
            ViewBag.Codigo_medico = new SelectList(_context.Medico, "Codigo_medico", "NombreCompleto");
            ViewBag.Codigo_paciente = new SelectList(_context.Paciente, "Codigo_paciente", "NombreCompleto");
        }
    }
}
