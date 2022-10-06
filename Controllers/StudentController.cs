using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class StudentController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public StudentController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    // get all student from database
    var model = await _context.Students.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  // action create student
  [HttpPost]
  public async Task<IActionResult> Create(Student student)
  {
    if (ModelState.IsValid)
    {
      // add student to database
      _context.Add(student);
      // save database
      await _context.SaveChangesAsync();
      // redirect to index action
      return RedirectToAction(nameof(Index));
    }
    return View(student);
  }
}

