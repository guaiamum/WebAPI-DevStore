using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevStore.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ProductController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();


        [Route("products")]
        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        

        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategory(int categoryId)
        {
            var result = db.Products.Include("Category").Where(e => e.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                var result = e.Message; //"Falha ao cadastrar produto"
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

    
    }
}