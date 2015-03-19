using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipXlsToMSSQLServer
{
    class Program
    {
        static void Main()
        {
            //string filePath = @"D:\SoftUni\Course #3\DBApps\TeamWork\Database-Apps-Teamwork-Project\Sample-Sales-Reports";
            string filePath = @"C:\Users\pc\Desktop\Sample-Sales-Reports.zip";
            ZipFileReader.ReadZipFile(filePath);
            // ZipFileReader.ReadDirectory(filePath);
        }
    }
}
