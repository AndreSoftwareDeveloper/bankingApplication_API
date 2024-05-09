using bankingApplication_API.Data;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;

namespace bankingApplication_API.Repository
{
    public class JuridicalPersonRepository : IJuridicalPersonInterface
    {
        private readonly DataContext _context;

        public JuridicalPersonRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<JuridicalPerson> GetJuridicalPersons() {
            return _context.juridicalPerson.OrderBy( np => np.id ).ToList();
        }

        public void CreateJuridicalPerson(JuridicalPerson juridicalPerson)
        {
            _context.juridicalPerson.Add(juridicalPerson);
            _context.SaveChanges();
        }

        public bool JuridicalPersonExists(int id)
        {
            return _context.juridicalPerson.Any(np => np.id == id);
        }

        public JuridicalPerson GetJuridicalPersonByID(int id)
        {
            return _context.juridicalPerson.Where(np => np.id == id).FirstOrDefault();
        }

        public bool VerificationTokenExists(int newToken)
        {
            var person = _context.juridicalPerson.SingleOrDefault(p => p.verificationToken == newToken);
            return person != null;
        }

        public bool customerNumberExists(int customerNumber)
        {
            var person = _context.juridicalPerson.SingleOrDefault(p => p.customerNumber == customerNumber);
            return person != null;
        }

        public JuridicalPerson FindCustomerNumber(int customerNumber) //if return null, customer number is wrong
        {
            JuridicalPerson juridicalPerson = _context.juridicalPerson.Where(np => np.customerNumber == customerNumber).FirstOrDefault();
            return juridicalPerson;
        }
    }
}
