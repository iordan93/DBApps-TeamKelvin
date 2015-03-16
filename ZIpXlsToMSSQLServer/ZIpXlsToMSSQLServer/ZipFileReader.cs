using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Ionic.Zip;

namespace ZipXlsToMSSQLServer
{
    public class ZipFileReader
    {
        public static void ReadZipFile(string filePath)
        {
            using (ZipFile zip = ZipFile.Read(filePath))
            {
                XlsFileReader.ReadXls(@"D:\SoftUni\Course #3\DBApps\TeamWork\Database-Apps-Teamwork-Project\" +
                    @"Sample-Sales-Reports\20-Jul-2014\Kaspichan-Center-Sales-Report-20-Jul-2014.xls");
                /*foreach (var file in zip)
                {
                    if (file.FileName.Contains(".xls"))
                    {
                        string pathInsideZip = file.FileName;
                        string fullPath = filePath + '\\' + pathInsideZip.Replace('/', '\\');

                        XlsFileReader.ReadXls(@"D:\SoftUni\Course #3\DBApps\TeamWork\Database-Apps-Teamwork-Project\Sample-Sales-Reports\20-Jul-2014\Bourgas-Plaza-Sales-Report-20-Jul-2014.xls");
                    }
                }*/
            }
        }

        public static void ReadDirecotory(string filePath)
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
