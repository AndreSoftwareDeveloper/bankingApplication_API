using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using bankingApplication_API.Controllers;

namespace bankingApplication_API.Models
{
    public class JuridicalPersonDto
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string correspondenceAddress { get; set; }
        public long nip { get; set; }
        public long regon { get; set; }
        public int phone { get; set; }
        public string email { get; set; }

        [NotMapped]
        public IFormFile entryKRS { get; set; }

        [NotMapped]
        public IFormFile companyAgreement { get; set; }

        public string representativeFirstName { get; set; }
        public string representativeLastName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly representativeBirthDate { get; set; }
        public string representativeBirthPlace { get; set; }
        public string representativeAddress { get; set; }
        public string representativePesel { get; set; }
        public int representativePhone { get; set; }
        public string representativeEmail { get; set; }
        public string representativeIdNumber { get; set; }

        [NotMapped]
        public IFormFile representativeIdScan { get; set; }

        public string password { get; set; } = "temporary_password";
        public int verificationToken { get; set; } = UniqueNumberGenerator.GenerateVerificationToken(typeof(JuridicalPersonController));
        public int customerNumber { get; set; } = UniqueNumberGenerator.GenerateCustomerNumber(typeof(JuridicalPersonController));
        public DateTime creationTime { get; set; } = DateTime.Now;
    }
}
