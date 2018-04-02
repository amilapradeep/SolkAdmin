using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO
{
    public class ServiceProvider : SolkAdmin.DTO.Contract.IServiceProvider
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ServiceCategoryId { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual List<ServiceProviderLocation> Locations { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }
    }
}
