using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Rules
{
    public class RuleSet
    {

        public IEnumerable<IRules> Rules;
        public IPaymentGateway PaymentGateway;
    }
}
