﻿using SolkAdmin.DTO;
using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DbAccess.Repository
{
    public class UserPasscodeRepository : BaseRepository
    {
        public UserPasscodeRepository(AppDBContext context) : base(context)
        {
        }

        public void Add(IUserPasscode Code)
        {
            try
            {
                UserPasscode code = new UserPasscode();
                code.Code = Code.Code;
                code.Phone = Code.Phone;
                code.Name = Code.Name;
                code.Time = DateTime.Now.ToUniversalTime();

                context.UserPasscodes.Add(code);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                var error = ex;
            }
            
        }

        public IUserPasscode GetByPhone(string Phone)
        {
            return context.UserPasscodes.Where(p => p.Phone.Equals(Phone)).OrderByDescending(k => k.Time).FirstOrDefault();
        }
    }
}
