using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        [Required(ErrorMessage = "Comment title is required")]
        public string CommentTitle { get; set; }
        [Required(ErrorMessage = "Write name is required")]
        public string WriterName { get; set; }
        [Required(ErrorMessage = "URL is required")]
        [DataType(DataType.Url)]
        public string URL { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
    public class CommentDBContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
    }
}