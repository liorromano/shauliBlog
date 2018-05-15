using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Fan
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public int TimeInClub { get; set; }

    }
    public class FanDBContext : DbContext
    {
        public DbSet<Fan> Fans { get; set; }
    }
}