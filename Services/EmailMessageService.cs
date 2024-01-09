using System.Net;
using System.Net.Mail;

namespace bankingApplication_API.Services
{   class EmailMessageService
    {
        public static void SendEmail(string emailHeader, string emailContent)
        {
            MailAddress to = new MailAddress("andriej2301@gmail.com");
            MailAddress from = new MailAddress("jka204@wp.pl");

            MailMessage email = new MailMessage(from, to);
            email.Subject = emailHeader;
            email.Body = emailContent;

            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            smtp.Host = "smtp.wp.pl"; //gmail has security policy, that prevents automatically sending an email from api, while wp.pl is ok
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("jka204@wp.pl", "szCZepionka70%");
            //smtp.Credentials = new NetworkCredential("murzyn448@gmail.com", "Bitcoinminer");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
