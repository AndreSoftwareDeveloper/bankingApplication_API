using System.Globalization;

namespace bankingApplication_API.Validators
{
    public class NaturalPersonValidator
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateOnly birthDate { get; set; }
        public string birthPlace { get; set; }
        public string address { get; set; }
        public string pesel { get; set; }
        public string idCardNumber { get; set; }
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public string? password { get; set; }
        public int verificationToken { get; set; }
        public long? nip { get; set; }
        public long? regon { get; set; }
        public int customerNumber { get; set; }
        public DateTime creationTime { get; set; }

        private static CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        private static TextInfo textInfo = cultureInfo.TextInfo;

        public NaturalPersonValidator(
            string firstName, string lastName, DateOnly birthDate, string birthPlace, string address, string pesel, string idCardNumber,
            int phoneNumber, string email, string password, int verificationToken, long? nip, long? regon, int customerNumber)
        {
            this.firstName = textInfo.ToTitleCase(firstName);
            this.lastName = textInfo.ToTitleCase(lastName);
            this.birthDate = birthDate;
            this.birthPlace = textInfo.ToTitleCase(birthPlace);
            this.address = textInfo.ToTitleCase(address);
            this.pesel = pesel;
            this.idCardNumber = textInfo.ToUpper(idCardNumber);
            this.phoneNumber = phoneNumber;
            this.email = textInfo.ToLower(email);
            this.password = password;
            this.verificationToken = verificationToken;
            this.nip = nip;
            this.regon = regon;
            this.customerNumber = customerNumber;
            this.creationTime = DateTime.Now;
        }
    }
}
