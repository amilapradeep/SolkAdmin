using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DbAccess.Repository
{
    public class ServiceProviderRepository : BaseRepository
    {
        public ServiceProviderRepository(AppDBContext context) : base(context)
        {                
        }

        public List<SolkAdmin.DTO.Contract.IServiceProvider> GetByServiceCategory(int ServiceCategoryId)
        {
            return context.ServiceProviders.Where(
                p => p.ServiceCategoryId == ServiceCategoryId).ToList<SolkAdmin.DTO.Contract.IServiceProvider>();
        }
    }
}
