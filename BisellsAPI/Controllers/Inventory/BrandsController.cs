using BisellsAPI.Filters;
using Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class BrandsController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {

            return Request.CreateResponse(HttpStatusCode.OK, Brand.GetDetails(CompanyId));
        }
    
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Brand.GetDetails(id, CompanyId));
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Brand brand = new Brand();
            brand.ID = id;
            brand.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, brand.Delete());
        }
     
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Brand brand)
        {
            if (brand.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, brand.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, brand.Update());
            }
        }
    }
}