using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using bankingApplication_API.Validators;

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
    }
}
