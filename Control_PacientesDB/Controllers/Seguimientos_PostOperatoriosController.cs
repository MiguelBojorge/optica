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
            ViewData["Codigo_Medicamento"] = new SelectList(_context.Medicamentos, "Codigo_Medicamento", "Nombre_Medicamento");
            ViewData["Codigo_medico"] = new SelectList(_context.Medico.Select(m => new {
                m.Codigo_medico,
                NombreCompleto = m.Nombres + " " + m.Apellidos
            }), "Codigo_medico", "NombreCompleto");
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente.Select(p => new {
                p.Codigo_paciente,
                NombreCompleto = p.Nombres + " " + p.Apellidos
            }), "Codigo_paciente", "NombreCompleto");
            return View();
        }

        // POST: Seguimientos_PostOperatorios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo_seguimiento,Codigo_medico,Codigo_paciente,Codigo_Medicamento,Fecha_Control,Programacion_ProximaCita,Observaciones")] Seguimientos_PostOperatorios seguimientos_PostOperatorios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguimientos_PostOperatorios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigo_Medicamento"] = new SelectList(_context.Medicamentos, "Codigo_Medicamento", "Nombre_Medicamento", seguimientos_PostOperatorios.Codigo_Medicamento);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", seguimientos_PostOperatorios.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", seguimientos_PostOperatorios.Codigo_paciente);
            return View(seguimientos_PostOperatorios);
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
            ViewData["Codigo_Medicamento"] = new SelectList(_context.Medicamentos, "Codigo_Medicamento", "Nombre_Medicamento", seguimientos_PostOperatorios.Codigo_Medicamento);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", seguimientos_PostOperatorios.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", seguimientos_PostOperatorios.Codigo_paciente);
            return View(seguimientos_PostOperatorios);
        }

        // POST: Seguimientos_PostOperatorios/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo_seguimiento,Codigo_medico,Codigo_paciente,Codigo_Medicamento,Fecha_Control,Programacion_ProximaCita,Observaciones")] Seguimientos_PostOperatorios seguimientos_PostOperatorios)
        {
            if (id != seguimientos_PostOperatorios.Codigo_seguimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguimientos_PostOperatorios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Seguimientos_PostOperatoriosExists(seguimientos_PostOperatorios.Codigo_seguimiento))
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
            ViewData["Codigo_Medicamento"] = new SelectList(_context.Medicamentos, "Codigo_Medicamento", "Nombre_Medicamento", seguimientos_PostOperatorios.Codigo_Medicamento);
            ViewData["Codigo_medico"] = new SelectList(_context.Medico, "Codigo_medico", "Apellidos", seguimientos_PostOperatorios.Codigo_medico);
            ViewData["Codigo_paciente"] = new SelectList(_context.Paciente, "Codigo_paciente", "Apellidos", seguimientos_PostOperatorios.Codigo_paciente);
            return View(seguimientos_PostOperatorios);
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
    }
}
