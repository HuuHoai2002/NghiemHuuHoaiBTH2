using System.Data;
using OfficeOpenXml;

namespace NghiemHuuHoaiBTH2.Models.Process;

public class ExcelProcess
{
  public DataTable ExcelToDataTable(string path)
  {
    FileInfo file = new FileInfo(path);
    ExcelPackage excelPackage = new ExcelPackage(file);
    DataTable dataTable = new DataTable();
    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];

    if (excelWorksheet.Dimension == null)
    {
      return dataTable;
    }
    List<string> columnNames = new List<string>();

    int currentColumn = 1;

    foreach (var cell in excelWorksheet.Cells[1, 1, 1, excelWorksheet.Dimension.End.Column])
    {
      string columnName = cell.Text.Trim();

      if (cell.Start.Column != currentColumn)
      {
        columnNames.Add("Header_" + currentColumn);
        dataTable.Columns.Add("Header_" + currentColumn);
        currentColumn++;
      }

      columnNames.Add(columnName);

      int occurences = columnNames.Count(x => x.Equals(columnName));
      if (occurences > 1)
      {
        columnName += "_" + occurences;
      }
      dataTable.Columns.Add(columnName);
      currentColumn++;
    }

    for (int i = 2; i <= excelWorksheet.Dimension.End.Row; i++)
    {
      var row = excelWorksheet.Cells[i, 1, i, excelWorksheet.Dimension.End.Column];
      DataRow newRow = dataTable.NewRow();
      foreach (var cell in row)
      {
        newRow[cell.Start.Column - 1] = cell.Text;
      }
      dataTable.Rows.Add(newRow);
    }
    return dataTable;
  }
}