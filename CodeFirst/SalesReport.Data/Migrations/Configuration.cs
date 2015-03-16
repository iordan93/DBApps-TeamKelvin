namespace SalesReport.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SalesReport.Models;
    using System.Collections.Generic;

    using EfEnumToLookup.LookupGenerator;

    public sealed class Configuration : DbMigrationsConfiguration<SalesReportDBContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesReportDBContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var enumToLookup = new EnumToLookup();
            enumToLookup.NameFieldLength = 42; // optional, example of how to override default values
            enumToLookup.Apply(context);

            var beerZagorka = new Product()
            {
                Name = "Beer \"Zagorka\"",
                MeasureType = MeasureType.ml,
                Price = 1.99m
            };

            context.Products.AddOrUpdate(beerZagorka);

            var vodkaTargovishte = new Product()
            {
                Name = "Vodka \"Targovishte\"",
                MeasureType = MeasureType.ml,
                Price = 8.49m
            };

            context.Products.AddOrUpdate(vodkaTargovishte);

            var beerBecks = new Product()
            {
                Name = "Beer \"Beck's\"",
                MeasureType = MeasureType.ml,
                Price = 8.49m
            };

            context.Products.AddOrUpdate(beerBecks);

            var chockolateMilka = new Product()
            {
                Name = "Chocolate \"Milka\"",
                MeasureType = MeasureType.gr,
                Price = 2.99m
            };

            context.Products.AddOrUpdate(chockolateMilka);

            var vodaDevin = new Product()
            {
                Name = "Mineralna Voda \"Devin\"",
                MeasureType = MeasureType.liters,
                Price = 3.49m
            };

            context.Products.AddOrUpdate(vodaDevin);

            var svinskaParjola = new Product()
            {
                Name = "Svinska Parjola",
                MeasureType = MeasureType.kg,
                Price = 5.99m
            };

            context.Products.AddOrUpdate(svinskaParjola);

            var pileshkaParjola = new Product()
            {
                Name = "Pileska Parjola",
                MeasureType = MeasureType.kg,
                Price = 4.99m
            };

            context.Products.AddOrUpdate(pileshkaParjola);

            var kinderSurprise = new Product()
            {
                Name = "Kinder Surprise",
                MeasureType = MeasureType.pieces,
                Price = 1.59m
            };

            context.Products.AddOrUpdate(kinderSurprise);

            var mlqkoRodopeq = new Product()
            {
                Name = "Kiselo mlqko \"Rodopeq\"",
                MeasureType = MeasureType.ml,
                Price = 1.59m
            };

            context.Products.AddOrUpdate(mlqkoRodopeq);

            context.Products.Add(beerZagorka);
            context.Products.Add(vodkaTargovishte);
            context.Products.Add(beerBecks);
            context.Products.Add(chockolateMilka);
            context.Products.Add(vodaDevin);
            context.Products.Add(svinskaParjola);
            context.Products.Add(pileshkaParjola);
            context.Products.Add(kinderSurprise);
            context.Products.Add(mlqkoRodopeq);

            var zagorkaCorp = new Vendor()
            {
                Name = "Zagorka Corp."
            };

            context.Vendors.AddOrUpdate(zagorkaCorp);

            var targovishteCorp = new Vendor()
            {
                Name = "Targovishte Bottling Company Ltd."
            };

            context.Vendors.AddOrUpdate(targovishteCorp);

            var becksCorp = new Vendor()
            {
                Name = "Brauerei Beck & Co."
            };

            context.Vendors.AddOrUpdate(becksCorp);

            var kraftFoods = new Vendor()
            {
                Name = "Kraft Foods"
            };

            context.Vendors.AddOrUpdate(kraftFoods);

            var bulgarianFood = new Vendor()
            {
                Name = "Bulgarian Food"
            };

            context.Vendors.AddOrUpdate(bulgarianFood);

            var kinder = new Vendor()
            {
                Name = "Kinder Corp."
            };

            context.Vendors.AddOrUpdate(kinder);

            context.Vendors.Add(zagorkaCorp);
            context.Vendors.Add(targovishteCorp);
            context.Vendors.Add(becksCorp);
            context.Vendors.Add(kraftFoods);
            context.Vendors.Add(bulgarianFood);

            beerZagorka.Vendor = zagorkaCorp;
            vodkaTargovishte.Vendor = targovishteCorp;
            beerBecks.Vendor = becksCorp;
            chockolateMilka.Vendor = kraftFoods;
            vodaDevin.Vendor = bulgarianFood;
            svinskaParjola.Vendor = bulgarianFood;
            pileshkaParjola.Vendor = bulgarianFood;
            kinderSurprise.Vendor = kinder;
            mlqkoRodopeq.Vendor = bulgarianFood;

            var zagorkaOrder1 = new Sale()
            {
                Quantity = 30,
                Date = DateTime.Now.AddDays(5),
                Product = beerZagorka
            };

            context.Sales.AddOrUpdate(zagorkaOrder1);

            var zagorkaOrder2 = new Sale()
            {
                Quantity = 1,
                Date = DateTime.Now.AddDays(3),
                Product = beerZagorka
            };

            context.Sales.AddOrUpdate(zagorkaOrder2);

            var zagorkaOrder3 = new Sale()
            {
                Quantity = 56,
                Date = DateTime.Now.AddDays(4),
                Product = beerZagorka
            };

            context.Sales.AddOrUpdate(zagorkaOrder3);

            var zagorkaOrder4 = new Sale()
            {
                Quantity = 12,
                Date = DateTime.Now.AddDays(3),
                Product = beerZagorka
            };

            context.Sales.AddOrUpdate(zagorkaOrder4);

            var vodkaOrder1 = new Sale()
            {
                Quantity = 2,
                Date = DateTime.Now.AddDays(4),
                Product = vodkaTargovishte
            };

            context.Sales.AddOrUpdate(vodkaOrder1);

            var vodkaOrder2 = new Sale()
            {
                Quantity = 20,
                Date = DateTime.Now.AddDays(5),
                Product = vodkaTargovishte
            };

            context.Sales.AddOrUpdate(vodkaOrder2);

            var vodkaOrder3 = new Sale()
            {
                Quantity = 13,
                Date = DateTime.Now.AddDays(5),
                Product = vodkaTargovishte
            };

            context.Sales.AddOrUpdate(vodkaOrder3);

            var vodkaOrder4 = new Sale()
            {
                Quantity = 30,
                Date = DateTime.Now.AddDays(3),
                Product = vodkaTargovishte
            };

            context.Sales.AddOrUpdate(vodkaOrder4);

            var becksOrder1 = new Sale()
            {
                Quantity = 50,
                Date = DateTime.Now.AddDays(4),
                Product = beerBecks
            };

            context.Sales.AddOrUpdate(becksOrder1);

            var becksOrder2 = new Sale()
            {
                Quantity = 5,
                Date = DateTime.Now.AddDays(5),
                Product = beerBecks
            };

            context.Sales.AddOrUpdate(becksOrder2);

            var becksOrder3 = new Sale()
            {
                Quantity = 13,
                Date = DateTime.Now.AddDays(2),
                Product = beerBecks
            };

            context.Sales.AddOrUpdate(becksOrder3);

            var becksOrder4 = new Sale()
            {
                Quantity = 35,
                Date = DateTime.Now.AddDays(4),
                Product = beerBecks
            };

            context.Sales.AddOrUpdate(becksOrder4);

            var milkaOrder1 = new Sale()
            {
                Quantity = 35,
                Date = DateTime.Now.AddDays(5),
                Product = chockolateMilka
            };

            context.Sales.AddOrUpdate(milkaOrder1);

            var milkaOrder2 = new Sale()
            {
                Quantity = 50,
                Date = DateTime.Now.AddDays(3),
                Product = chockolateMilka
            };

            context.Sales.AddOrUpdate(milkaOrder2);

            var milkaOrder3 = new Sale()
            {
                Quantity = 5,
                Date = DateTime.Now.AddDays(4),
                Product = chockolateMilka
            };

            context.Sales.AddOrUpdate(milkaOrder3);

            var milkaOrder4 = new Sale()
            {
                Quantity = 1,
                Date = DateTime.Now.AddDays(3),
                Product = chockolateMilka
            };

            context.Sales.AddOrUpdate(milkaOrder4);

            var devinOrder1 = new Sale()
            {
                Quantity = 500,
                Date = DateTime.Now.AddDays(5),
                Product = vodaDevin
            };

            context.Sales.AddOrUpdate(devinOrder1);

            var devinOrder2 = new Sale()
            {
                Quantity = 5,
                Date = DateTime.Now.AddDays(4),
                Product = vodaDevin
            };

            context.Sales.AddOrUpdate(devinOrder2);

            var devinOrder3 = new Sale()
            {
                Quantity = 1,
                Date = DateTime.Now.AddDays(3),
                Product = vodaDevin
            };

            context.Sales.AddOrUpdate(devinOrder3);

            var devinOrder4 = new Sale()
            {
                Quantity = 12,
                Date = DateTime.Now.AddDays(4),
                Product = vodaDevin
            };

            context.Sales.AddOrUpdate(devinOrder4);

            var svParjolaOrder1 = new Sale()
            {
                Quantity = 15,
                Date = DateTime.Now.AddDays(5),
                Product = svinskaParjola
            };

            context.Sales.AddOrUpdate(svParjolaOrder1);

            var svParjolaOrder2 = new Sale()
            {
                Quantity = 11,
                Date = DateTime.Now.AddDays(4),
                Product = svinskaParjola
            };

            context.Sales.AddOrUpdate(svParjolaOrder2);

            var svParjolaOrder3 = new Sale()
            {
                Quantity = 30,
                Date = DateTime.Now.AddDays(3),
                Product = svinskaParjola
            };

            context.Sales.AddOrUpdate(svParjolaOrder3);

            var svParjolaOrder4 = new Sale()
            {
                Quantity = 31,
                Date = DateTime.Now.AddDays(2),
                Product = svinskaParjola
            };

            context.Sales.AddOrUpdate(svParjolaOrder4);

            var plParjolaOrder1 = new Sale()
            {
                Quantity = 27,
                Date = DateTime.Now.AddDays(4),
                Product = pileshkaParjola
            };

            context.Sales.AddOrUpdate(plParjolaOrder1);

            var plParjolaOrder2 = new Sale()
            {
                Quantity = 12,
                Date = DateTime.Now.AddDays(5),
                Product = pileshkaParjola
            };

            context.Sales.AddOrUpdate(plParjolaOrder2);

            var plParjolaOrder3 = new Sale()
            {
                Quantity = 6,
                Date = DateTime.Now.AddDays(3),
                Product = pileshkaParjola
            };

            context.Sales.AddOrUpdate(plParjolaOrder3);

            var plParjolaOrder4 = new Sale()
            {
                Quantity = 3,
                Date = DateTime.Now.AddDays(2),
                Product = pileshkaParjola
            };

            context.Sales.AddOrUpdate(plParjolaOrder4);

            var kinderOrder1 = new Sale()
            {
                Quantity = 2,
                Date = DateTime.Now.AddDays(5),
                Product = kinderSurprise
            };

            context.Sales.AddOrUpdate(kinderOrder1);

            var kinderOrder2 = new Sale()
            {
                Quantity = 13,
                Date = DateTime.Now.AddDays(4),
                Product = kinderSurprise
            };

            context.Sales.AddOrUpdate(kinderOrder2);

            var kinderOrder3 = new Sale()
            {
                Quantity = 30,
                Date = DateTime.Now.AddDays(3),
                Product = kinderSurprise
            };

            context.Sales.AddOrUpdate(kinderOrder3);

            var kinderOrder4 = new Sale()
            {
                Quantity = 4,
                Date = DateTime.Now.AddDays(2),
                Product = kinderSurprise
            };

            context.Sales.AddOrUpdate(kinderOrder4);

            var rodopeqOrder1 = new Sale()
            {
                Quantity = 4,
                Date = DateTime.Now.AddDays(2),
                Product = mlqkoRodopeq
            };

            context.Sales.AddOrUpdate(rodopeqOrder1);

            var rodopeqOrder2 = new Sale()
            {
                Quantity = 15,
                Date = DateTime.Now.AddDays(5),
                Product = mlqkoRodopeq
            };

            context.Sales.AddOrUpdate(rodopeqOrder2);

            var rodopeqOrder3 = new Sale()
            {
                Quantity = 11,
                Date = DateTime.Now.AddDays(2),
                Product = mlqkoRodopeq
            };

            context.Sales.AddOrUpdate(rodopeqOrder3);

            var rodopeqOrder4 = new Sale()
            {
                Quantity = 7,
                Date = DateTime.Now.AddDays(2),
                Product = mlqkoRodopeq
            };

            context.Sales.AddOrUpdate(rodopeqOrder4);

            context.Sales.Add(zagorkaOrder1);
            context.Sales.Add(zagorkaOrder2);
            context.Sales.Add(zagorkaOrder3);
            context.Sales.Add(zagorkaOrder4);

            context.Sales.Add(vodkaOrder1);
            context.Sales.Add(vodkaOrder2);
            context.Sales.Add(vodkaOrder3);
            context.Sales.Add(vodkaOrder4);

            context.Sales.Add(becksOrder1);
            context.Sales.Add(becksOrder2);
            context.Sales.Add(becksOrder3);
            context.Sales.Add(becksOrder4);

            context.Sales.Add(milkaOrder1);
            context.Sales.Add(milkaOrder2);
            context.Sales.Add(milkaOrder3);
            context.Sales.Add(milkaOrder4);

            context.Sales.Add(devinOrder1);
            context.Sales.Add(devinOrder2);
            context.Sales.Add(devinOrder3);
            context.Sales.Add(devinOrder4);

            context.Sales.Add(svParjolaOrder1);
            context.Sales.Add(svParjolaOrder2);
            context.Sales.Add(svParjolaOrder3);
            context.Sales.Add(svParjolaOrder4);

            context.Sales.Add(plParjolaOrder1);
            context.Sales.Add(plParjolaOrder2);
            context.Sales.Add(plParjolaOrder3);
            context.Sales.Add(plParjolaOrder4);

            context.Sales.Add(kinderOrder1);
            context.Sales.Add(kinderOrder2);
            context.Sales.Add(kinderOrder3);
            context.Sales.Add(kinderOrder4);

            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
