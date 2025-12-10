public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; }

    public Blog()
    {
        Posts = new List<Post>();
    }
}