--------------------------------------------------------
--  File created - Четвъртък-Март-19-2015   
--------------------------------------------------------

--------------------------------------------------------
--  DDL for Table VENDORS
--------------------------------------------------------

CREATE TABLE "VENDORS" 
(	
	"ID" INT NOT NULL PRIMARY KEY, 
	"VENDOR NAME" NVARCHAR(50)
)
Insert into VENDORS (ID,"VENDOR NAME") values (10,'Nestle Sofia Corp.');
Insert into VENDORS (ID,"VENDOR NAME") values (20,'Zagorka Corp.');
Insert into VENDORS (ID,"VENDOR NAME") values (30,'Targovishte Bottling Company OOD');
Insert into VENDORS (ID,"VENDOR NAME") values (40,'Natural Potato and Co. OOD');
Insert into VENDORS (ID,"VENDOR NAME") values (50,'Home Wine Distillery Corp.');
Insert into VENDORS (ID,"VENDOR NAME") values (60,'Happy Goat OOD');
Insert into VENDORS (ID,"VENDOR NAME") values (70,'Oily Business EOOD');


  --------------------------------------------------------
--  DDL for Table MEASURES
--------------------------------------------------------

CREATE TABLE "MEASURES" 
(	
	"ID" INT NOT NULL PRIMARY KEY, 
	"MEASURES NAME" NVARCHAR(50)
)
Insert into MEASURES (ID,"MEASURES NAME") values (100,'liters');
Insert into MEASURES (ID,"MEASURES NAME") values (200,'pieces');
Insert into MEASURES (ID,"MEASURES NAME") values (300,'kg');
Insert into MEASURES (ID,"MEASURES NAME") values (400,'g');
Insert into MEASURES (ID,"MEASURES NAME") values (500,'ml');
Insert into MEASURES (ID,"MEASURES NAME") values (600,'kit');


--------------------------------------------------------
--  DDL for Table PRODUCTS
--------------------------------------------------------

CREATE TABLE "PRODUCTS" 
(	
	"ID" INT NOT NULL PRIMARY KEY, 
	"VENDOR ID" INT, 
	"PRODUCT NAME" NVARCHAR(50), 
	"MEASURE ID" INT, 
	"PRICE" INT
)

Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (1,20,'Beer "Zagorka"',100,1);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (2,30,'Vodka "Targovishte"',100,8);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (3,20,'Beer "Beck''s"',100,1);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (4,10,'Chocolate "Milka"',200,3);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (5,40,'Potatoes *****',300,2);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (6,40,'Potatoes ***',300,1);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (7,50,'Wine "Chardak"',100,2);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (8,50,'Wine "Chorbajy"',100,9);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (9,60,'Cheese "White Delight"',200,11);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (10,60,'Cheese "Blue Goo"',300,18);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (11,60,'Cheese "Smelly Jelly"',300,16);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (12,70,'Sunflower Oil 1l.',100,7);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (13,70,'Someflower Oil 1l.',100,3);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (14,70,'Sunflower Oil 0.5l',100,4);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (15,70,'Someflower Oil 0.5l.',100,1);
Insert into PRODUCTS (ID,"VENDOR ID","PRODUCT NAME","MEASURE ID",PRICE) values (16,40,'Package "Grow your own..."',600,50);

--------------------------------------------------------
--  Ref Constraints for Table PRODUCTS
--------------------------------------------------------

  ALTER TABLE "PRODUCTS" ADD FOREIGN KEY ("VENDOR ID")
	  REFERENCES "VENDORS" ("ID");
  ALTER TABLE "PRODUCTS" ADD FOREIGN KEY ("MEASURE ID")
	  REFERENCES "MEASURES" ("ID");