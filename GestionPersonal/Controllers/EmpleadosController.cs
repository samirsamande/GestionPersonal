using GestionPersonal.Data;
using GestionPersonal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionPersonal.Controllers;

public class EmpleadosController : Controller
{
    private readonly ApplicationDbContext _context;
    public EmpleadosController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var empleados = _context.Empleado.Include(e => e.Departamento);
        return View(await empleados.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var empleado = await _context.Empleado
            .Include(e => e.Departamento)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null) return NotFound();
        return View(empleado);
    }

    public IActionResult Create()
    {
        CargarDepartamentos();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Cargo,Salario,DepartamentoId")] Empleado empleado)
    {
        if (ModelState.IsValid)
        {
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarDepartamentos(empleado.DepartamentoId);
        return View(empleado);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var empleado = await _context.Empleado.FindAsync(id);
        if (empleado == null) return NotFound();
        CargarDepartamentos(empleado.DepartamentoId);
        return View(empleado);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cargo,Salario,DepartamentoId")] Empleado empleado)
    {
        if (id != empleado.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(empleado);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Empleado.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        CargarDepartamentos(empleado.DepartamentoId);
        return View(empleado);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var empleado = await _context.Empleado
            .Include(e => e.Departamento)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null) return NotFound();
        return View(empleado);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empleado = await _context.Empleado.FindAsync(id);
        if (empleado != null)
        {
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void CargarDepartamentos(int? seleccionado = null) =>
        ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "Id", "Nombre", seleccionado);
}