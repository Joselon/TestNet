using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<BlogContext>()
    .UseNpgsql("Host=localhost;Database=redflagdb;Username=monhigas;Password=conT0m@te");

using var context = new BlogContext(builder.Options);
