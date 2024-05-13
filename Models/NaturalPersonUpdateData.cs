namespace bankingApplication_API.Models
{
    public class NaturalPersonUpdateData
    {
        public int VerificationToken { get; set; }
        public string NewPassword { get; set; }
        public long Nip { get; set; }
        public long Regon {  get; set; }
    }
}
