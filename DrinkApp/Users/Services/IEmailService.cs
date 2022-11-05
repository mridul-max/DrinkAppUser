using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Services
{
    public interface IEmailService
    {
        Task SendEmail(SendGridMessage Body);
        string GenerateTokenCode();
    }
}
