using NghiemHuuHoaiBTH2.Data;
using NghiemHuuHoaiBTH2.Models;
using NghiemHuuHoaiBTH2.Models.Process;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Controllers;

public class CustomerController : Controller
{
  private readonly ApplicationDbContext _context;
  private ExcelProcess _excelProcess = new ExcelProcess();

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
            var customer = new Customer();
            customer.CustomerID = dataTable.Rows[0][0].ToString();
            customer.CustomerName = dataTable.Rows[0][1].ToString();
            customer.Address = dataTable.Rows[0][2].ToString();

            _context.Customers.Add(customer);
          }
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
        }
      }
    }
    return View();
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

