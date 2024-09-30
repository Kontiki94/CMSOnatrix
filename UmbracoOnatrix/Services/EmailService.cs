using Azure.Communication.Email;
using Azure;
using System.Text.RegularExpressions;

namespace UmbracoOnatrix.Services;

public class EmailService
{
    public bool SendEmail(string email)
    {
        bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        var connectionString = "endpoint=https://comserv-silicon.europe.communication.azure.com/;accesskey=qyAxzKYuzKtxm3xA9TsibPqOTgWU53lpU2NFvUp00NxoRPm3esELxFTo0ksvQOQVtXJbcZI7tOTCoxPS+6VuTw==";
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
