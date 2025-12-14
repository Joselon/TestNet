using Microsoft.EntityFrameworkCore;

namespace TestWebBlazorServer.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoTask> Todos { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
