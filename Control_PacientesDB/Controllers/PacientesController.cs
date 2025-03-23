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
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el paciente, dirección y teléfono asociados
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccion.FindAsync(paciente.Id_Direccion);
            var telefono = await _context.Telefono.FirstOrDefaultAsync(t => t.Codigo_paciente == paciente.Codigo_paciente);

            // Crear y poblar el ViewModel con los datos obtenidos
            var viewModel = new PacienteViewModel
            {
                // Datos de Paciente
                Codigo_paciente = paciente.Codigo_paciente,
                Nombres = paciente.Nombres,
                Apellidos = paciente.Apellidos,
                Cedula = paciente.Cedula,
                FechaNac = paciente.FechaNac,

                // Datos de Direccion
                Id_Direccion = direccion?.Id_Direccion ?? 0,
                Direccion_Domicilio = direccion?.Direccion_Domicilio,
                Ciudad = direccion?.Ciudad,
                Departamentos = direccion?.Departamentos,

                // Datos de Telefono
                ID_Telefono = telefono?.ID_Telefono ?? 0,
                Num_Telefono = telefono?.Num_Telefono,
                Company = telefono?.Company
            };

            return View(viewModel);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Guardar Direccion
                var direccion = new Direccion
                {
                    Id_Direccion = model.Id_Direccion,
                    Direccion_Domicilio = model.Direccion_Domicilio,
                    Ciudad = model.Ciudad,
                    Departamentos = model.Departamentos
                };
                _context.Direccion.Add(direccion);
                await _context.SaveChangesAsync();

                // 2. Guardar Paciente con Id_Direccion
                var paciente = new Paciente
                {
                    Codigo_paciente = model.Codigo_paciente,
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    Cedula = model.Cedula,
                    FechaNac = model.FechaNac,
                    Id_Direccion = direccion.Id_Direccion // Relacionar con la dirección recién creada
                };
                _context.Paciente.Add(paciente);
                await _context.SaveChangesAsync();

                // 3. Guardar Telefono con Codigo_paciente
                var telefono = new Telefono
                {
                    ID_Telefono = model.ID_Telefono,
                    Num_Telefono = model.Num_Telefono,
                    Company = model.Company,
                    Codigo_paciente = paciente.Codigo_paciente // Relacionar con el paciente recién creado
                };
                _context.Telefono.Add(telefono);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el paciente, dirección y teléfono asociados
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccion.FindAsync(paciente.Id_Direccion);
            var telefono = await _context.Telefono.FirstOrDefaultAsync(t => t.Codigo_paciente == paciente.Codigo_paciente);

            // Crear y poblar el ViewModel con los datos obtenidos
            var viewModel = new PacienteViewModel
            {
                // Datos de Paciente
                Codigo_paciente = paciente.Codigo_paciente,
                Nombres = paciente.Nombres,
                Apellidos = paciente.Apellidos,
                Cedula = paciente.Cedula,
                FechaNac = paciente.FechaNac,

                // Datos de Direccion
                Id_Direccion = direccion?.Id_Direccion ?? 0,
                Direccion_Domicilio = direccion?.Direccion_Domicilio,
                Ciudad = direccion?.Ciudad,
                Departamentos = direccion?.Departamentos,

                // Datos de Telefono
                ID_Telefono = telefono?.ID_Telefono ?? 0,
                Num_Telefono = telefono?.Num_Telefono,
                Company = telefono?.Company
            };

            return View(viewModel);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PacienteViewModel viewModel)
        {
            if (id != viewModel.Codigo_paciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar Paciente
                    var paciente = await _context.Paciente.FindAsync(id);
                    if (paciente != null)
                    {
                        paciente.Codigo_paciente = viewModel.Codigo_paciente;
                        paciente.Nombres = viewModel.Nombres;
                        paciente.Apellidos = viewModel.Apellidos;
                        paciente.Cedula = viewModel.Cedula;
                        paciente.FechaNac = viewModel.FechaNac;
                        _context.Update(paciente);
                    }

                    // Actualizar Direccion
                    var direccion = await _context.Direccion.FindAsync(paciente.Id_Direccion);
                    if (direccion != null)
                    {
                        direccion.Id_Direccion = viewModel.Id_Direccion;
                        direccion.Direccion_Domicilio = viewModel.Direccion_Domicilio;
                        direccion.Ciudad = viewModel.Ciudad;
                        direccion.Departamentos = viewModel.Departamentos;
                        _context.Update(direccion);
                    }

                    // Actualizar Telefono
                    var telefono = await _context.Telefono.FirstOrDefaultAsync(t => t.Codigo_paciente == id);
                    if (telefono != null)
                    {
                        telefono.ID_Telefono = viewModel.ID_Telefono;
                        telefono.Num_Telefono = viewModel.Num_Telefono;
                        telefono.Company = viewModel.Company;
                        _context.Update(telefono);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(viewModel.Codigo_paciente))
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
            return View(viewModel);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Codigo_paciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.Codigo_paciente == id);
        }
    }
}
