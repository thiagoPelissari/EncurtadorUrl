using EncurtadorUrl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncurtadorUrl.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            this.db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UrlInfos()
        {
            return View();
        }

        public ActionResult UrlRedirect(int urlId)
        {
            var urlControl = db.UrlControl.Find(urlId);

            if(urlControl != null)
            {
                urlControl.Hits += 1;
                db.Entry(urlControl).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("http://" + urlControl.Url);
            }
            else
            {
                return View();
            }

            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}