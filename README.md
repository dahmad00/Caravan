# Caravan
Caravan is a Railway Rservation System developed using ASP.NET Core MVC framework. This was my course project for "Advanced Programming."
## Features
1. Elegant Interface
2. Login/Sign Up
3. Browse active tickets, based on arrival, departure and date
4. Fill the ticket booking form to book a ticket and receive its code
5. Admin can add new routes/trains
6. Admin can view/cancel any ticket bookings

## Tools
1. Microsoft SQL Server
2. ASP.NET Core MVC

## Steps to Run This Web Application
1. Clone the Repository
2. Setup Microsoft SQL Server, create a database
3. Open The create_tables.sql file. Copy these queries and execute them on your database.
4. Open the insert_dummy_data.sql file, and execute these queries as well.
5. Generate Connection String from SQL Server Management Studio, and replace it with the string in GlobalSettings.cs file in the Models Folder.
6. Execte the project, the website should open up in your browser.
