using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShopping.PaymentProcessor
{
    public class ProcesserPayment : IProcesserPayment
    {
        public bool PaymentProcessor()
        {
            return true;
        }
    }
}
