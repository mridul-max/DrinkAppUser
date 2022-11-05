using System;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Text;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace Users.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(SendGridMessage msg)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
        private readonly Random random = new Random();
        public string RandomGenerateCode(int size,bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random random = new Random();
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;
            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        } 
        public string GenerateTokenCode()
        {
            var passwordBuilder = new StringBuilder(); 
            passwordBuilder.Append(RandomGenerateCode(6, true));
            passwordBuilder.Append(RandomNumber(1000, 9999));
            passwordBuilder.Append(RandomGenerateCode(2));
            return passwordBuilder.ToString();
        }
    }
}
