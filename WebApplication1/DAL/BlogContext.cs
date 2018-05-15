using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.DAL
{
    //merge the 3 kinds of table, by using the name 'BlogContext'.
    //when i write BlogContext db. this db can be the post table, the enrlloments table ot the comments table.
    public class BlogContext : DbContext
    {

        public BlogContext() : base("BlogContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}