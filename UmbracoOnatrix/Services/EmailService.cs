using Azure.Communication.Email;
using Azure;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;

namespace UmbracoOnatrix.Services;

public class EmailService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public bool SendEmail(string email)
    {
        bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        var connectionString = _configuration.GetValue<string>("ConnectionStrings:CommunicationServices");
        EmailClient emailClient = new EmailClient(connectionString);

        if (isEmail && !string.IsNullOrEmpty(email))
        {
            var result = emailClient.Send(WaitUntil.Completed,
                senderAddress: "DoNotReply@81db00d7-be6f-41c5-ba9d-27991446c584.azurecomm.net",
                recipientAddress: email,
                subject: "Onatrix will be in touch!",
                htmlContent: "We have been notified about your request for a call back. We will return to you as fast as we can!",
                plainTextContent: "We have been notified about your request for a call back. We will return to you as fast as we can!"
                );

            if (result.HasCompleted)
                return true;

            return false;
        }
        return false;
    }
}
