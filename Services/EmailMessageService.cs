using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

namespace bankingApplication_API.Services
{   class EmailMessageService
    {
        public static void SendEmail(string emailHeader, string emailContent)
        {
            MailAddress from = new MailAddress("jka204@wp.pl");
            MailAddress to = new MailAddress("andriej2301@gmail.com");
            MailMessage email = new MailMessage(from, to);

            email.Subject = emailHeader;
            email.Body = emailContent;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.wp.pl"; //gmail has security policy, that prevents automatically sending an email from api, while wp.pl is ok
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587;
            smtp.EnableSsl = true;

            EmailCredentials credentials = LoadEmailCredentials();            
            smtp.Credentials = new NetworkCredential(credentials.Address, credentials.Password);

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

        private static EmailCredentials LoadEmailCredentials()
        {
            string secretsPath = "EmailCredentials.json"; //todo

            try
            {
                string secrets = File.ReadAllText(secretsPath);
                EmailCredentials emailCredentials = JsonConvert.DeserializeObject<EmailCredentials>(secrets);
                return emailCredentials;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while loading email credentials: {ex.Message}");
            }
        }
    }

    struct EmailCredentials
    {
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
