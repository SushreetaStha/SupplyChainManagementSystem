INSERT INTO public."Products" ("Id","ProductCode","Date","ProductName","Category","Quantity","SellingPrice") VALUES
	 ('b976618b-3930-4c47-965c-33210f7e041e','PROD001','2022-07-01 00:00:00','Shopping Bag',1,550,100.0),
	 ('a1420734-d9c7-4f76-934e-89863c3156fd','PROD002','2022-07-07 00:00:00','Yoghurt Package',0,450,110.0),
	 ('2efab447-8c7c-4182-ba51-8be46020369d','PROD003','2022-06-30 00:00:00','Milk Package',2,800,200.0);
	 
INSERT INTO public."RawMaterials" ("Id","MaterialCode","Date","MaterialName","Quantity","CostPrice") VALUES
	 ('38b934ab-4965-4a45-954f-f85033743a54','MAT001','2022-03-17 00:00:00','PPE',500,110.0),
	 ('97386255-df12-4684-9a8f-ac05a19a175e','MAT002','2022-06-09 00:00:00','LDPE',700,150.0),
	 ('9b2426b9-c86f-4c12-8994-0090e52cf54d','MAT003','2022-06-11 00:00:00','HDPE',450,120.0),
	 ('4b421e02-f4b5-4818-9884-6746b446ffea','MAT004','2022-06-06 00:00:00','LLDPE',600,170.0);
	 
INSERT INTO public."Suppliers" ("Id","SupplierCode","Date","SupplierName","Contact","Address") VALUES
	 ('34781015-f4aa-4ec7-a344-bb705ab703ab','SUP001','2022-04-06 00:00:00','Hari Phuyal','9841568712','Sitapaila'),
	 ('cd865f61-5785-477d-ad1c-cca3fc73be62','SUP002','2022-05-27 00:00:00','Ramesh Maharjan','9841457897','Dallu');
 
INSERT INTO public."Customers" ("Id","CustomerCode","Date","CustomerName","Contact","Address") VALUES
	 ('774d0a57-5c0f-4347-ae7b-c072fc0f85ff','CUS001','2022-04-01 00:00:00','Manish Neupane','9860657894','Sitapaila'),
	 ('5344e96d-3dc3-4d0a-a63d-b34beef8588b','CUS002','2022-06-01 00:00:00','Sunita Shrestha','9841785469','Dhalko'),
	 ('c457c8cf-57f4-4611-b15b-89abb68d3ec0','CUS003','2022-07-05 00:00:00','Bina Karki','9841759231','Kalimati'),
	 ('fa1e2aad-6b65-4fff-b5f5-ff8fff470c66','CUS004','2022-07-09 00:00:00','Maheshwor Dhakal','9845812047','Gongabu'),
	 ('0debd6ef-91e6-4cb3-869a-26531e4a7a9d','CUS005','2022-07-16 00:00:00','Sweta Maharjan','9874598741','Bafal');