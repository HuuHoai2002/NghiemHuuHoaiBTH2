using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class PersonController : Controller
{
  private readonly ApplicationDbContext _context;
  public PersonController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var model = await _context.Persons.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> Create(Person person)
  {
    if (ModelState.IsValid)
    {
      _context.Persons.Add(person);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(person);
  }
}

