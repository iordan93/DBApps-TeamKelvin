using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReport.ConsoleClient
{
    public class ExcelExporter
    {
      public static void ExportToExcel()
      {
          string sqliteDbPath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\..\\");
          sqliteDbPath = Path.GetFullPath(sqliteDbPath);
          AppDomain.CurrentDomain.SetData("DataDirectory", sqliteDbPath);
          var context = new VendorTaxesDbContext();


      }
    }
}
