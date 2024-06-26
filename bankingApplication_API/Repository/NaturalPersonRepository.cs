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

        public NaturalPerson GetNaturalPerson(string LastName)
        {
            return _context.naturalPerson.Where(np => np.LastName == LastName).FirstOrDefault();
        }

        public NaturalPerson GetNaturalPersonByID(int id)
        {
            return _context.naturalPerson.Where(np => np.Id == id).FirstOrDefault();
        }

        public NaturalPerson GetNaturalPersonByPesel(string pesel)
        {
            return _context.naturalPerson.Where(np => np.Pesel == pesel).FirstOrDefault();
        }

        public NaturalPerson FindCustomerNumber(int customerNumber) //if return null, customer number is wrong
        {
            NaturalPerson naturalPerson = _context.naturalPerson.Where(np => np.CustomerNumber == customerNumber).FirstOrDefault();
            return naturalPerson;
        }

        public ICollection<NaturalPerson> GetNaturalPersons() {
            return _context.naturalPerson.OrderBy( np => np.Id ).ToList();
        }

        public void CreateNaturalPerson(NaturalPerson naturalPerson) {
            _context.naturalPerson.Add(naturalPerson);
            _context.SaveChanges();
        }

        public bool NaturalPersonExists(int id) {
            return _context.naturalPerson.Any(np => np.Id == id);
        }     
        
        public bool VerificationTokenExists(int newToken)
        {
            var person = _context.naturalPerson.SingleOrDefault(p => p.VerificationToken == newToken);
            return person != null;
        }

        public bool CustomerNumberExists(int customerNumber)
        {
            var person = _context.naturalPerson.SingleOrDefault(p => p.CustomerNumber == customerNumber);
            return person != null;
        }

        public NaturalPerson SetupNaturalPersonData(NaturalPersonUpdateData updateData) //returns id of the person with specified verification token
        {
            NaturalPerson updatedPerson = _context.naturalPerson.Where(np => np.VerificationToken == updateData.VerificationToken).FirstOrDefault(); //todo check if updatedPerson isn't null

            updatedPerson.Password = updateData.NewPassword;
            updatedPerson.Nip = updateData.Nip;
            updatedPerson.Regon = updateData.Regon;

            _context.SaveChanges();
            return updatedPerson;
        }
    }
}
