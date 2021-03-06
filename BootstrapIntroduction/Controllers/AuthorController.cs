﻿using System;
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
using BootstrapIntroduction.Filters;
using System.Web.ModelBinding;
using BootstrapIntroduction.Services;

namespace BootstrapIntroduction.Controllers
{
    [RoutePrefix("Writer")]
    public class AuthorController : Controller
    {
        //private BookContext db = new BookContext();
        private AuthorService authorService;

        public AuthorController()
        {
            authorService = new AuthorService();
            AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
        }

        //
        // GET: /Author/
        [GenerateResultListFilterAttribute(typeof(Author), typeof(AuthorViewModel))]
        //[Route("~/Writers")]
        public ActionResult Index([Form]QueryOptions queryOptions)
        {
            //var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            //var authors = db.Authors.
            //OrderBy(queryOptions.Sort).
            //Skip(start).
            //Take(queryOptions.PageSize);

            //queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            ////ViewBag.QueryOptions = queryOptions;
            //ViewData["QueryOptions"] = queryOptions;

            //AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();

            //return View(
            //    new ResultList<AuthorViewModel>
            //    {
            //        QueryOptions = queryOptions,
            //        Results = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
            //    });

            var authors = authorService.Get(queryOptions);
            ViewData["QueryOptions"] = queryOptions;
            return View(authors.ToList());
        }

        //
        // GET: /Author/Details/5

        [BasicAuthorization]
        [Route("Details/{id:int:min(0)?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Author author = db.Authors.Find(id);
            //if (author == null)
            //{
            //    //return HttpNotFound();
            //    throw new System.Data.Entity.Core.ObjectNotFoundException
            //    (string.Format("Unable to find author with id {0}", id));
            //}

            //AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            var author = authorService.GetById(id.Value);
            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        [Route("Details/{id:int:min(0)?}")]
        public ActionResult GetById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);

            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        //[Route("Details/{name}")]
        public ActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetByName(name);
            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
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
                //AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
                //db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
                //db.SaveChanges();
                authorService.Insert(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
                return RedirectToAction("Index");
            }

            //return View(author);

            return View("Form", new AuthorViewModel());
        }

        //
        // GET: /Author/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Author author = db.Authors.Find(id);
            //if (author == null)
            //{
            //    return HttpNotFound();
            //}

            //AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            var author = authorService.GetById(id.Value);
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
                //AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
                //db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;
                //db.SaveChanges();
                authorService.Update(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //
        // GET: /Author/Delete/5

        public ActionResult Delete(int? id)
        {
            //Author author = db.Authors.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);
            //AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
            
            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        //
        // POST: /Author/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Author author = db.Authors.Find(id);
            //db.Authors.Remove(author);
            //db.SaveChanges();
            var author = authorService.GetById(id);
            authorService.Delete(author);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            if (disposing)
            {
                authorService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}