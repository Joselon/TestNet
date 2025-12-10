public static class Seeder
{
    public static async Task SeedAsync(BlogContext context)
    {
        if (!context.Blogs.Any())
        {
            Console.WriteLine("Insertando blogs...");
            var blog = new Blog { Name = "RedFlagList", Url = "https://redflaglist.com" };        
            context.Blogs.Add(blog);

            var blog2 = new Blog { Name = "WhiteFlagList", Url = "https://whiteflaglist.com" };        
            context.Blogs.Add(blog2);

            for (var index = 1; index <=10; index++) {
                var post = new Post() {
                    Title = $"Red Flag #{index}",
                    CreatedAt = DateTime.UtcNow,
                    Content = $"Esta es la Red flag: {index}"
                };
            
                blog.Posts.Add(post);
            }

            for (var index = 1; index <=5; index++) {
                var post = new Post() {
                    Title = $"White Flag #{index}",
                    CreatedAt = DateTime.UtcNow,
                    Content = $"Esta es la White flag: {index}"
                };
            
                blog2.Posts.Add(post);
            }

            await context.SaveChangesAsync();
        }
    }
}