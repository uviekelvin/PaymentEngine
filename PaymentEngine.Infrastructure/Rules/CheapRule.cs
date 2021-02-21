﻿using PaymentEngine.Infrastructure.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Rules
{
    public class CheapRule : IRules
    {
        public bool IsValid(decimal amount)
        {
            return amount <= 20;
        }
    }
}
