using Azure;
using System.Net.Mail;
using Umbraco.Cms.Core.Models.Email;

namespace OnatrixProject.Services
{
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(string email, string name)
        {
            //var connectionString = "endpoint=https://cs-umbrella.europe.communication.azure.com/;accesskey=DQTxfGFwtcuvUAYCb1iHa1QYwSSsnHA2YRIyU6tJmLLepwqqD2dQJQQJ99AIACULyCpq7IbPAAAAAZCSyTbw";
            //var emailClient = new EmailClient(connectionString);
            //var emailContent = new EmailContent(name + ", yo dawg!")
            //{
            //    PlainText = "This is an email sent to you from me.\nRemember to brush your teeth.\n\nMuch love!",
            //    Html = "<p>This is an email sent to you from me.</p>\n<p>Remember to brush your teeth.</p>\n<p>Much love!</p>"
            //};
            //var emailMessage = new EmailMessage(
            //    senderAddress: "donotreply@6e360ee1-2eca-4fa2-93fc-cc39e3f19d6a.azurecomm.net",
            //content: emailContent,
            //    recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(email) })
            //);
            //try
            //{
            //    EmailSendOperation emailSendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
                //return emailSendOperation.HasCompleted;
                return true;
        //}
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        }
    }
}
