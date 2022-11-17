using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;
using NghiemHuuHoaiBTH2.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NghiemHuuHoaiBTH2.Controllers;

public class StudentController : Controller
{
  private readonly ApplicationDbContext _context;
  private ExcelProcess _excelProcess = new ExcelProcess();

  public StudentController(ApplicationDbContext context)
  {
    _context = context;
  }
  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var model = await _context.Students.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName");

    return View();
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("StudentID, StudentName, Address, FacultyID")] Student student)
  {
    if (ModelState.IsValid)
    {
      _context.Students.Add(student);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", student.FacultyID);

    return View(student);
  }
}

