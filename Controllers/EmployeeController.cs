using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class EmployeeController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public EmployeeController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    // get all student from database
    var model = await _context.Employees.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  // action create student
  [HttpPost]
  public async Task<IActionResult> Create(Employee employee)
  {
    if (ModelState.IsValid)
    {
      // add employee to database
      _context.Add(employee);
      // save database
      await _context.SaveChangesAsync();
      // redirect to index action
      return RedirectToAction(nameof(Index));
    }
    return View(employee);
  }
}

