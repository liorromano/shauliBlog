using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;


namespace WebApplication1.Controllers
{
    public class AdminsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMaxComments()
        {//takes the id of the post and serach this id in the enrrollments table and merge them, then we take this result and we search the id in the comments table and merge it then we get the table of the comments of the specified post
            //then does group (counts then number of comments of every posts)
            db.Configuration.ProxyCreationEnabled = false;

            var results = from a in db.Posts
                          join b in db.Enrollments on a.ID equals b.PostID
                          join c in db.Comments on b.CommentID equals c.CommentID
                          group c.CommentID by a.Title into g
                          orderby g.Count() descending    //key= title of the post.
                                                          //display the name of the post and next to it display the number of coments.
                          select new { PostName = g.Key, Amount = g.Count() };
            return Json(results);
        }

        public ActionResult getNumOfEachWriterComment()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var results = from a in db.Comments
                          join b in db.Comments on a.CommentID equals b.CommentID
                          group b.CommentID by a.WriterName into g
                          orderby g.Count() descending
                          select new { PostName = g.Key, Amount = g.Count() };
            return Json(results);
        }
        [HttpPost]
        public ActionResult getDatac(string t_title, string t_author, string t_cauthor)
        {// gets the 3 parameters and we wants to filter the result
            db.Configuration.ProxyCreationEnabled = false;
            //takes the id of the post and serach this id in the enrrollments table and merge them, then we take this result and we search the id in the comments table and merge it then we get the table of the comments of the specified post
            var results = from a in db.Posts
                          join b in db.Enrollments on a.ID equals b.PostID
                          join c in db.Comments on b.CommentID equals c.CommentID
                                      //we filter the lines of the table according to the the given parameters (of the function) in our new table
                          where a.Title.Equals(t_title) || a.WriterName.Equals(t_author) || c.WriterName.Equals(t_cauthor)
                          select new
                          {//create new table with the filters data
                              PostTitle = a.Title,
                              PostAuthorName = a.WriterName,
                              CommentAuthorName = c.WriterName,
                              CommentContent = c.Content
                          };
            return Json(results);
        }
    }
}