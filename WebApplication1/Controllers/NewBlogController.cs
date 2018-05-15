using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class NewBlogController : Controller
    {
        //blogContext is a table that holds the posts of the enrollments and comments.
        private BlogContext db = new BlogContext();
       
        // GET: NewBlog
        public ActionResult Index(string byName,string byLetter, string byTitle)
        {
            // this funcation gets 3 parameters and activate the view 
            //the first thing this funcation does is drag all the post from db.
            // the funcation sort the data from the db according to the given parameters value.
            //all the 3 parametrs can be null.
            //fianally the result will show on the screen (go to the view)
            var posts = from m in db.Posts
                       select m;

            if (!String.IsNullOrEmpty(byName))
            {
                posts = posts.Where(s => s.WriterName.Contains(byName));
            }
            if (!string.IsNullOrEmpty(byLetter))
            {
                posts = posts.Where(x => x.Content.Contains(byLetter));
            }
            if (!string.IsNullOrEmpty(byTitle))
            {
                posts = posts.Where(x => x.Title == byTitle);
            }
            return View(posts);
        }

        public ActionResult AddComment([Bind(Include = "CommentID,CommentTitle,WriterName,URL,Content")] Comment comment, int ID)
        {
            //the funcation bind take the all paramters (commentid, commaenttitle and ect) and create new comment.
            //the int ID is the id of the post in the enrollments table.
            //this function gets
            if (ModelState.IsValid)
            {
                //adds the comment that crated in the first line to the comment table
                db.Comments.Add(comment);
                //create new line in the enrollment table with the given parameters
                Enrollment t = new Enrollment { PostID = ID, CommentID = comment.CommentID };
                db.Enrollments.Add(t);
                db.SaveChanges();
                //return to the website and now show me the new comment (go the index in the package view in newblog (running the index function))
                return RedirectToAction("Index", "NewBlog");
            }

            return View(comment);
        }

        protected override void Dispose(bool disposing)
        {
            //this funcation creats automatically from the system
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        

    }
}
