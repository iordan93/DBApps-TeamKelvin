using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Ionic.Zip;

namespace SalesReport.ConsoleClient.XLStoSQLSERVER
{
    public class ZipFileReader
    {
        public static void ReadZipFile(string filePath)
        {
            using (ZipFile zip = ZipFile.Read(filePath))
            {
                foreach (var file in zip)
                {
                    if (file.FileName.Contains(".xls"))
                    {
                        string pathInsideZip = file.FileName;
                        string fullPath = filePath.Replace("Sample-Sales-Reports.zip", "Sales") + '\\' + pathInsideZip.Replace('/', '\\');
                        file.Extract(@"D:\SoftUni\Course #3\DBApps\TeamWork\Database-Apps-Teamwork-Project\Sales\", ExtractExistingFileAction.OverwriteSilently);

                        XlsFileReader.ReadXls(fullPath);
                    }
                }
            }
        }

        public static void ReadDirectory(string filePath)
        {
            string[] subDirectories = Directory.GetDirectories(filePath);
            foreach (string subDir in subDirectories)
            {
                // Console.WriteLine(subDir);
                string[] xlsFiles = Directory.GetFiles(subDir);

                foreach (string xlsFile in xlsFiles)
                {
                    XlsFileReader.ReadXls(xlsFile);
                    // Console.WriteLine(xlsFile);
                }
            }
        }
    }
}
