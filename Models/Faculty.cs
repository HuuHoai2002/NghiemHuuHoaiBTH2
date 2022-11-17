using System.ComponentModel.DataAnnotations;

namespace NghiemHuuHoaiBTH2.Models;

public class Faculty
{
  [Key]
  public string FacultyID { get; set; }
  public string FacultyName { get; set; }
}