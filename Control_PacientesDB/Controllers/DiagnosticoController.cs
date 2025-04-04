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
            Console.WriteLine($"Datos recibidos - ID: {id}, Codigo_medico: {model.Codigo_medico}, Valoracion: {model.Valoracion_oftalmologica}");

            if (id != model.Codigo_diagnostico)
            {
                Console.WriteLine("Error: ID no coincide");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
                CargarListas();
                return View(model);
            }
            try
            {
                // 1. Obtener la entidad existente SIN TRACKING
                var diagnosticoExistente = await _context.Diagnostico
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Codigo_diagnostico == id);

                if (diagnosticoExistente == null)
                {
                    Console.WriteLine("Diagnóstico no encontrado");
                    return NotFound();
                }

                // 2. Crear nueva instancia con los datos actualizados
                var diagnosticoActualizado = new Diagnostico
                {
                    Codigo_diagnostico = id,
                    Codigo_medico = model.Codigo_medico,
                    Codigo_paciente = model.Codigo_paciente,
                    Valoracion_oftalmologica = model.Valoracion_oftalmologica,
                    ResultadosExa_Glucosa = model.ResultadosExa_Glucosa,
                    Valoracion_MedInterna = model.Valoracion_MedInterna,
                    Valoracion_Anestesia = model.Valoracion_Anestesia,
                    Fecha_diagnostico = model.Fecha_diagnostico,
                    Notas_diagnostico = model.Notas_diagnostico
                };
                var cambios = _context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified)
                    .ToList();

                Console.WriteLine($"Entidades a actualizar: {cambios.Count}");
                // 3. Adjuntar y marcar como modificado
                _context.Attach(diagnosticoActualizado);
                _context.Entry(diagnosticoActualizado).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 4. Manejar errores de concurrencia
                if (!DiagnosticoExists(id))
                    return NotFound();

                ModelState.AddModelError("", "El registro fue modificado por otro usuario. Por favor refresque los datos.");
                Console.WriteLine($"Error de concurrencia: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.ToString()}");
                ModelState.AddModelError("", "Ocurrió un error al guardar. Intente nuevamente.");
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
