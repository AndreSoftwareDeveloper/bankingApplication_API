using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankingApplication_API.Models
{
    [Table("JuridicalPerson")]
    public class JuridicalPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string correspondenceAddress { get; set; }
        public long nip { get; set; }
        public long regon { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public byte[] entryKRS { get; set; }
        public byte[] companyAgreement { get; set; }

        public string representativeFirstName { get; set; }
        public string representativeLastName { get; set; }
        public DateOnly representativeBirthDate { get; set; }
        public string representativeBirthPlace { get; set; }
        public string representativeAddress { get; set; }
        public string representativePesel { get; set; }
        public int representativePhone { get; set; }
        public string representativeEmail { get; set; }
        public string representativeIdNumber { get; set; }
        public byte[] representativeIdScan { get; set; }

        public string password { get; set; }
        public int verificationToken { get; set; }
        public int customerNumber { get; set; }
        public DateTime creationTime { get; set; }

        public JuridicalPerson(string companyName, string companyAddress, string correspondenceAddress,
            long nip, long regon, int phone, string email, byte[] entryKRS, byte[] companyAgreement, 
            string representativeFirstName, string representativeLastName, DateOnly representativeBirthDate,
            string representativeBirthPlace, string representativeAddress, string representativePesel, 
            int representativePhone, string representativeEmail, string representativeIdNumber, 
            byte[] representativeIdScan, string password, int verificationToken, int customerNumber)
        {
            this.companyName = companyName;
            this.companyAddress = companyAddress;
            this.correspondenceAddress = correspondenceAddress;
            this.nip = nip;
            this.regon = regon;
            this.phone = phone;
            this.email = email;
            this.entryKRS = entryKRS;
            this.companyAgreement = companyAgreement;
            this.representativeFirstName = representativeFirstName;
            this.representativeLastName = representativeLastName;
            this.representativeBirthDate = representativeBirthDate;
            this.representativeBirthPlace = representativeBirthPlace;
            this.representativeAddress = representativeAddress;
            this.representativePesel = representativePesel;
            this.representativePhone = representativePhone;
            this.representativeEmail = representativeEmail;
            this.representativeIdNumber = representativeIdNumber;
            this.representativeIdScan = representativeIdScan;
            this.password = password;
            this.verificationToken = verificationToken;
            this.customerNumber = customerNumber;
            this.creationTime = DateTime.Now;
        }
    }
}