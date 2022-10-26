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

  // public async Task<IActionResult> Edit(int? id)
  // {
  //   if (id == null)
  //   {
  //     return NotFound();
  //   }
  //   var customer = await _context.Customers.FindAsync(id);
  //   if (customer == null)
  //   {
  //     return NotFound();
  //   }
  //   return View(customer);
  // }
}

