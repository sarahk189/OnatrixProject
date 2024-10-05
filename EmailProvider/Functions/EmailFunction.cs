using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using EmailProvider.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailProvider.Functions
{
    public class EmailFunction
    {
        private readonly ILogger<EmailFunction> _logger;
        private readonly EmailClient _emailClient;

        public EmailFunction(ILogger<EmailFunction> logger, EmailClient emailClient)
        {
            _logger = logger;
            _emailClient = emailClient;
        }

        [Function(nameof(EmailFunction))]
        public async Task Run(
            [ServiceBusTrigger("email_request", Connection = "ServiceBusConnection")]
                ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            try
            {
                var emailRequest = UnpackEmailRequest(message);
                if (emailRequest != null && !string.IsNullOrEmpty(emailRequest.To))
                {
                    if (SendEmail(emailRequest))
                    {
                        await messageActions.CompleteMessageAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR : EmailFunction.Run :: {ex.Message}");
            }
        }

        public EmailRequest UnpackEmailRequest(ServiceBusReceivedMessage message)
        {
            try
            {
                var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(message.Body.ToString());
                if (emailRequest != null)
                {
                    return emailRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR : EmailFunction.UnpackEmailRequest :: {ex.Message}");
            }

            return null;
        }

        public bool SendEmail(EmailRequest emailRequest)
        {
            try
            {
                var result = _emailClient.Send(
                    Azure.WaitUntil.Completed,
                    senderAddress: Environment.GetEnvironmentVariable("SenderAddress"),
                    recipientAddress: emailRequest.To,
                    subject: emailRequest.Subject,
                    htmlContent: emailRequest.HtmlBody,
                    plainTextContent: emailRequest.PlainText);

 
                if (result.HasCompleted)
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR : EmailFunction.SendEmail :: {ex.Message}");
              
            }
            return false;
        }
    }
}
