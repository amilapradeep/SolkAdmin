using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO
{
    public class UserPasscode : IUserPasscode
    {
        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
