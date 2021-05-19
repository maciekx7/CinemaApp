# CinemaApp
Client-Server Cinema Application. Android Mobile Client, REST API ASP.NET Core server and MySQL database.

This project was created on porpouse of academic project. 

In src folder you can find:
* Server source code written in ASP.NET Core C#,
* Adroid .APK file of client application,
* Mysql database script which contains database structure and example data.

In `src/CinemaServer/config/json/database` you should place valid connection data for your MySql database. 
If Mysql script is not working, delete 7-18 and 466-472 rows in SQL script.

In first version of this application some security gaps were created on education porpouse. In Documentation_v1 you can find all of them. 
In Documentation_v2 you can find that all gaps was fixed and for example in final version of project `/Ticket/Buy` API is a POST HTTP request (Not GET, as Documentation_v1 says).
