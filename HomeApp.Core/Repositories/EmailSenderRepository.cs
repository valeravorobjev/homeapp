using System.IO;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Models;
using HomeApp.Core.Repositories.Contracts;
using MimeKit;
using MimeKit.Text;
using RazorLight;

namespace HomeApp.Core.Repositories
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        private readonly string _templatePath;
        public EmailSenderRepository(string templatePath)
        {
            _templatePath = templatePath;
        }

        public async Task SendEmailConfirmationAsync(string email, string callbackUrl, Language language)
        {
            var engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(_templatePath)
                .UseMemoryCachingProvider()
                .Build();

            var message = new MimeMessage();

            if (language == Language.En)
            {
                message.From.Add(new MailboxAddress("HOMEAPP PRO", "info@sadfasdf.ru"));
                message.Subject = "Registration confirmation";

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = await engine.CompileRenderAsync("ConfirmRu.cshtml", new object())
                };
            }
            else if (language == Language.Ru)
            {
                message.From.Add(new MailboxAddress("HOMEAPP PRO", "info@safsdfadsf.ru"));
                message.Subject = "Подтверждение регистрации";

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = await engine.CompileRenderAsync("ConfirmRu.cshtml", new EmailConfirmModel { Email = email, CallbackUrl = callbackUrl })
                };
            }

            message.To.Add(new MailboxAddress(email, email));

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mail.ru", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("info@asdfff.ru", "Tasfasdfadf");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
