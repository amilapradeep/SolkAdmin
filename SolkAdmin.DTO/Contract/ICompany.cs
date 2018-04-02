using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO.Contract
{
    public interface ICompany
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }
    }
}
