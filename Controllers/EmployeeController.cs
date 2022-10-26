using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class EmployeeController : Controller
{
  private readonly ApplicationDbContext _context;
  public EmployeeController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var model = await _context.Employees.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> Create(Employee employee)
  {
    if (ModelState.IsValid)
    {
      _context.Employees.Add(employee);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(employee);
  }

  public async Task<IActionResult> Edit(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
      return NotFound();
    }
    return View(employee);
  }
  [HttpPost]
  public async Task<IActionResult> Edit(string id, [Bind("EmployeeID, EmployeeName")] Employee employee)
  {
    if (id != employee.EmployeeID)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      try
      {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!EmployeeExists(employee.EmployeeID))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
    }
    return View(employee);
  }
  public async Task<IActionResult> Delete(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
    if (employee == null)
    {
      return NotFound();
    }

    return View(employee);
  }
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(string id)
  {
    var employee = await _context.Employees.FindAsync(id);
    if (employee != null)
    {
      _context.Employees.Remove(employee);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }

  private bool EmployeeExists(string id) => _context.Employees.Any(e => e.EmployeeID == id);
}

