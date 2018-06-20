using BisellsAPI.Filters;
using BisellsERP.Masters;
using Entities.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class JobsController : ApiController
    {
        /// <summary>
        /// save or update job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Job job)
        {
           if (job.ID>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, job.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, job.Save());

            }
        }
        /// <summary>
        /// get jobs of specific customer
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]int customer_id)
        {

            return Request.CreateResponse(HttpStatusCode.OK, Job.GetDetails(customer_id));
        }
        /// <summary>
        /// return specific job
        /// </summary>
        /// <param name="job_id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetJob([FromUri]int job_id)
        {

            return Request.CreateResponse(HttpStatusCode.OK, Job.GetJob(job_id));
        }
        /// <summary>
        /// delete job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Job job)
        {

            return Request.CreateResponse(HttpStatusCode.OK, job.Delete());
        }

        [HttpPost]
        public HttpResponseMessage UpdateStatus([FromBody]Job job)
        {   
            return Request.CreateResponse(HttpStatusCode.OK, job.UpdateStatus());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomer([FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Job.GetCustomer(CompanyID));
        }

        [HttpPost]
        public HttpResponseMessage GetCustomerwiseJob([FromUri]int Id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Job.GetCustomerwiseJob(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetJobForVoucher([FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Job.GetJobsForVoucher(CompanyID));
        }

        //public HttpResponseMessage AddTask([FromBody] Task task,[FromUri] int companyId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, task.Save());
        //}

        public HttpResponseMessage AddTask([FromBody]Entities.Application.Task task, [FromUri] int companyId)
        {
           return Request.CreateResponse(HttpStatusCode.OK, task.Save());
        }

        public HttpResponseMessage UpdateTask([FromBody]Entities.Application.Task task)
        {
            return Request.CreateResponse(HttpStatusCode.OK, task.Update());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTasks([FromUri]int JobId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Job.GetTasks(JobId));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddImageToTask([FromBody] Entities.Application.Task task)
        {
            return await System.Threading.Tasks.Task.Run(() => {
               return Request.CreateResponse(HttpStatusCode.OK, task.AddImage());
            });
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTask([FromUri]int TaskId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.Task.GetTask(TaskId));
        }

        [HttpPost]
        public HttpResponseMessage UpateTaskStatus(Entities.Application.Task t)
        {
            return Request.CreateResponse(HttpStatusCode.OK, t.UpdateStatus());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetEmployeeDetails([FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.Employee.GetEmployeeDetails(CompanyID));
        }
        [HttpDelete]
        public HttpResponseMessage DeleteTask([FromBody]Entities.Application.Task t)
        {
            return Request.CreateResponse(HttpStatusCode.OK, t.Delete());
        }

        [HttpDelete]
        public HttpResponseMessage DeleteImage([FromBody]Entities.Application.Task task)
        {
            return Request.CreateResponse(HttpStatusCode.OK, task.DeleteImage());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetJobs([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Job.GetJobList(CompanyId));
        }
    }
}