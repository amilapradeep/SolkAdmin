using SolkAdmin.DbAccess;
using SolkAdmin.DbAccess.Repository;
using SolkAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SolkAdmin.UI.Helpers;
using System.Threading.Tasks;

namespace SolkAdmin.UI.Controllers
{
    public class ServiceRequestController : Controller
    {
        private IEnumerable<EnquiryForAdminDetail> enquiryForAdminDetail;
        private EnquiryForAdmin enquiryForAdmin;

        // GET: api/ServiceRequest
        public ActionResult Get()
        {
            using (AppDBContext context = new AppDBContext())
            {
                enquiryForAdminDetail = new ServiceRequestRepository(context).GetAllForSendQuote(null, null, null, null, true);
            }

            enquiryForAdmin = new EnquiryForAdmin();
            enquiryForAdmin.EnquiryForAdminDetails = enquiryForAdminDetail;

            return View(enquiryForAdmin);
        }

        public ActionResult GetDetail(int Id)
        {
            using (AppDBContext context = new AppDBContext())
            {
                enquiryForAdminDetail = new ServiceRequestRepository(context).GetAllForSendQuote(Id, null, null, null, true);
            }
            EnquiryForAdminDetail model = enquiryForAdminDetail.Where(x => x.Id == Id).FirstOrDefault();

            return PartialView("SendQuote", model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<JsonResult> SubmitQuote(int Id, string QuotationText)
        {
            try
            {
                using (AppDBContext context = new AppDBContext())
                {
                    enquiryForAdminDetail = new ServiceRequestRepository(context).GetAllForSendQuote(Id, null, null, null, true);
                }

                EnquiryForAdminDetail model = enquiryForAdminDetail.Where(x => x.Id == Id).FirstOrDefault();

                await SendSMS.SendMessage(model.UserPhoneNumber, QuotationText);
                
                return Json(new { s = "Success" });
            }
            catch(Exception)
            {
                return Json(new { s = "Failed" });
            }
        }
    }
}
