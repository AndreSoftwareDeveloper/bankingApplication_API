using bankingApplication_API.Models;

namespace bankingApplication_API.Interfaces
{
    public interface INaturalPersonInterface
    {
        public ICollection<NaturalPerson> GetNaturalPersons();
        public NaturalPerson GetNaturalPersonByID(int id);
        public NaturalPerson GetNaturalPersonByPesel(string pesel);
        public NaturalPerson FindCustomerNumber(int customerNumber);
        public NaturalPerson GetNaturalPerson(string LastName);
        public bool NaturalPersonExists(int id);

        public void CreateNaturalPerson(NaturalPerson naturalPerson);
        public bool VerificationTokenExists(int newToken);
        public bool CustomerNumberExists(int customerNumber);
        public NaturalPerson? SetupNaturalPersonData(NaturalPersonUpdateData updateData);
    }
}
