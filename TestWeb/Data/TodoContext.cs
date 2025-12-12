using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class TodoContext : DbContext
{
   public DbSet<TodoTask> Todos { get; set; }
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
            
    }   
}