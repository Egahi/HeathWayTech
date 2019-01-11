# HeathWayTech

This is a web application where different you can rate books and view books that have ratings higher than 3.
<br/>Your user name is your unique identifier and you can only rate a book once.

## Technolgies used
* ASP.NET Web Application templates on Visual Studio
* ADO.NET data access framework
* mySQL database
PS: Ensure you have these tools full setup on your computer. A simple google search will get you started.

## Setting up your local database
Run the following commands from your mySQL command Line Client to setup your local database

1. create a database with a name of your choice e.g heathwaytech <br/>
* `use heathwaytech`

2. create Books table <br/>
* `create table Books (ID int auto_increment not null, title varchar(255) not null, year int, author varchar(255), primary key(ID));`

3. Insert books into the books table<br/>
* `insert into Books (ID, title, year, author) values (null, 'A Tale of Two Cities', 1859, 'Charles Dickens');` <br/>
* `insert into Books (ID, title, year, author) values (null, 'The Lord of the Rings', 1955, 'JRR Tolkien');` <br/>
* `insert into Books (ID, title, year, author) values (null, 'The Hobbit', 1937, NULL);` <br/>
* `insert into Books (ID, title, year, author) values (null, 'The Little Prince', 1943, 'Antoine');` <br/>

4. create Users table <br/>
* `create table Users (ID int auto_increment not null, name varchar(255) not null, primary key(ID));`

5. Create Ratings table <br/>
* `create table Ratings (UserID int not null, BookID int not null, rating int not null, ratingdate date, primary key(UserID));`

## Running the Application
1. Clone this repository into your computer<br/>
* `git clone https://github.com/Egahi/HeathWayTech.git`
2. Lauch visual Studio
3. Navigate to File > Open > Project/Solution
4. In the dialog box that pops up, navigate to the HeathWayTech folder you just clone
5. Select HeathWayTech file (it is formated as a visual studio solution)
6. Click open
7. Open Web.config file (should be displayed in the solution explorer)
8. update the code on line 28 to reflect your local mySQL setup<br/>
`<add name="myConnectionString" connectionString="server=localhost; user id=root; database=heathwaytech; password=;" providerName="System.Data.SqlClient"/>`
* set the user id (if not root) to yours
* set the password to yours (if any)
* set the databasename (if you used a different name for your database)
9. Save and Launch the application
