using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;




namespace WebApplication1.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int PostID { get; set; }
        public int CommentID { get; set; }
        public virtual Post Post { get; set; }
        public virtual Comment Comment { get; set; }
    }
}