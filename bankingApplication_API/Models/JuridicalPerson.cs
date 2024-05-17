using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankingApplication_API.Models
{
    [Table("JuridicalPerson")]
    public class JuridicalPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }
}
