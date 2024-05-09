using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankingApplication_API.Models
{
    [Table("NaturalPerson")]
    public class NaturalPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateOnly birthDate { get; set; }
        public string birthPlace { get; set; }
        public string address { get; set; }
        public string pesel { get; set; }
        public string idCardNumber { get; set; } //TODO separate class for IDCardNumber
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public string? password { get; set; }
        public int verificationToken { get; set; }
        public long? nip { get; set; }
        public long? regon { get; set; }
        public int customerNumber { get; set; }
        public DateTime creationTime { get; set; }

        public NaturalPerson(
            string firstName, string lastName, DateOnly birthDate, string birthPlace, string address, string pesel, string idCardNumber,
            int phoneNumber, string email, string password, int verificationToken, long? nip, long? regon, int customerNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.birthPlace = birthPlace;
            this.address = address;
            this.pesel = pesel;
            this.idCardNumber = idCardNumber;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
            this.verificationToken = verificationToken;
            this.nip = nip;
            this.regon = regon;
            this.customerNumber = customerNumber;
            this.creationTime = DateTime.Now;
        }
    }
}
