using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSData;
using OracleData; 

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var oracle = new OrclEntities();
            var mssql = new MSEntities();

            
            var measuresOR = oracle.MEASURES;
            var measuresMS = mssql.MEASURES;

            foreach (var msrOR in measuresOR)
            {
                var isDuplicate = false;
                foreach (var msrMS in measuresMS)
                {
                    if (msrOR.MEASURES_NAME == msrMS.MEASURES_NAME && msrOR.ID == msrMS.ID)
                    {
                        isDuplicate = true;
                        //Console.WriteLine(msrMS.MEASURES_NAME);
                    }
                }

                if (isDuplicate == false)
                {
                    measuresMS.Add(new MSData.MEASURES
                    {
                        ID = msrOR.ID,
                        MEASURES_NAME = msrOR.MEASURES_NAME,
                    });
                    Console.WriteLine(msrOR.MEASURES_NAME + " Added to MSSQL Database!");
                }
            }

            mssql.SaveChanges();


            var vendorsOR = oracle.VENDORS;
            var vendorsMS = mssql.VENDORS;

            foreach (var vendOR in vendorsOR)
            {
                var isDuplicate = false;
                foreach (var vendMS in vendorsMS)
                {
                    if (vendOR.VENDOR_NAME == vendMS.VENDOR_NAME && vendOR.ID == vendMS.ID)
                    {
                        isDuplicate = true;
                        //Console.WriteLine("duplicate found!");
                    }
                }

                if (isDuplicate == false)
                {
                    vendorsMS.Add(new MSData.VENDORS
                    {
                        ID = vendOR.ID,
                        VENDOR_NAME = vendOR.VENDOR_NAME,

                    });
                    Console.WriteLine(vendOR.VENDOR_NAME + " Added to MSSQL Database!");
                }
            }

            mssql.SaveChanges();


            var productsOR = oracle.PRODUCTS;
            var productsMS = mssql.PRODUCTS;

            foreach (var prodOR in productsOR)
            {
                var isDuplicate = false;
                foreach (var prodMS in productsMS)
                {
                    if (prodOR.PRODUCT_NAME == prodMS.PRODUCT_NAME && prodOR.PRICE == prodMS.PRICE)
                    {
                        isDuplicate = true;
                        //Console.WriteLine("duplicate found!");
                    }
                }

                if (isDuplicate == false)
                {
                    productsMS.Add(new MSData.PRODUCTS
                    {
                        ID = prodOR.ID,
                        PRODUCT_NAME = prodOR.PRODUCT_NAME,
                        VENDOR_ID = prodOR.VENDOR_ID,
                        MEASURE_ID = prodOR.MEASURE_ID,
                        PRICE = prodOR.PRICE
                    });
                    Console.WriteLine(prodOR.PRODUCT_NAME + " Added to MSSQL Database!");
                }
            }


            mssql.SaveChanges();           
        }
    }
}
