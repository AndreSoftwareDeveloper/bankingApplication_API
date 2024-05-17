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
        public byte[] EntryKRS { get; set; }
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
        public byte[] RepresentativeIdScan { get; set; }
        public string Password { get; set; }
        public int VerificationToken { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime CreationTime { get; set; }

        private static readonly CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        private static readonly TextInfo textInfo = cultureInfo.TextInfo;

        public JuridicalPersonValidator(string companyName, string companyAddress, string correspondenceAddress,
            long nip, long regon, int phone, string email, IFormFile entryKRS, IFormFile companyAgreement,
            string representativeFirstName, string representativeLastName, DateOnly representativeBirthDate,
            string representativeBirthPlace, string representativeAddress, string representativePesel,
            int representativePhone, string representativeEmail, string representativeIdNumber,
            IFormFile representativeIdScan, string password, int verificationToken, int customerNumber)
        {
            CompanyName = textInfo.ToTitleCase(companyName);
            CompanyAddress = textInfo.ToTitleCase(companyAddress);
            CorrespondenceAddress = textInfo.ToTitleCase(correspondenceAddress);
            Nip = nip;
            Regon = regon;
            Phone = phone;
            Email = textInfo.ToLower(email);
            EntryKRS = convertIFormFileToByteArray(entryKRS);
            CompanyAgreement = convertIFormFileToByteArray(companyAgreement);
            RepresentativeFirstName = textInfo.ToTitleCase(representativeFirstName);
            RepresentativeLastName = textInfo.ToTitleCase(representativeLastName);
            RepresentativeBirthDate = representativeBirthDate;
            RepresentativeBirthPlace = textInfo.ToTitleCase(representativeBirthPlace);
            RepresentativeAddress = textInfo.ToTitleCase(representativeAddress);
            RepresentativePesel = representativePesel;
            RepresentativePhone = representativePhone;
            RepresentativeEmail = textInfo.ToLower(representativeEmail);
            RepresentativeIdNumber = textInfo.ToUpper(representativeIdNumber);
            RepresentativeIdScan = convertIFormFileToByteArray(representativeIdScan);
            Password = password;
            VerificationToken = verificationToken;
            CustomerNumber = customerNumber;
            CreationTime = DateTime.Now;
        }

        private readonly Func<IFormFile, byte[]> convertIFormFileToByteArray = (file) =>
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        };
    }
}
