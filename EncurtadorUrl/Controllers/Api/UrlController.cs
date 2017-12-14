using EncurtadorUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EncurtadorUrl.Controllers.Api
{
    public class UrlController : ApiController
    {
        private ApplicationDbContext db;

        public UrlController()
        {
            this.db = new ApplicationDbContext();
        }

        //GET /api/url
        public IHttpActionResult GetUrl()
        {
            return Ok(db.UrlControl.ToList());
        }


        /// <summary>
        /// Cria a url encurtada e salva no banco caso a mesma já não tenha sido salva.
        /// caso a url já exista no banco então retorna o valor existente para a View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public string CreateUrl(string url)
        {

            bool existUrl = db.UrlControl.Any(w => w.Url == url);
            UrlControl urlControl = new UrlControl();

            if (!existUrl)
            {
                urlControl.Url = url;

                db.UrlControl.Add(urlControl);
                db.SaveChanges();

                return "lk" + urlControl.Id;
            }
            else
            {
                urlControl = db.UrlControl.Where(w => w.Url == url).FirstOrDefault();
                return "lk" + urlControl.Id;
            }

            
        }
    }
}
