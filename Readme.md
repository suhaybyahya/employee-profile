# Employee Profile


The project aims to tidy up and ease up the process of handling and managing employee data, departments, and linking them together.


You can do the ordinary operations you can do on an employee profile. You can add new ones or delete the old ones. Plus, you can update employee data or link them to other departments.


## Technical specs


The project is a Web API new core project built with the latest versions of.net (.net 7 and .net core 3.1).


## Packages used


1. AutoMapper: Used to map between view models and entity models.
2. log4net: used for logging (debug and error messages).
3. EF: Used to build the object-relation mapper and the data access layer.
4. Swashbuckle and swagger : used to test the APIs from the swagger UI page.


## APP Structure


The app is split into three different layers; this structure is simple for apps that are not too complicated.


1. API Layer: used as an entry point to the app where the requests start and are validated before moving on to the next layer.
2. Managers: It acts as a middleware between the client/API and the data access layer; this layer is responsible for doing and preparing the data fetched from the data access layer and doing the needed calculations and operations before sending back the needed response.
3. Data Access Layer: The layer that is responsible for communicating and fetching or inserting data into the database.



## How to run the application locally


### Prerequisites


Make sure you have .net core SDK or at least the .net runtime installed.
and you also have to prepare a valid SQL DB server connection. Whether it is hosted on your machine or on other machines, you only need a valid connection.


### How to run the app


Before running the app, you should change a couple of things.


1. DB connection: Navigate to app.settings file and set the value for the key '[ConnectionStrings][DefaultConnection]'.


    ![ConnectionStrings](image-1.png)


2. Change the logging directory path: Navigate to the log4net.config file and specify the directory path for logging: `<file value="D:\TestLog\" />`


Then you can run the `dotnet run` command in the terminal, and the app will launch.


> No need to create a DB instance; the app will create the DB, do the migration, and seed data automatically.