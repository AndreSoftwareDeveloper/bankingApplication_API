using System.Globalization;

namespace bankingApplication_API.Validators
{
    public class NaturalPersonValidator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Address { get; set; }
        public string Pesel { get; set; }
        public string IdCardNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int VerificationToken { get; set; }
        public long? Nip { get; set; }
        public long? Regon { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime CreationTime { get; set; }

        private static readonly CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        private static readonly TextInfo textInfo = cultureInfo.TextInfo;

        public NaturalPersonValidator(
            string firstName, string lastName, DateOnly birthDate, string birthPlace, string address, string pesel, string idCardNumber,
            int phoneNumber, string email, string password, int verificationToken, long? nip, long? regon, int customerNumber)
        {
            FirstName = textInfo.ToTitleCase(firstName);
            LastName = textInfo.ToTitleCase(lastName);
            BirthDate = birthDate;
            BirthPlace = textInfo.ToTitleCase(birthPlace);
            Address = textInfo.ToTitleCase(address);
            Pesel = pesel;
            IdCardNumber = textInfo.ToUpper(idCardNumber);
            PhoneNumber = phoneNumber;
            Email = textInfo.ToLower(email);
            Password = password;
            VerificationToken = verificationToken;
            Nip = nip;
            Regon = regon;
            CustomerNumber = customerNumber;
            CreationTime = DateTime.Now;
        }
    }
}
