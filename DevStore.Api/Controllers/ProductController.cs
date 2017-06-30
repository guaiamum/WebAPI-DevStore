using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DevStore.Api.Controllers
{
    [EnableCors(origins: "*",headers: "*", methods: "*")]
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
            catch (Exception)
            {
                var result = "Falha ao cadastrar produto";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            
        }

        [HttpPatch]
        [Route("products")]
        public HttpResponseMessage PatchProduct(Product product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Product>(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                var result = "Falha ao atualizar produto";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpPut]
        [Route("products")]
        public HttpResponseMessage PutProduct(Product product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Product>(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                var result = "Falha ao atualizar produto";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpDelete]
        [Route("products")]
        public HttpResponseMessage DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Products.Remove(db.Products.Find(productId));
                db.SaveChanges();

                var result = "Produto Excluído";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                var result = "Falha ao excluir produto";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

    
    }
}