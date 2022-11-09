using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;
using NghiemHuuHoaiBTH2.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class StudentController : Controller
{
  private readonly ApplicationDbContext _context;
  private ExcelProcess _excelProcess = new ExcelProcess();

  public StudentController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var model = await _context.Students.ToListAsync();
    return View(model);
  }
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> Create(Student student)
  {
    if (ModelState.IsValid)
    {
      _context.Students.Add(student);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(student);
  }

  [HttpGet]
  public IActionResult Upload()
  {
    return View();
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Upload(IFormFile file)
  {
    if (file != null)
    {
      var fileExtension = Path.GetExtension(file.FileName);
      if (fileExtension != ".xls" && fileExtension != ".xlsx")
      {
        ViewBag.Message = "This file format is not supported";
        return View();
      }
      else
      {
        var fileName = DateTime.Now.ToBinary() + fileExtension;
        var filePath = Path.Combine(Directory.GetCurrentDirectory() + "Uploads/Excels", fileName);
        var fileLocation = new FileInfo(filePath).ToString();

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(stream);

          var dataTable = _excelProcess.ExcelToDataTable(fileLocation);
          for (int i = 0; i < dataTable.Rows.Count; i++)
          {
            var student = new Student();
            student.StudentID = dataTable.Rows[0][0].ToString();
            student.StudentName = dataTable.Rows[0][1].ToString();
            student.Address = dataTable.Rows[0][2].ToString();

            _context.Students.Add(student);
          }
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
        }
      }
    }
    return View();
  }

  [HttpGet]
  public async Task<IActionResult> Edit(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var student = await _context.Students.FindAsync(id);
    if (student == null)
    {
      return NotFound();
    }
    return View(student);
  }
  [HttpPost]
  public async Task<IActionResult> Edit(string id, [Bind("StudentID, StudentName")] Student student)
  {
    if (id != student.StudentID)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      try
      {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StudentExists(student.StudentID))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
    }
    return View(student);
  }
  public async Task<IActionResult> Delete(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentID == id);
    if (student == null)
    {
      return NotFound();
    }

    return View(student);
  }
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(string id)
  {
    var student = await _context.Students.FindAsync(id);
    if (student != null)
    {
      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }

  private bool StudentExists(string id) => _context.Students.Any(e => e.StudentID == id);
}

