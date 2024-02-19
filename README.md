# Bakery.Solution

####  An MVC web application for managing the treats, flavors, and orders of a fictional bakery.

#### By Teddy Peterschmidt

## Technologies Used

* C#
* .NET 6 SDK
* Entity Framework Core

## Description

This application allows the user to manage the bakery's treats, flavors, and orders through the following functionality: 

* Creating an account and signing in and out
* Adding to a list of treats 
* Editing treat information
* Deleting treats
* Adding flavors to treats
* Adding to a list of flavors
* Editing flavor details
* Deleting flavors
* Adding to a list of orders associated with the current user
* Editing order information
* Deleting orders

In order to create, edit, or delete flavors or orders, the user must be signed in. For editing treats, the user must be signed in and given the Admin role.

## Setup/Installation Requirements

* Clone this repository.
* If needed, download and configure MySQL Workbench for your operating system by following the instructions in [this lesson.](https://full-time.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql) 
* Navigate to the production directory "Bakery".
* Within the production directory "Bakery", create a new file called `appsettings.json`.
* Within `appsettings.json`, put in the following code, replacing the `database`, `uid`, and `pwd` values with your own username and password for MySQL.
```json 
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database={YOUR_DATABASE_NAME_HERE};uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}
```
* In the command line, run "dotnet restore" to download and install packages.
* If needed, add `dotnet-ef` to your device by running "dotnet tool install --global dotnet-ef --version 6.0.0"
* In the command line run "dotnet ef database update" to update your database.
* In the command line, run the command "dotnet run" to compile and execute the application.
* Optionally, you can run "dotnet build" to compile this application without running it.

## Known Bugs

* The Admin role assignment may not be recognzied right away, and may require the user to restart the application to gain access to controller methods the require the Admin role.

## License

[MIT License](./LICENSE)