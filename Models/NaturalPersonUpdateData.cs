namespace bankingApplication_API.Models
{
    public class NaturalPersonUpdateData
    {
        public int verificationToken { get; set; }
        public string newPassword { get; set; }
        public long nip { get; set; }
        public long regon {  get; set; }
    }
}
