using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

namespace bankingApplication_API.Services
{   class EmailMessageService
    {
        public static void SendConfigurationMessage(int verificationToken, int customerNumber, string receiverAddress, string ceidgInfo = "")
        {
            string header = "Prośba o Uzupełnienie Danych w Celu Aktywacji Konta w TwójBank";
            string message = $"Szanowny Kliencie,\n" +
                "Serdecznie witamy Cię w TwójBank! Dziękujemy za założenie konta w naszym banku. Abyś mógł pełnić korzyści z naszych usług, prosimy o kilka dodatkowych informacji.\n" +
                "W celu zabezpieczenia Twojego konta, zalecamy natychmiastową zmianę tymczasowego hasła przydzielonego podczas rejestracji.\n" +
                "Jeśli zakładasz konto jako osoba fizyczna, prosimy o uzupełnienie numeru NIP.\n" +
                "Te informacje są niezbędne do pełnej aktywacji Twojego konta.\n" +
                "Proszę użyj poniższego linku do uzupełnienia powyższych danych:\n" +
                "Link do uzupełnienia danych: http://localhost:8100/set_up_data?verificationToken=" + verificationToken + "\n" +
                "Zaloguj się z użyciem tego numeru klienta: " + customerNumber + ".\n" +
                "Zachowaj go, by używać go do logowania w przyszłości.\n" +
                "Proszę pamiętać, że link będzie aktywny przez godzinę od chwili wysłania tego e-maila. Po tym okresie będziesz musiał(a) skontaktować się z nami w celu uzyskania nowego linku.\n\n" +
                "Twoje bezpieczeństwo jest dla nas priorytetem, dlatego wykorzystujemy szyfrowane połączenia, aby zapewnić bezpieczeństwo Twoich danych.\n" +
                "Dziękujemy za zaufanie i wybór TwójBank. Jesteśmy gotowi służyć Ci najlepszymi usługami finansowymi.\n" +
                "W razie pytań lub problemów prosimy o kontakt z naszym działem obsługi klienta pod numerem [numer_telefonu] lub drogą mailową pod adresem [adres_email].\n\n" +
                ceidgInfo +
                "\n\nPozdrawiamy,\n" +
                "Zespół TwójBank";

            SendEmail(header, message, receiverAddress);
        }

        public static void SendEmail(string emailHeader, string emailContent, string emailReceiver)
        {
            EmailCredentials credentials = LoadSecrets();
            MailAddress from = new MailAddress(credentials.Address);
            MailAddress to = new MailAddress(emailReceiver);
            MailMessage email = new MailMessage(from, to);

            email.Subject = emailHeader;
            email.Body = emailContent;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.wp.pl"; //gmail has security policy, that prevents automatically sending an email from api, while wp.pl is ok
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587;
            smtp.EnableSsl = true;                        
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

        public static EmailCredentials LoadSecrets()
        {
            string secretsPath = "Secrets\\EmailCredentials.json";

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
        public string CEIDGToken { get; set; }
    }
}
