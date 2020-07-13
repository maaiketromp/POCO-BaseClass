# Plain Data Object BaseClass #
This is an example of an intelligent baseobject, classes derived from the baseclass contain properties that mirror tables and their columns in a database.

This project is part of an assignment in a Web Development course. 
The assignment: Make your plain data objects intelligent.
1. The object are POCO's and have a one-on-one relation to tables and their columns in the database.
1. An object can update itself.
2. Or by supplying an id, it can populate itself by retrieving data from the database.
3. The use of the Entity Framework is not allowed.
4. Therefore this project became almost immediately a database-first solution.


So, if you want to achieve some functionality resembling what Entity does for your POCO's, but
1. Don't use Identity
2. Work Database first

This project might interest you.

This base object gives a generic solution, using reflection to retrieve data like property names and values from the child classes.

The base object is able to:
1. update an entire object to the database.
2. populate an entire object if an id is supplied.
3. populate an object, if data is supplied.
4. update a single property in the database.

(For a compound primary key, only 3. is not supported.) 

Supplied is a class library and an example application.
In the individual properties you can spot differences in the implementation of get and set functions.
The base class will check any behaviour that is not allowed, for example when you try to update a default-only property.
