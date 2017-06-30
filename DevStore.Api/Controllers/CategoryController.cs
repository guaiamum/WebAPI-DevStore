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
using DevStore.Domain;
using DevStore.Infra.DataContexts;

namespace DevStore.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class CategoryController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        
    }
}