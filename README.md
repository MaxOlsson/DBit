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

//get a specific value (datatype) from the table
int contactID = dbit.Query("SELECT TOP 1 ID * FROM CONTACT").Populate();
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
                    
//Supports arrays
var contact = dbit.Query("SELECT TOP 1 * FROM CONTACT WHERE ID IN ( @IDS )")
                      .AddParameter("@IDS", new int[] { 8228, 500, 404})
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

| Code         |  Duration (Best) |
|------------- |---------- |
|`.Populate();`| 33ms |
|`.Populate<T>();`| 35ms |
|`.PopulateModels();`| 33ms |
|`.PopulateModels<T>();`| 38ms |

The exact code I ran to test this was with
```C#
    Stopwatch sw = new Stopwatch();

    DBit dbit = new DBit(new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection_DEV"].ToString()));

    sw.Start();
    
    /*  dbit   */
    //dynamic c1 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate();
    //dynamic c2 = dbit.Query("SELECT TOP 1 * FROM [CONTACT] WHERE ID = @ID").AddParameter("@ID", 15318).Populate();
    //Contact c3 = dbit.Query("SELECT TOP 1 * FROM [CONTACT]").Populate<Contact>();
    //IEnumerable<dynamic> c4 = dbit.Query("SELECT TOP 100 * FROM [CONTACT]").PopulateModels();
    //IEnumerable<Contact> c5 = dbit.Query("SELECT TOP 100 * FROM [CONTACT]").PopulateModels<Contact>();

    sw.Stop();
    Console.WriteLine("Executed in: {0} (minutes, seconds, millisecs)", sw.Elapsed.ToString("mm\\:ss\\.fff"));

```
License
-------------------------

    The MIT License (MIT)

    Copyright (c) 2013 Max Olsson

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
