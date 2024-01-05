using bankingApplication_API.Models;

namespace bankingApplication_API.Interfaces
{
    public interface INaturalPersonInterface
    {
        ICollection<NaturalPerson> GetNaturalPersons();
        NaturalPerson GetNaturalPerson(int id);
        NaturalPerson GetNaturalPerson(string LastName);
        bool NaturalPersonExists(int id);

        void CreateNaturalPerson(NaturalPerson naturalPerson);
        bool VerificationTokenExists(int newToken);
    }
}
