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


  [HttpGet]
  public async Task<IActionResult> Edit(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var person = await _context.Persons.FindAsync(id);
    if (person == null)
    {
      return NotFound();
    }
    return View(person);
  }
  [HttpPost]
  public async Task<IActionResult> Edit(string id, [Bind("PersonID, PersonName")] Person person)
  {
    if (id != person.PersonID)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      try
      {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonExists(person.PersonID))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
    }
    return View(person);
  }
  public async Task<IActionResult> Delete(string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var person = await _context.Persons.FirstOrDefaultAsync(p => p.PersonID == id);
    if (person == null)
    {
      return NotFound();
    }

    return View(person);
  }
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(string id)
  {
    var person = await _context.Persons.FindAsync(id);
    if (person != null)
    {
      _context.Persons.Remove(person);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }

  private bool PersonExists(string id) => _context.Persons.Any(e => e.PersonID == id);
}

