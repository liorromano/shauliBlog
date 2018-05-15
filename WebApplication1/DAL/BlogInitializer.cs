using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace WebApplication1.DAL
{//when the web is empty. this file is the first thing that run behind the scence.
    public class BlogInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {

            //creates lior, noa and guy as admins
            ApplicationDbContext context2 = new ApplicationDbContext();
            IdentityRole role1=new IdentityRole { Name = "Admin" };
            context2.Roles.Add(role1);
            context2.SaveChanges();

           
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Lr1234!");
            ApplicationUser admin = new ApplicationUser { UserName = "liorromano90@gmail.com", Email = "liorromano90@gmail.com", PasswordHash = password , SecurityStamp = Guid.NewGuid().ToString(),LockoutEnabled =true};
            context2.Users.Add(admin);
            string password2 = passwordHash.HashPassword("Go1234!");
            ApplicationUser admin2 = new ApplicationUser { UserName = "guyorbach1@gmail.com", Email = "guyorbach1@gmail.com", PasswordHash = password2, SecurityStamp = Guid.NewGuid().ToString(),LockoutEnabled = true };
            context2.Users.Add(admin2);
            string password3 = passwordHash.HashPassword("Nf1234!");
            ApplicationUser admin3 = new ApplicationUser { UserName = "nonkif@gmail.com", Email = "nonkif@gmail.com", PasswordHash = password3, SecurityStamp = Guid.NewGuid().ToString(), LockoutEnabled = true };
            context2.Users.Add(admin3);
            context2.SaveChanges();

            IdentityUserRole adminRole = new IdentityUserRole()
            {
                RoleId = role1.Id,
                UserId = admin.Id
            };
            admin.Roles.Add(adminRole);
            context2.SaveChanges();

            IdentityUserRole adminRole2 = new IdentityUserRole()
            {
                RoleId = role1.Id,
                UserId = admin2.Id
            };
            admin2.Roles.Add(adminRole2);
            context2.SaveChanges();

            IdentityUserRole adminRole3 = new IdentityUserRole()
            {
                RoleId = role1.Id,
                UserId = admin3.Id
            };
            admin3.Roles.Add(adminRole3);
            context2.SaveChanges();



            Post p1 = new Post { Title = "BATTOCCHIO JOINS MACCABI TEL AVIV", WriterName = "Maccabi-tlv", Date = DateTime.Now, Content = "Cristian Battocchio joined Maccabi Tel Aviv as the 25-year-old Argentine born midfielder successfully underwent a medical and signed a two-year-deal with an option for a further season. The diminutive midfielder who made 20 appearances for Italy’s U-20 and U-21’s, joins Maccabi on a free transfer after spending the last two seasons in France playing for Stade Brest. Maccabi’s coach Jordi Cruyff told the club’s  official website: “Cristian is the kind of player we feel can give us a different dimension and upgrade our game”. Cristian told the club: “I am happy to be here and sign for Maccabi.It is a new experience for me to sign for such a big club which will always fight for all the titles and competing in Europe is another challenge I am looking forward to”. The Rosario born midfielder who began his playing career at the Youth Academy of Newll’s Old Boys, joined Italian club Udinese in 2009 at the age of 16.In 2012 Battocchio joined Watford on loan and after impressing the Hornets boss Gianfranco Zola made the move to Vicarage Road permanent in 2013. The arrival of former Maccabi coach Slavisa Jokanovic at Watford in 2014 saw Battocchio return to Italy and join Virtus Entella on loan before switching to Brest for which he scored 8 goals in 35 appearances last season.", TrendPicture = "https://static.maccabi-tlv.co.il/wp-content/uploads/2017/07/MTA_201707100140390e068bdf68e24ba392bd8a978139faab.jpg", TrendVideoURL = null, URL = "http://www.one.co.il" };
            context.Posts.Add(p1);
            Post p2 = new Post { Title = "'Seinfeld': things you didn't know", WriterName = "lior", Date = DateTime.Now, Content = "One of the most popular TV shows of all time is celebrating its 28th birthday. 'Seinfeld' took a while to find its feet after premiering in 1989, but by the mid-'90s the show was a hit. It ended in 1997 after nine glorious seasons with more than 75 million Americans tuning in to watch the finale. FAVORITE TV REUNIONS To celebrate the show’s birthday, news.com.au assembled some of the most interesting facts about the “show about nothing. JERRY SEINFELD’S FAVOURITE EPISODE There were 180 episodes in total, but Jerry Seinfeld has two favorites in particular. “One was the 'The Rye,' because we got to shoot that at Paramount Studios in LA which was the first time that we thought, ‘wow this is almost like a real TV show’,” Seinfeld said during a Reddit AMA. “We hadn’t felt like a real TV show, the early years of the TV show were not successful...We felt like we were a weird little orphan show.So that was a big deal for us.” And the comedian’s other favorite episode is 'The Pothole.' “Newman drives his mail truck over a sewing machine and his mail truck burst into flames,” Seinfeld recalled. “It was really fun to shoot, and it was fun to set Newman on fire.And he screamed, ‘oh the humanity’ like from the Hindenberg disaster.", TrendPicture = "http://www.sonypictures.com/tv/seinfeld/assets/images/onesheet.jpg", TrendVideoURL = "https://www.youtube.com/embed/9RK99NAJyeg", URL = "http://www.sonypictures.com/tv/seinfeld/" };
            context.Posts.Add(p2);
            context.SaveChanges();


            Comment c1 = new Comment { CommentID = 1050, CommentTitle = "Great Player", Content = "One of the best players that they could sign", URL = "http://www.walla.co.il", WriterName = "Lior" };
            context.Comments.Add(c1);
            Comment c2 = new Comment { CommentID = 3000, CommentTitle = "Hahaha", Content = "Thats great. Nice video", URL = "http://www.seinfeld.com", WriterName = "Guy" };
            context.Comments.Add(c2);
            Comment c3 = new Comment { CommentID = 3001, CommentTitle = "I love them", Content = "I love this show!!", URL = "http://www.seinfeld.com", WriterName = "Noa" };
           context.Comments.Add(c3);
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
            new Enrollment{PostID=p1.ID,CommentID=c1.CommentID},
            new Enrollment{PostID=p2.ID,CommentID=c2.CommentID},
            new Enrollment{PostID=p2.ID,CommentID=c3.CommentID},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
           context.SaveChanges();

           
            

        }
    }
}

