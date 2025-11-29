//TestNet Program.cs
using Microsoft.Data.SqlClient;

Console.WriteLine("TestNet init");

using var con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestNetdB;Integrated Security=True;Connect Timeout=30");
//con.Open();
await con.OpenAsync();

//Select
using var selectPre = new SqlCommand("SELECT TOP 10 Id, Name FROM Users", con);
var readerPre = await selectPre.ExecuteReaderAsync();
Console.WriteLine("Initial Users:");
while (await readerPre.ReadAsync())
{
    PrintUser((int)readerPre["Id"], readerPre["Name"].ToString());
}

readerPre.Close();

//Insert
using var insert = new SqlCommand("INSERT INTO Users (Name) VALUES ('NewUser'+ CAST(((SELECT COUNT(*) FROM Users) +1) AS NVARCHAR))", con);
var rows = await insert.ExecuteNonQueryAsync();
Console.WriteLine($"Inserted rows: {rows}");

//Select
using var selectPost = new SqlCommand("SELECT TOP 10 * FROM Users", con);
var readerPost = await selectPost.ExecuteReaderAsync();
Console.WriteLine("Post-insert Users:");
while (await readerPost.ReadAsync())
{
    PrintUser((int)readerPost["Id"], readerPost["Name"].ToString());
}
readerPost.Close();
//con.Close();
Console.WriteLine("TestNet end");

static void PrintUser(int id, string? name)
{
    Console.WriteLine("\n" + "Id -> " + id);
    Console.WriteLine("Name -> " + name + "\n");
}