using BisellsAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class PreferencesController : ApiController
    {
        /// <summary>
        /// Get Company details basis of company id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id)
        {

            return Request.CreateResponse(HttpStatusCode.OK,Entities.Master.Company.GetDetails(Id));
        }
        /// <summary>
        /// used to load states
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetStates([FromBody]int CompanyId, [FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)Entities.Master.State.GetStates(id, CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrganizationProfile(Entities.Master.Company comp)
        {
            
            return Request.CreateResponse(HttpStatusCode.OK, comp.Update());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetGeneralSettings()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.Settings.GetDetails());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTermsandCondition([FromUri]int KeyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.Settings.GetSetting(KeyID));
        }

        [HttpPost]
        public HttpResponseMessage UpdateGeneralSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveGeneralSettings());
        }

        [HttpPost]
        public HttpResponseMessage UpdateFinantialSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveFinantialSettings());
        }
        [HttpPost]
        public HttpResponseMessage UpdateEmailSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveEmailSettings());
        }
        [HttpPost]
        public async Task<HttpResponseMessage> TestConnections(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(()=> setting.TestConnection()));
        }
        [HttpPost]
        public HttpResponseMessage UpdateInvoiceTemplateSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveInvoiceTepmlateSettings());
        }
        [HttpPost]
        public HttpResponseMessage UpdateTermsAndConditonSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveTermsSettings());
        }
        [HttpPost]
        public HttpResponseMessage UpdateOtherSetting(Entities.Application.Settings setting)
        {

            return Request.CreateResponse(HttpStatusCode.OK, setting.SaveOtherSettings());
        }
    }
}