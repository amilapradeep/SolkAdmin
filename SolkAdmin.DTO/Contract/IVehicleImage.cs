using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO.Contract
{
    public interface IVehicleImage
    {
        long Id { get; set; }
        long SRId { get; set; }
        DateTime Date { get; set; }
        byte[] ImageBytes { get; set; }
        int? Order { get; set; }
    }
}
