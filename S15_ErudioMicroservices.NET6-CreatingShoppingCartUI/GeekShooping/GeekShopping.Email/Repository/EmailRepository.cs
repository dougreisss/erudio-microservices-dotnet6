using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;

        public EmailRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }

        public async Task LogEmail(UpdatePaymentResultMessage updatePaymentResultMessage)
        {
            EmailLog email = new()
            {
                Email = updatePaymentResultMessage.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {updatePaymentResultMessage.OrderId} has been created succesfully!"
            };

            await using var _db = new MySQLContext(_context);
             
            _db.Emails.Add(email);
            
            await _db.SaveChangesAsync();
            
        }

    }
}
