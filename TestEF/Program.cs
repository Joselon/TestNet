using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<BlogContext>()
    .UseNpgsql("Host=localhost;Database=redflagdb;Username=monhigas;Password=conT0m@te");

using var context = new BlogContext(builder.Options);

await context.Database.EnsureCreatedAsync();

await Seeder.SeedAsync(context);

var blogs = context.Blogs.Include(b => b.Posts).Where(b => b.Posts.Any(p => p.Id > 5));

foreach (var blog in blogs) {
    Console.WriteLine($"blog name {blog.Name}" );
    Console.WriteLine($"El blog tiene {blog.Posts.Count} posts y su mayor id es {blog.Posts.Max(p => p.Id)}");
}