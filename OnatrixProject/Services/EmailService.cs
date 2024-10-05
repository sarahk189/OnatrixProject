using Azure.Communication.Email;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.Email;

namespace OnatrixProject.Services
{
    public class EmailService
    {
        

        public async Task<bool> SendEmailAsync(string email, string name)
        {

            var connectionString = "endpoint=https://cs-onatrix-communication.europe.communication.azure.com/;accesskey=5bFp8IOC8ayBRNdjUzYGkQwCz6gFObjYNHPca5GTxaTc3wC75b4aJQQJ99AJACULyCpq7IbPAAAAAZCSMnnJ";

            var emailClient = new EmailClient(connectionString);

            var emailContent = new EmailContent($"Hello {name}!")
            {
                PlainText = "Thank you for requesting an email from us!\nWe will get back to you shortly.\n\n-Onatrix",
                Html = "<p>Thank you for requesting an email from us!</p><p>We will get back to you shortly.</p><p>-Onatrix</p>"
            };

            var emailMessage = new Azure.Communication.Email.EmailMessage(
                senderAddress: "DoNotReply@7f793857-98b5-4bae-8dc5-5a65c3177348.azurecomm.net", 
                content: emailContent,
                recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(email) })
            );

            try
            {
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);

                return emailSendOperation.HasCompleted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
