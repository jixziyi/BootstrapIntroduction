﻿using BootstrapIntroduction.DAL;
using BootstrapIntroduction.Models;
using BootstrapIntroduction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BootstrapIntroduction.Controllers.Api
{
    public class AuthorController : ApiController
    {
        private BookContext db = new BookContext();

        // GET: api/Authors
        public ResultList<AuthorViewModel> Get([FromUri] QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            var authors = db.Authors.OrderBy(queryOptions.Sort).
                Skip(start).Take(queryOptions.PageSize);
            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();

            return new ResultList<AuthorViewModel>{
                QueryOptions = queryOptions,
                Results = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>
                    (authors.ToList())
            };
        }

        // PUT: api/author/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
            db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State
                = System.Data.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult Post(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
            db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
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
