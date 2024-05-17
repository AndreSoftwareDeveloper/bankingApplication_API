using bankingApplication_API.Models;

namespace bankingApplication_API.Interfaces
{
    public interface IJuridicalPersonInterface
    {
        public ICollection<JuridicalPerson> GetJuridicalPersons();
        public void CreateJuridicalPerson(JuridicalPerson juridicalPerson);
        public bool JuridicalPersonExists(int id);
        public JuridicalPerson GetJuridicalPersonByID(int id);
        public bool VerificationTokenExists(int newToken);
        public bool customerNumberExists(int customerNumber);
        public JuridicalPerson FindCustomerNumber(int customerNumber);
    }
}
