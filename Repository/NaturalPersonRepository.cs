using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;

namespace bankingApplication_API.Repository
{
    public class NaturalPersonRepository : INaturalPersonInterface
    {
        private readonly DataContext _context;

        public NaturalPersonRepository(DataContext context)
        {
            _context = context;
        }

        public NaturalPerson GetNaturalPerson(int id)
        {
            return _context.naturalPerson.Where(np => np.id == id).FirstOrDefault();
        }

        public NaturalPerson GetNaturalPerson(string LastName)
        {
            return _context.naturalPerson.Where(np => np.lastName == LastName).FirstOrDefault();
        }

        public ICollection<NaturalPerson> GetNaturalPersons()
        {
            return _context.naturalPerson.OrderBy( np => np.id ).ToList();
        }
        public void CreateNaturalPerson(NaturalPerson naturalPerson)
        {
            _context.naturalPerson.Add(naturalPerson);
            _context.SaveChanges();
        }

        public bool NaturalPersonExists(int id)
        {
            return _context.naturalPerson.Any(np => np.id == id);
        }     
        
        public bool VerificationTokenExists(int newToken)
        {
            var person = _context.naturalPerson.SingleOrDefault(p => p.verificationToken == newToken);
            return person != null;
        }

        public NaturalPerson SetupNaturalPersonData(int verificationToken, string newPassword, long nip) //returns id of the person with specified verification token
        {
            NaturalPerson updatedPerson = _context.naturalPerson.Where(np => np.verificationToken == verificationToken).FirstOrDefault(); //todo check if updatedPerson isn't null

            updatedPerson.password = newPassword;
            updatedPerson.nip = nip;

            _context.SaveChanges();
            return updatedPerson;
        }
    }
}
