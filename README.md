DBit
====

A lightweight micro-ORM with performance and ease-of-use in mind


Execute Querys
-------------------------
```C#
//returns a dynamic object
dynamic contact = dbit.Query("SELECT TOP 1 * FROM CONTACT").Populate();

//returns a IEnumerable<dynamic>
IEnumerable<dynamic> contacts = dbit.Query("SELECT * FROM CONTACT").PopulateModels();

//returns an object of type T
Contact contact = dbit.Query("SELECT TOP 1 * FROM CONTACT").Populate<Contact>();

//returns a IEnumerable<T>
IEnumerable<Contact> contacts = dbit.Query("SELECT TOP 1 * FROM CONTACT").PopulateModels<Contact>();
```

Execute Stored Procedures
-------------------------
```C#
//returns a dynamic object
dynamic contact = dbit.StoredProcedure("usp_GetContacts").Populate();

//returns a IEnumerable<dynamic>
IEnumerable<dynamic> contacts = dbit.StoredProcedure("usp_GetContacts").PopulateModels();

//returns an object of type T
Contact contact = dbit.StoredProcedure("usp_GetContacts").Populate<Contact>();

//returns a IEnumerable<T>
IEnumerable<Contact> contacts = dbit.StoredProcedure("usp_GetContacts").PopulateModels<Contact>();
```

Add parameters to Querys or Stored Procedures
-------------------------
```C#
//Returns an object of type T
var contact = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .Populate<Contact>();
```
Return the int-result
-------------------------
```C#
var result = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .Execute();
```
Get the returned dataset
-------------------------
```C#
DataSet ds = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .ExecuteDataSet();
```
