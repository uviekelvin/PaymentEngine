using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Rules.Interfaces
{
    public interface IRules
    {
        bool IsValid(decimal amount);
    }
}
