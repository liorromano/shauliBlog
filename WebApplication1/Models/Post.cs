using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web; 
using System.ComponentModel.DataAnnotations; 

namespace WebApplication1.Models
{
    public class Post
    {
        public int ID { get; set; }
        [Required] 
        [Display(Name = "Post Title")]
        public string Title{ get; set; }
        [Required]
        [Display(Name = "Writer Name")]
        public string WriterName { get; set; }
        [Required(ErrorMessage = "URL is required")]
        [DataType(DataType.Url)]
        public string URL { get; set; }
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        [DataType(DataType.Url)]
        public string TrendPicture { get; set; }
        [DataType(DataType.Url)]
        public string TrendVideoURL { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
    public class PostDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}