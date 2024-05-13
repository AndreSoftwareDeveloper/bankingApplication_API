using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace bankingApplication_API.Validators
{
    public class JuridicalPersonValidator
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CorrespondenceAddress { get; set; }
        public long Nip { get; set; }
        public long Regon { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public byte[] EntryKRS { get; set; }

        [NotMapped]
        public byte[] CompanyAgreement { get; set; }

        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }
        public DateOnly RepresentativeBirthDate { get; set; }
        public string RepresentativeBirthPlace { get; set; }
        public string RepresentativeAddress { get; set; }
        public string RepresentativePesel { get; set; }
        public int RepresentativePhone { get; set; }
        public string RepresentativeEmail { get; set; }
        public string RepresentativeIdNumber { get; set; }

        [NotMapped]
        public byte[] RepresentativeIdScan { get; set; }

        public string Password { get; set; }
        public int VerificationToken { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime CreationTime { get; set; }

        private static CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        private static TextInfo textInfo = cultureInfo.TextInfo;

        public JuridicalPersonValidator(string companyName, string companyAddress, string correspondenceAddress,
            long nip, long regon, int phone, string email, byte[] entryKRS, byte[] companyAgreement,
            string representativeFirstName, string representativeLastName, DateOnly representativeBirthDate,
            string representativeBirthPlace, string representativeAddress, string representativePesel,
            int representativePhone, string representativeEmail, string representativeIdNumber,
            byte[] representativeIdScan, string password, int verificationToken, int customerNumber)
        {
            CompanyName = textInfo.ToTitleCase(companyName);
            CompanyAddress = textInfo.ToTitleCase(companyAddress);
            CorrespondenceAddress = textInfo.ToTitleCase(correspondenceAddress);
            Nip = nip;
            Regon = regon;
            Phone = phone;
            Email = textInfo.ToLower(email);
            EntryKRS = entryKRS;
            CompanyAgreement = companyAgreement;
            RepresentativeFirstName = textInfo.ToTitleCase(representativeFirstName);
            RepresentativeLastName = textInfo.ToTitleCase(representativeLastName);
            RepresentativeBirthDate = representativeBirthDate;
            RepresentativeBirthPlace = textInfo.ToTitleCase(representativeBirthPlace);
            RepresentativeAddress = textInfo.ToTitleCase(representativeAddress);
            RepresentativePesel = representativePesel;
            RepresentativePhone = representativePhone;
            RepresentativeEmail = textInfo.ToLower(representativeEmail);
            RepresentativeIdNumber = textInfo.ToUpper(representativeIdNumber);
            RepresentativeIdScan = representativeIdScan;
            Password = password;
            VerificationToken = verificationToken;
            CustomerNumber = customerNumber;
            CreationTime = DateTime.Now;
        }
    }
}
