using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO
{
    public class EnquiryForAdmin
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string EnquiryStatus { get; set; }
        public string UserPhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string VehicleNo { get; set; }
        public string RegistrationCategory { get; set; }
        public string VehicleValue { get; set; }
        public string ClaimType { get; set; }
        public string UsageType { get; set; }
        public int VehicleYear { get; set; }
        public string IsFinanced { get; set; }
        public string Location { get; set; }
        public string CreatedTime { get; set; }
        public string ExpireTime { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ContactMethod { get; set; }
        public string RequestOrigin { get; set; }

        public string IsQuoteSent { get; set; }
        public string LastQuoteSentDate { get; set; }

        public string QuotationText { get; set; }

    }
}
