# Plain Data Object BaseClass #
This is an intelligent base class in a C# ASP.NET Core Class Library. Classes and their properties derived from this baseclass mirror respectively tables and columns from a connected (Sql Server) database.

This project is part of an assignment in a Web Development course. 
The assignment was: make your plain data objects intelligent by enabling them:
1. To mirror a table in the database: the object are POCO's and have a one-on-one relation to database tables and their columns.
1. To update itself. 
1. To populate themselves by supplying an id.
1. To populate themselves by supplying data.

Restrictions were:
1. Do not use the Entity Framework.
As we did not want to create a migration- and versionmanagementsystem, this project became database first almost immediately.

So, if you want to achieve some functionality resembling what Entity does for your POCO's, but
1. For some particular reason do not want to use the Identity framework,
1. Or want a database-first system,
This project might interest you.

This base class creates a generic solution, using reflection to retrieve data like property names and values from the derived classes.

The base object is able to:
1. Update an entire object to the database.
2. Populate an entire object with an id.
3. Populate an object, with supplied data.
4. Update a single property in the database.

(For a compound primary key only 3. is not supported.) 

The base class will check for any behaviour that is not allowed. Examples:
1. You are not allowed to update properties with an id attribute. 
1. If the resultset contains a non-null value, but the object does not contain a corresponding property, an exception will be thrown. 

Supplied is a class library, an example application and a testproject. 
Not only normal cases are tested, also all Exceptions are triggered and tested if behaviour is as expected.

This assignment's first release was in July 2020.
