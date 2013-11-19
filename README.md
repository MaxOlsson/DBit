DBit
====

A lightweight micro-ORM with performance and ease-of-use in mind


Execute Querys
===
```
//returns a dynamic object
dynamic contact = dbit.Query("SELECT TOP 1 * FROM CONTACT").Populate();

//returns a list<dynamic>
IEnumerable<dynamic> contacts = dbit.Query("SELECT * FROM CONTACT").PopulateModels();

Contact contact = dbit.Query("SELECT TOP 1 * FROM CONTACT").Populate<Contact>();
IEnumerable<Contact> contacts = dbit.Query("SELECT TOP 1 * FROM CONTACT").PopulateModels<Contact>();
```

Execute Stored Procedures
==
```
dynamic contact = dbit.StoredProcedure("usp_GetContacts").Populate();
IEnumerable<dynamic> contacts = dbit.StoredProcedure("usp_GetContacts").PopulateModels();

Contact contact = dbit.StoredProcedure("usp_GetContacts").Populate<Contact>();
IEnumerable<Contact> contacts = dbit.StoredProcedure("usp_GetContacts").PopulateModels<Contact>();
```

Add parameters to Querys or Stored Procedures
===
var contact = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .Populate<Contact>();

Return an int-result of the Query or Stored Procedure
===
var result = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .Execute();

Get the returned dataset from the Query or Stored Procedure
===
DataSet ds = dbit.Query("SELECT * FROM CONTACT WHERE ID = @ID AND NAME = @NAME")
                    .AddParameter("@ID", 8128)
                    .AddParameter("@NAME", "dbit")
                    .ExecuteDataSet();
