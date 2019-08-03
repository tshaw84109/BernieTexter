using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BernieTexter
{
    class Texter
    {
        public Texter()
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC4595a923e0e7ea24af94e9e7ee966ee5";
            const string authToken = "35bae18cb01292d268b2ac4ffcdedc66";

            TwilioClient.Init(accountSid, authToken);
        }
            
        public void SendMessage(string text, string from, string to)
        {
            var message = MessageResource.Create(
                body: $"{text}",
                from: new Twilio.Types.PhoneNumber($"{from}"),
                to: new Twilio.Types.PhoneNumber($"{to}")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
