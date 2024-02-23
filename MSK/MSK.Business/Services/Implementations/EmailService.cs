using Microsoft.Extensions.Options;
using MSK.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MSK.Core.Models;

namespace MSK.Business.Services.Implementations
{
    public class EmailService:IEmailService
    {
        private const string TemplatePath = @"./Templates/{0}.html";
        private readonly SMTPConfigModel _sMTPConfig;

        public async Task SendEmailToUserForConfirmation(UserEmailOption uSerEmailOptions)
        {
            uSerEmailOptions.Subject = UpdatePlaceHolders($"hello //UserName// , confirm your email id ", uSerEmailOptions.PlaceHolders);
            uSerEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), uSerEmailOptions.PlaceHolders);
            await SendEmail(uSerEmailOptions);
        }
        public async Task SendEmailForForgetPassword(UserEmailOption uSerEmailOptions)
        {
            uSerEmailOptions.Subject = UpdatePlaceHolders($"hello //UserName// , forget your password", uSerEmailOptions.PlaceHolders);
            uSerEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgetPassword"), uSerEmailOptions.PlaceHolders);
            await SendEmail(uSerEmailOptions);
        }
        public EmailService(IOptions<SMTPConfigModel> sMTPConfig)
        {
            _sMTPConfig = sMTPConfig.Value;
        }
        private async Task SendEmail(UserEmailOption uSerEmailOptions)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Subject = uSerEmailOptions.Subject,
                Body = uSerEmailOptions.Body,
                From = new MailAddress(_sMTPConfig.SenderAdress, _sMTPConfig.SenderDisplayName),
                IsBodyHtml = _sMTPConfig.IsBodyHTML,

            };
            foreach (var tomail in uSerEmailOptions.ToEmails)
            {
                mailMessage.To.Add(tomail);
            }
            NetworkCredential networkCredential = new NetworkCredential
                (_sMTPConfig.UserName, _sMTPConfig.Password);
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _sMTPConfig.Host,
                Port = _sMTPConfig.Port,
                EnableSsl = _sMTPConfig.EnableSSL,
                UseDefaultCredentials = _sMTPConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);

        }

        private string GetEmailBody(string tempName)
        {
            var result = File.ReadAllText(string.Format(TemplatePath, tempName));
            return result;

        }
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

    }
}
