﻿using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO
{
    public class LoginHistory : ILoginHistory
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ClientType { get; set; }
        public DateTime Time { get; set; }
    }
}
