Data Object Base is an example of an intelligent baseobject, containing properties that mirror tables and their columns in a database.

This project is part of an assignment in a Web Development course. 
The assignment: Make your objects intelligent:
1. The object are POCO's and have a one-on-one relation to tables and their columns in the database.
1. An object can update itself.
2. Or by supplying an id, it can populate themselves by retrieving data from the database.
3. The use of Entity Framework was not allowed.
4. therefore almost immediately turning this project into a database-first project.


So, if you want to achieve some functionality resembling what Entity does for your POCO's, but
1. Don't use Identity
2. Work Database first

This project might interest you.

This base object gives a generic solution, using reflection to retrieve property names and their values.

The base object is able to:
1. update an entire object to the database.
2. populate an entire object by supplying an Id.
3. populate an object in the constructor of a extended class (supplying a datareader).
4. support active loading and update a single property in the database.

1 and 2 (Updating or populating an entire object) are also supported for a compound primary key.
Populating by Id is not supported. 

Supplied are some classes. In those classes the properties have unique get and sets.
There are four categories:
1. Id property
2. Id poprerty that is part of a compound key.
3. A normal property
4. A readonly property that in the database always has its default value. [DefaultColumn] 
Usually this is a DateTime for a LastUpdated column, but it could be something else also.
