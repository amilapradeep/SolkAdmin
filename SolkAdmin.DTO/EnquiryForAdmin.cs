using System;
using System.Collections.Generic;

namespace SolkAdmin.DTO
{
    public class EnquiryForAdmin
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

       public IEnumerable<EnquiryForAdminDetail> EnquiryForAdminDetails { get; set; }
    }
}
