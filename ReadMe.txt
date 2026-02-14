1.Before build the application ,ensure that you have given your databaseServer correctly in appsettings.json:
ex:
 "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\localDB;Database=CustomersWebDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }

2.Open the Package Manager Console and Select FidenzApp.Infranstructure as the default project.After that run the following commands:
    i.add-migration "message(ex:Init db)"
    ii.update-database
    Now the database has been created on your LocalDB.

3.then build the application using https.

4.You will see the Login page.The credentials:

Admin role:

User name: FidenzAdmin
Password : Admin123

User role:

User name: FidenzUser
Password : User123

5.You can open the swagger using below link:
https://localhost:7299/swagger
(your_localhost/swagger )