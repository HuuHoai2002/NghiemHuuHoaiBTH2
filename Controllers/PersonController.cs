using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class PersonController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public PersonController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    // get all student from database
    var model = await _context.Persons.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  // action create student
  [HttpPost]
  public async Task<IActionResult> Create(Person person)
  {
    if (ModelState.IsValid)
    {
      // add person to database
      _context.Add(person);
      // save database
      await _context.SaveChangesAsync();
      // redirect to index action
      return RedirectToAction(nameof(Index));
    }
    return View(person);
  }
}

