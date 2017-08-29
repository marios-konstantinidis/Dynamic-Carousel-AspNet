using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DynamicSlider.Models;
using System.IO;

namespace DynamicSlider.Controllers
{
    public class PagesController : Controller
    {
        private DynamicSliderContext db = new DynamicSliderContext();

        // GET: Pages
        public ActionResult Index()
        {
            return View(db.Pages.ToList());
        }

        // GET: Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
           List<Page> pages = db.Pages.ToList();
            List<Page> myPage = new List<Page>();
            foreach(Page page in pages)
            {
                if (page.Catagory == id)
                    myPage.Add(page);
            }
            if (myPage.Count == 0)
            {
                return HttpNotFound();
            }
            return View(myPage);
        }

        // GET: Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image , Page page)
        {
            string path = System.IO.Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            path.Substring(0, 8);  // Return 8 character string
            if (image.ContentLength > 0)
            {

                var fileName = Path.GetFileName(image.FileName);
                var x = Path.GetExtension(fileName);
                if (x == ".jpg") { path = path + ".jpg"; }
                else if (x == ".png") { path = path + ".png"; }
                else { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

                var path1 = Path.Combine(Server.MapPath("~/Content/images"), path);
                image.SaveAs(path1);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                page.ImgPath = path;
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(page);

        }

        // GET: Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImgPath,Heading,Text,Catagory")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
