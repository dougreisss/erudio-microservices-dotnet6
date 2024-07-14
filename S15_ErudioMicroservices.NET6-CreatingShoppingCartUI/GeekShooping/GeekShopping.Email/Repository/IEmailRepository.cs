using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage updatePaymentResultMessage);
    }
}
