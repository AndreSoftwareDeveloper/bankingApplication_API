using System.Text.Json.Serialization;
using bankingApplication_API.Controllers;

namespace bankingApplication_API.Dto
{
    public class NaturalPersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly BirthDate { get; set; }

        public string BirthPlace { get; set; }
        public string Address { get; set; }
        public string Pesel { get; set; }
        public string IdCardNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int VerificationToken { get; set; } = UniqueNumberGenerator.GenerateVerificationToken(typeof(NaturalPersonController));
        public long? Nip { get; set; } = null;
        public long? Regon { get; set; } = null;
        public int CustomerNumber { get; set; } = UniqueNumberGenerator.GenerateCustomerNumber(typeof(NaturalPersonController));
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
