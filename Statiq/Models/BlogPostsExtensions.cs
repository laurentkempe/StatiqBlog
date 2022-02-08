namespace Blog.Statiq.Models;

public static class BlogPostsExtensions
{
    public static List<string> AllTagsOrdered(this IEnumerable<BlogPost> blogPosts) => 
        blogPosts.SelectMany(post => post.Tags).Except(new[] { "" }).Distinct().OrderBy(s => s).ToList();

    public static IEnumerable<BlogPost> AllPostsPerTag(this IEnumerable<BlogPost> blogPosts, string tagName) => 
        blogPosts.Where(post => post.Tags.Contains(tagName)).OrderByDescending(post => post.Date);

    public static int CountPostsPerTag(this IEnumerable<BlogPost> blogPosts, string tagName) => 
        blogPosts.Count(post => post.Tags.Contains(tagName));
}