using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesReport.Data;
using MySql.Data;
using System.Data.Entity;
using SalesReport.Data.Migrations;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
namespace MySqlXlsReport
{

    class Program
    {
        static void Main(string[] args)
        {
            XlsReport();
            //MysqlConnect();
        }
        public static void MysqlConnect()
        {


            try
            {
                var conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = "server=127.0.0.1;uid=root;pwd=vertrigo;database=salesreport;";
                conn.Open();

                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesReportDBContext, Configuration>());

                SalesReportDBContext db = new SalesReportDBContext();

                var productReportCollection = db.Products;


                var a = productReportCollection.Select(p => new
                {
                    ProduckName = p.Name,
                    VendorName = db.Vendors.Where(x => x.Id == p.VendorId).Select(x => x.Name).FirstOrDefault(),
                    Incomes = db.Sales.Where(x => x.Id == p.VendorId).Select(x => x.Quantity == null ? 0m : x.Quantity).Sum() * p.Price,
                    Expenses = 0m
                    //Expenses = db.Expenses.Where(x => x.VendorName == db.Vendors.Where(y => y.Id == p.VendorId).Select(y => y.Name).FirstOrDefault())
                    //.Select(x => x.Sum == null ? 0m : x.Sum).Sum()
                });
                

                foreach (var item in a)
                {
                    var comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO `ExpensesAndIncomes`(`Product`, `Vendor`, `Income`, `Expense`) VALUES (?product, ?vendor, ?in, ?ex)";
                    comm.Parameters.AddWithValue("?product", item.ProduckName);
                    comm.Parameters.AddWithValue("?vendor", item.VendorName);
                    comm.Parameters.AddWithValue("?in", item.Incomes);
                    comm.Parameters.AddWithValue("?ex", item.Expenses);
                    comm.ExecuteNonQuery();

                }

                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }
        }
        public static void XlsReport()
        {
            var conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = "server=127.0.0.1;uid=root;pwd=vertrigo;database=salesreport;";
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM  `ExpensesAndIncomes`";
         
            MySqlDataReader reader = cmd.ExecuteReader();
           
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;

            _Workbook workbook = (_Workbook)(excelapp.Workbooks.Add(Type.Missing));
            _Worksheet worksheet = (_Worksheet)workbook.ActiveSheet;

            worksheet.Cells[1, 1] = "Product";
            worksheet.Cells[1, 2] = "Vendor";
            worksheet.Cells[1, 3] = "Income";
            worksheet.Cells[1, 4] = "Expense";
            var i = 2;
            while (reader.Read())
            {
                worksheet.Cells[i, 1] = reader.GetString(0);
                worksheet.Cells[i, 2] = reader.GetString(1);
                worksheet.Cells[i, 3] = reader.GetDecimal(2);
                worksheet.Cells[i, 4] = reader.GetDecimal(3);
                i++;
            }
            
            excelapp.UserControl = true;
        }
    }
}
