using BisellsAPI.Filters;
using Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class CategoriesController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {

            return Request.CreateResponse(HttpStatusCode.OK, Category.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Category.GetDetails(id, CompanyId));
        }
        
        //public void Post([FromBody]string value)
        //{
        //}
        //public void Put(int id, [FromBody]string value)
        //{
        //}
       
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Category category = new Category();
            category.ID = id;
            category.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, category.Delete());

        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]Category Category)
        {
            if (Category.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Category.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Category.Update());
            }
        }
    }
}