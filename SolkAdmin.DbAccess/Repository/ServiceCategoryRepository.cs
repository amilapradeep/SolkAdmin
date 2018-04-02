﻿using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DbAccess.Repository
{
    public class ServiceCategoryRepository : BaseRepository
    {
        public ServiceCategoryRepository(AppDBContext context) : base(context)
        {            
        }

        public List<IServiceCategory> GetAll()
        {
            return context.ServiceCategories.Where(c => c.IsActive).ToList<IServiceCategory>();
        }

        public List<IServiceCategory> GetAllRoot()
        {
            return context.ServiceCategories.Where(c => c.IsActive && c.ParentId == null).ToList<IServiceCategory>();
        }
    }
}
