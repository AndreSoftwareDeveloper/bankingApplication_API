using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankingApplication_API.Models
{
    [Table("NaturalPerson")]
    public class NaturalPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Address { get; set; }
        public string Pesel { get; set; }
        public string IdCardNumber { get; set; } //TODO separate class for IDCardNumber
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int VerificationToken { get; set; }
        public long? Nip { get; set; }
        public long? Regon { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
