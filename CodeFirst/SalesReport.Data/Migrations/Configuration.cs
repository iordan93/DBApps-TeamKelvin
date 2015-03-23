namespace SalesReport.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SalesReport.Models;
    using System.Collections.Generic;

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

            var mililiters = new MeasureType("ml");
            var kilograms = new MeasureType("kg");
            var liters = new MeasureType("l");
            var pieces = new MeasureType("pieces");
            var grams = new MeasureType("gr");

            var beerZagorka = new Product()
            {
                Name = "Beer “Zagorka”",
                MeasureType = mililiters,
                Price = 1.99m
            };

            context.Products.AddOrUpdate(beerZagorka);

            var zagorkaBurgasPlazza = new Product()
            {
                Name = "Beer “Zagorka”",
                MeasureType = mililiters,
                Price = 1
            };

            context.Products.AddOrUpdate(zagorkaBurgasPlazza);

            var zagorkaKaspichan = new Product()
            {
                Name = "Beer “Zagorka”",
                MeasureType = mililiters,
                Price = 0.92m
            };

            context.Products.AddOrUpdate(zagorkaKaspichan);

            var zagorkaPlovdiv = new Product()
            {
                Name = "Beer “Zagorka”",
                MeasureType = mililiters,
                Price = 0.88m
            };

            context.Products.AddOrUpdate(zagorkaPlovdiv);

            var vodkaTargovishte = new Product()
            {
                Name = "Vodka “Targovishte”",
                MeasureType = mililiters,
                Price = 8.49m
            };

            context.Products.AddOrUpdate(vodkaTargovishte);

            var vodkaTargovishteBurgas = new Product()
            {
                Name = "Vodka “Targovishte”",
                MeasureType = mililiters,
                Price = 8.5m
            };

            context.Products.AddOrUpdate(vodkaTargovishteBurgas);

            var vodkaTargovishtePld = new Product()
            {
                Name = "Vodka “Targovishte”",
                MeasureType = mililiters,
                Price = 7.7m
            };

            context.Products.AddOrUpdate(vodkaTargovishtePld);

            var vodkaTargovishteZmeiovo = new Product()
            {
                Name = "Vodka “Targovishte”",
                MeasureType = mililiters,
                Price = 7.8m
            };

            context.Products.AddOrUpdate(vodkaTargovishteZmeiovo);

            var beerBecks = new Product()
            {
                Name = "Beer “Beck’s”",
                MeasureType = mililiters,
                Price = 1.7m
            };

            context.Products.AddOrUpdate(beerBecks);

            var beerBecksKaspichan = new Product()
            {
                Name = "Beer “Beck’s”",
                MeasureType = mililiters,
                Price = 1.2m
            };

            context.Products.AddOrUpdate(beerBecksKaspichan);

            var beerBecksStoliupinovo = new Product()
            {
                Name = "Beer “Beck’s”",
                MeasureType = mililiters,
                Price = 1.05m
            };

            context.Products.AddOrUpdate(beerBecksStoliupinovo);

            var chockolateMilka = new Product()
            {
                Name = "Chocolate “Milka”",
                MeasureType = grams,
                Price = 2.99m
            };

            context.Products.AddOrUpdate(chockolateMilka);

            var chockolateMilkaKaspichan = new Product()
            {
                Name = "Chocolate “Milka”",
                MeasureType = grams,
                Price = 2.9m
            };

            context.Products.AddOrUpdate(chockolateMilkaKaspichan);

            var chockolateMilkaZmeiovo = new Product()
            {
                Name = "Chocolate “Milka”",
                MeasureType = grams,
                Price = 2.85m
            };

            context.Products.AddOrUpdate(chockolateMilkaZmeiovo);

            var vodaDevin = new Product()
            {
                Name = "Mineralna Voda \"Devin\"",
                MeasureType = liters,
                Price = 3.49m
            };

            context.Products.AddOrUpdate(vodaDevin);

            var svinskaParjola = new Product()
            {
                Name = "Svinska Parjola",
                MeasureType = kilograms,
                Price = 5.99m
            };

            context.Products.AddOrUpdate(svinskaParjola);

            var pileshkaParjola = new Product()
            {
                Name = "Pileska Parjola",
                MeasureType = kilograms,
                Price = 4.99m
            };

            context.Products.AddOrUpdate(pileshkaParjola);

            var kinderSurprise = new Product()
            {
                Name = "Kinder Surprise",
                MeasureType = pieces,
                Price = 1.59m
            };

            context.Products.AddOrUpdate(kinderSurprise);

            var mlqkoRodopeq = new Product()
            {
                Name = "Kiselo mlqko \"Rodopeq\"",
                MeasureType = mililiters,
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
            context.Products.Add(zagorkaBurgasPlazza);
            context.Products.Add(zagorkaKaspichan);
            context.Products.Add(zagorkaPlovdiv);
            context.Products.Add(vodkaTargovishteBurgas);
            context.Products.Add(vodkaTargovishtePld);
            context.Products.Add(vodkaTargovishteZmeiovo);
            context.Products.Add(beerBecksKaspichan);
            context.Products.Add(beerBecksStoliupinovo);
            context.Products.Add(chockolateMilkaKaspichan);
            context.Products.Add(chockolateMilkaZmeiovo);
            
            var burgasPlaza = new Vendor()
            {
                Name = "Supermarket “Bourgas – Plaza”"
            };

            context.Vendors.AddOrUpdate(burgasPlaza);

            var kaspichanCenter = new Vendor()
            {
                Name = "Supermarket “Kaspichan – Center”"
            };

            context.Vendors.AddOrUpdate(kaspichanCenter);

            var baiIvan = new Vendor()
            {
                Name = "Supermarket “Bay Ivan” – Zmeyovo"
            };

            context.Vendors.AddOrUpdate(baiIvan);

            var pldStolipinovo = new Vendor()
            {
                Name = "Supermarket “Plovdiv – Stolipinovo”"
            };

            context.Vendors.AddOrUpdate(pldStolipinovo);

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
            context.Vendors.Add(baiIvan);
            context.Vendors.Add(pldStolipinovo);
            context.Vendors.Add(kaspichanCenter);
            context.Vendors.Add(burgasPlaza);

            beerZagorka.Vendor = zagorkaCorp;
            zagorkaBurgasPlazza.Vendor = burgasPlaza;
            zagorkaKaspichan.Vendor = kaspichanCenter;
            zagorkaPlovdiv.Vendor = pldStolipinovo;
            vodkaTargovishteBurgas.Vendor = burgasPlaza;
            vodkaTargovishte.Vendor = targovishteCorp;
            beerBecks.Vendor = becksCorp;
            chockolateMilka.Vendor = kraftFoods;
            vodaDevin.Vendor = bulgarianFood;
            svinskaParjola.Vendor = bulgarianFood;
            pileshkaParjola.Vendor = bulgarianFood;
            kinderSurprise.Vendor = kinder;
            mlqkoRodopeq.Vendor = bulgarianFood;
            vodkaTargovishtePld.Vendor = pldStolipinovo;
            vodkaTargovishteZmeiovo.Vendor = baiIvan;
            beerBecksKaspichan.Vendor = kaspichanCenter;
            beerBecksStoliupinovo.Vendor = pldStolipinovo;
            chockolateMilkaKaspichan.Vendor = kaspichanCenter;
            chockolateMilkaZmeiovo.Vendor = baiIvan;

            context.SaveChanges();
        }
    }
}
