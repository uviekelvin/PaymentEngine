using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Domain.Models
{

    public enum PaymentStatesOptions
    {
        Pending = 1,
        Processed = 2,
        Failed = 3
    }
}
