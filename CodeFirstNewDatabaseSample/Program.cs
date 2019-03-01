using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // when ever we use a context we have to wrap it into a using statement
            using( var db = new BlogContext())
            {
                Console.Write("Enter a name for a new blog:");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                // we write the name to the blog property of the Entity Framework context
                db.Blogs.Add(blog);
                // alle pending changes are pushed to the context to the database
                db.SaveChanges();

                // linq query to select all Blogs from the database ordered by name
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.ReadKey();
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        // added this property with "Add-Migration" Command to initial database
        // see Migrations folder 201903010726027_AddUrl.cs
        public string Url { get; set; }

        // Navigation Property marked with virtual: Defines relationship of Classes for Entity Framework
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        // Navigation Property marked with virtual: Defines relationship of Classes for Entity Framework
        public virtual Blog Blog { get; set; }
    }

    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }

    //DbContext is a base type from Entity Framework and represents the session with the database and allows us to query and save data
    public class BlogContext : DbContext
    {
        // these Properties allow us to query and save instances of these types
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        // added after *_AddUrl.cs. This Class has no property that the system recoginzes as Key
        public DbSet<User> Users { get; set; }
    }


}
