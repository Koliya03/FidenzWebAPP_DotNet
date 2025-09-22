1.Before build the application ,ensure that you have given your databaseServer correctly in appsettings.json:
ex:
 "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\localDB;Database=CustomersWebDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }

2.Open the Package Manager Console and run the following commands
    i.add-migration "message(ex:Init db)"
    ii.update-database

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

(for the local host ,if you want to launch it in http please go to the launchSetting.json and paste your https or http)