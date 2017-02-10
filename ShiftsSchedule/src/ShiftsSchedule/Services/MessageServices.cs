using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace ShiftsSchedule.Services
{
    // This class is used by the application to send Email and SMS

    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Shifts Schedule", "shiftsschedule@yahoo.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };
            var credentials = new NetworkCredential(Options.MailUser, Options.MailKey);
            
            using (var client = new SmtpClient())
            {
                
                await client.ConnectAsync("smtp.mail.yahoo.com", 465, SecureSocketOptions.SslOnConnect).ConfigureAwait(false);
            //    await client.AuthenticateAsync(credentials);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
