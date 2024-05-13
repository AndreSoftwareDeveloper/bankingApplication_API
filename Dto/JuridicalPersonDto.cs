using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using bankingApplication_API.Controllers;

namespace bankingApplication_API.Models
{
    public class JuridicalPersonDto
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
        public IFormFile EntryKRS { get; set; }

        [NotMapped]
        public IFormFile CompanyAgreement { get; set; }

        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly RepresentativeBirthDate { get; set; }
        public string RepresentativeBirthPlace { get; set; }
        public string RepresentativeAddress { get; set; }
        public string RepresentativePesel { get; set; }
        public int RepresentativePhone { get; set; }
        public string RepresentativeEmail { get; set; }
        public string RepresentativeIdNumber { get; set; }

        [NotMapped]
        public IFormFile RepresentativeIdScan { get; set; }

        public string Password { get; set; } = "temporary_password";
        public int VerificationToken { get; set; } = UniqueNumberGenerator.GenerateVerificationToken(typeof(JuridicalPersonController));
        public int CustomerNumber { get; set; } = UniqueNumberGenerator.GenerateCustomerNumber(typeof(JuridicalPersonController));
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
