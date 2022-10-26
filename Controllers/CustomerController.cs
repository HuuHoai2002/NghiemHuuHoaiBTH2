using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class CustomerController : Controller
{
  private readonly ApplicationDbContext _context;
  public CustomerController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var model = await _context.Customers.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> Create(Customer customer)
  {
    if (ModelState.IsValid)
    {
      _context.Customers.Add(customer);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(customer);
  }

  public async Task<IActionResult> Edit(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var customer = await _context.Customers.FindAsync(id);
    if (customer == null)
    {
      return NotFound();
    }
    return View(customer);
  }
  [HttpPost]
  public async Task<IActionResult> Edit(string id, [Bind("CustomerID, CustomerName")] Customer customer)
  {
    if (id != customer.CustomerID)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      try
      {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CustomerExists(customer.CustomerID))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
    }
    return View(customer);
  }
  public async Task<IActionResult> Delete(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
    if (customer == null)
    {
      return NotFound();
    }

    return View(customer);
  }
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(string id)
  {
    var customer = await _context.Customers.FindAsync(id);
    if (customer != null)
    {
      _context.Customers.Remove(customer);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }

  private bool CustomerExists(string id) => _context.Students.Any(e => e.StudentID == id);
}

