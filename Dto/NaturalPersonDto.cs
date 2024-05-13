using System.Text.Json.Serialization;
using bankingApplication_API.Controllers;

namespace bankingApplication_API.Dto
{
    public class NaturalPersonDto
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly birthDate { get; set; }

        public string birthPlace { get; set; }
        public string address { get; set; }
        public string pesel { get; set; }
        public string idCardNumber { get; set; }
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public string? password { get; set; }
        public int verificationToken { get; set; } = UniqueNumberGenerator.GenerateVerificationToken(typeof(NaturalPersonController));
        public long? nip { get; set; } = null;
        public long? regon { get; set; } = null;
        public int customerNumber { get; set; } = UniqueNumberGenerator.GenerateCustomerNumber(typeof(NaturalPersonController));
        public DateTime creationTime { get; set; } = DateTime.Now;
    }
}
