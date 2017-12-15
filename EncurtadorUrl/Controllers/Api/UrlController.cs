using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EncurtadorUrl.Models;


namespace EncurtadorUrl.Controllers.Api
{
    public class UrlController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Cria a url encurtada e salva no banco caso a mesma já não tenha sido salva.
        /// caso a url já exista no banco então retorna o valor existente para a View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateUrl(string url)
        {

            bool existUrl = db.UrlControl.Any(w => w.Url == url);
            UrlControl urlControl = new UrlControl();

            if (!existUrl)
            {
                urlControl.Url = url;

                db.UrlControl.Add(urlControl);
                db.SaveChanges();

                var urlInfo = new
                {
                    url = "lk" + urlControl.Id,
                    hits = 0

                };

                return Ok(urlInfo);
            }
            else
            {
                urlControl = db.UrlControl.Where(w => w.Url == url).FirstOrDefault();
                var urlInfo = new
                {
                    url = "lk" + urlControl.Id,
                    hits = urlControl.Hits
                };

                return Ok(urlInfo);
            }


        }


        // GET: api/stats
        [HttpGet]
        public IHttpActionResult Stats()
        {
            var Info = new
            {
                hits = db.UrlControl.ToList().Sum(s => s.Hits),
                qtdUrl = db.UrlControl.Count()
            };

            return Ok(Info);
        }

        // GET: api/UrlControls
        public IQueryable<UrlControl> GetUrlControl()
        {
            return db.UrlControl;
        }

        // GET: api/UrlControls/5
        [ResponseType(typeof(UrlControl))]
        public IHttpActionResult GetUrlControl(int id)
        {
            UrlControl urlControl = db.UrlControl.Find(id);
            if (urlControl == null)
            {
                return NotFound();
            }

            return Ok(urlControl);
        }

        // PUT: api/UrlControls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUrlControl(int id, UrlControl urlControl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urlControl.Id)
            {
                return BadRequest();
            }

            db.Entry(urlControl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlControlExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UrlControls
        [ResponseType(typeof(UrlControl))]
        public IHttpActionResult PostUrlControl(UrlControl urlControl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UrlControl.Add(urlControl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = urlControl.Id }, urlControl);
        }

        // DELETE: api/UrlControls/5
        [ResponseType(typeof(UrlControl))]
        public IHttpActionResult DeleteUrlControl(int id)
        {
            UrlControl urlControl = db.UrlControl.Find(id);
            if (urlControl == null)
            {
                return NotFound();
            }

            db.UrlControl.Remove(urlControl);
            db.SaveChanges();

            return Ok(urlControl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrlControlExists(int id)
        {
            return db.UrlControl.Count(e => e.Id == id) > 0;
        }
    }
}
