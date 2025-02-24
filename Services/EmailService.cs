using BootCampDAL.Data.Models;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace BootCampNetFullStack.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private EmailSendDTO emailSend;
        private UserManager<User> _userManager;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public EmailService(EmailSendDTO emailSend)
        {
            this.emailSend = emailSend;
        }

        public async Task<bool> SendEmailAsync(EmailSendDTO emailSend)
        {
            MailjetClient client = new MailjetClient(_config["MailJet:ApiKey"], _config["MailJet:SecretKey"]);
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_config["Email:From"], _config["Email:ApplicationName"]))
                .WithSubject(emailSend.Subject)
                .WithHtmlPart(emailSend.Body)
                .WithTo(new SendContact(emailSend.To))
                .Build();
            var response = await client.SendTransactionalEmailAsync(email);

            if(response.Messages != null)
            {
                if (response.Messages[0].Status == "success")
                {
                    return true;
                }
            }
            return false;

        }
        public async Task<bool> SendConfirmedEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["JWT:ConfirmationEmailPath"]}?token={token}&email={user.Email}";
            var body = $"<p>Bonjour {user.Prenom} {user.Nom}</p>" +
                        "<<p>Afin de pouvoir activer votre compte, nous devons valider votre adresse email. Cliquez simplement sur le lien :</p>" +
                        $"<p><a href='{url}'>Activer mon compte</a></p>" +
                        "<p>Bienvenue à bord !</p>";
            var emailSend = new EmailSendDTO(user.Email, "Confirmation", body);
            return await SendEmailAsync(emailSend);
        }
    }
}
