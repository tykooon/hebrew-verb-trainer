// See https://aka.ms/new-console-template for more information
using HebrewVerb.DbTest;

var db = new TestDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<TestDbContext>());

Console.WriteLine(db.Database.EnsureCreated());
var name = db.Gizras.Find(1)?.Name ?? "Not Found";
Console.WriteLine(name);

