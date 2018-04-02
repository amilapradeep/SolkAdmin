﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DTO.Contract
{
    public interface IMessageThread
    {
        long Id { get; set; }       
        long RequestId { get; set; }
        long BuyerId { get; set; }
        long AgentId { get; set; }
        long CreatedBy { get; set; }
        DateTime CreatedTime { get; set; }

        List<Message> Messages { get; set; }
    }
}
