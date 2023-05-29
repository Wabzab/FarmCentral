# FarmCentral
Prototype website for Farm Central. Allows farmers to log in to view and manage their products, and employees to log in to create new farmers and view their products.

## Requirements
- .NET 6.0
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SQLServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoftt.VisualStudio.Web.CodeGeneration.Design

## Building
- Open `FarmCentral.sln` in Visual Studio.
- Select `Build -> Build Solution` in the top bar.

## Debugging
In case of invalid certificate:

Close Visual Studio and reopen it. 

This issue seems to occur when initially generating the localhost certificate in order to access the website.
When first generated it does not seem to correctly send the certificate to the browser, resulting in a denial of access.

## How to use
The landing page for the Farm Central website will provide a user with the option to sign in as a 'Farmer' or an 'Employee'.

Signing in as a Farmer requires the user to enter correct account details to be allowed it, same for employee.

Farmers are presented with a filterable list of all their recorded products with options to add new product or edit/delete old products.

Employees are preseented with a list of farmer accounts that have been created with an option to create a new farmer account or edit/delete an old farmer.

Employees are also able to view a filterable list of recorded products for each farmer. This is only a read operation, employees are not able to edit a farmers stock.

## Sample accounts
- Employees

Name: Max

Password: admin


- Farmers

Name: John

Password: password


Name: Doe

Password: password
