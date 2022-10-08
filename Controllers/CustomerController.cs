using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class CustomerController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public CustomerController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    // get all student from database
    var model = await _context.Customers.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  // action create student
  [HttpPost]
  public async Task<IActionResult> Create(Customer customer)
  {
    if (ModelState.IsValid)
    {
      // add customer to database
      _context.Add(customer);
      // save database
      await _context.SaveChangesAsync();
      // redirect to index action
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

