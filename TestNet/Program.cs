//TestNet Program.cs
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

Console.WriteLine("TestNet init");

using var con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestNetdB;Integrated Security=True;Connect Timeout=30");
await con.OpenAsync();

//SelectPre

Console.WriteLine("Initial Users:");
await selectAllUsers(con);
Console.WriteLine("\n--------------------------\n");

//Insert
Console.WriteLine("New User insertion:");
using var insert = new SqlCommand("INSERT INTO Users (Name) VALUES ('NewUser'+ CAST(((SELECT COUNT(*) FROM Users) +1) AS NVARCHAR))", con);
var rows = await insert.ExecuteNonQueryAsync();
Console.WriteLine($"Inserted rows: {rows}");
Console.WriteLine("\n--------------------------\n");

//SelectPost
Console.WriteLine("All Users:");
await selectAllUsers(con);
Console.WriteLine("\n--------------------------\n");

Console.WriteLine("TestNet end");

static void PrintUser(int id, string? name)
{
    Console.WriteLine("\n" + "Id -> " + id);
    Console.WriteLine("Name -> " + name + "\n");
}

async Task selectAllUsers(SqlConnection con)
{
    using var selectAll = new SqlCommand("SELECT TOP 10 Id, Name FROM Users", con);
    var readerAll = await selectAll.ExecuteReaderAsync();
    while (await readerAll.ReadAsync())
    {
        PrintUser((int)readerAll["Id"], readerAll["Name"].ToString());
    }
    readerAll.Close();
}