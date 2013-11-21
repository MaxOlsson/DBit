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

Performance tests
-------------------------

All tests were performed on a Windows 7 64-bit machine, with Intel i5 2.67GHz and 8GB RAM. 
Connecting to a local SQL 2012 Server and doing lookups against a [Contact] table which has 37 columns, and contains 380207 records.

<table>
  <tr>
    <th>Code</th><th>Fasted Recorded Time</th>
  </tr>
  <tr>
    <td>```C#dynamic c1 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate();```</td><td>33 ms</td>
  </tr>
  <tr>
    <td>dynamic c2 = dbit.Query("SELECT TOP 1 * FROM [CONTACT] WHERE ID = @ID").AddParameter("@ID", 15318).Populate();</td><td>36 ms</td>
  </tr>
  <tr>
    <td>Contact c3 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate<Contact>();</td><td>39 ms</td>
  </tr>
  <tr>
    <td>IEnumerable<dynamic> c4 = dbit.Query("select top 100 * from contact").PopulateModels();</td><td>33 ms</td>
  </tr>
  <tr>
    <td>IEnumerable<Contact> c5 = dbit.Query("select top 100 * from contact").PopulateModels<Contact>();</td><td>38 ms</td>
  </tr>
</table>

The exact code I ran to test this was with
```C#
    Stopwatch sw = new Stopwatch();

    DBit dbit = new DBit(new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection_DEV"].ToString()));

    sw.Start();
    
    /*  dbit   */
    //dynamic c1 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate(); //best: 33 ms ID: 15318
    //dynamic c2 = dbit.Query("SELECT TOP 1 * FROM [CONTACT] WHERE ID = @ID").AddParameter("@ID", 15318).Populate(); //best: 36ms
    //Contact c3 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate<Contact>(); //best: 39ms
    //IEnumerable<dynamic> c4 = dbit.Query("select top 100 * from contact").PopulateModels(); //best 33ms
    //IEnumerable<Contact> c5 = dbit.Query("select top 100 * from contact").PopulateModels<Contact>(); //best 38ms

    sw.Stop();
    Console.WriteLine("Executed in: {0} (minutes, seconds, millisecs)\nPress any key to continue", sw.Elapsed.ToString("mm\\:ss\\.fff"));

```
