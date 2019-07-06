# coremvc-market
This example of web application for a product store.
Anonymous user can to list of products and watch details of item.
Registered users can manage of products:
1. Add new product
2. Edit product
3. Delete product
Also users can add picture for product. Picture must be a jpg or png format, size is lower than 500 Kb and width and heght are equeals 200 px.
Validate picture implemented in frontend by JavaScript and backend.

Application uses EF Core and migrations to work with SQL Express.

001_InitialCreate_Miq2019_07_06.sql - this script creates empty database. Applcation at first start create initial data in DB.
