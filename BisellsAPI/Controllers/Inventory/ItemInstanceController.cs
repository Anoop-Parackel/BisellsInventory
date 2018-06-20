using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Master;


namespace BisellsAPI.Controllers
{
    public class ItemInstanceController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(ItemInstance it)
        {
            if (it.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, it.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, it.Save());
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            ItemInstance it = new ItemInstance();
            it.ID = Id;
            it.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, it.Delete());
        }

    }
}