using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using BootstrapIntroduction.Models;
using BootstrapIntroduction.DAL;
using System.Net;
using BootstrapIntroduction.ViewModels;

namespace BootstrapIntroduction.Controllers
{
    public class AuthorController : Controller
    {
        private BookContext db = new BookContext();

        //
        // GET: /Author/

        public ActionResult Index(QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            var authors = db.Authors.
            OrderBy(queryOptions.Sort).
            Skip(start).
            Take(queryOptions.PageSize);
            queryOptions.TotalPages = (int) Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            ViewBag.QueryOptions = queryOptions;

            AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();

            return View(AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList()));
        }

        //
        // GET: /Author/Details/5

        public ActionResult Details(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        //
        // GET: /Author/Create

        public ActionResult Create()
        {
            return View("Form", new AuthorViewModel());
        }

        //
        // POST: /Author/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
                db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        //
        // GET: /Author/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            return View("Form", AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        //
        // POST: /Author/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
                db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //
        // GET: /Author/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        //
        // POST: /Author/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}