using SolkAdmin.DbAccess;
using SolkAdmin.DbAccess.Repository;
using SolkAdmin.DTO;
using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace SolkAdmin.UI.Controllers
{
    public class ServiceRequestController : Controller
    {
        // GET: api/ServiceRequest
        public ActionResult Get()
        {
            IEnumerable<ServiceRequest> request = null;
            ServiceRequestModel model = null;
            

            //try
            //{
                using (AppDBContext context = new AppDBContext())
                {
                    request = new ServiceRequestRepository(context).GetAll();
                }

                return View(request);
            //}
            //catch (Exception ex)
            //{
            //    //Logger.Log(typeof(ServiceRequestController), ex.Message + ex.StackTrace, LogType.ERROR);
            //    return InternalServerError();
            //}
        }

        //// GET: api/ServiceRequest/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/ServiceRequest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ServiceRequest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ServiceRequest/5
        public void Delete(int id)
        {
        }
    }
}
