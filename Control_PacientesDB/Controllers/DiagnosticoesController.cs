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
    public class DiagnosticoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diagnosticoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diagnostico.Include(d => d.Medico).Include(d => d.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Diagnosticoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Medico)
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_diagnostico == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticoes/Create
        public IActionResult Create()
        {
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos");
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos");
            return View();
        }

        // POST: Diagnosticoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo_diagnostico,Codigo_medico,Codigo_paciente,Valoracion_oftalmologica,ResultadosExa_Glucosa,Valoracion_MedInterna,Valoracion_Anestesia,Fecha_diagnostico,Notas_diagnostico")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", diagnostico.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", diagnostico.Codigo_paciente);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", diagnostico.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", diagnostico.Codigo_paciente);
            return View(diagnostico);
        }

        // POST: Diagnosticoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo_diagnostico,Codigo_medico,Codigo_paciente,Valoracion_oftalmologica,ResultadosExa_Glucosa,Valoracion_MedInterna,Valoracion_Anestesia,Fecha_diagnostico,Notas_diagnostico")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Codigo_diagnostico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Codigo_diagnostico))
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
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", diagnostico.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", diagnostico.Codigo_paciente);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Medico)
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo_diagnostico == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnostico.Remove(diagnostico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnostico.Any(e => e.Codigo_diagnostico == id);
        }
    }
}
