using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;
using bankingApplication_API.Services;
using bankingApplication_API.Validators;

namespace bankingApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuridicalPersonController : Controller
    {
        public static IJuridicalPersonInterface _juridicalPersonInterface; //TODO should be private and non static
        private readonly IMapper _mapper;

        public JuridicalPersonController(IJuridicalPersonInterface juridicalPersonInterface, IMapper mapper)
        {
            _juridicalPersonInterface = juridicalPersonInterface;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JuridicalPerson>))]
        [ProducesResponseType(400)]
        public IActionResult GetJuridicalPersons()
        {
            var juridicalPersons = _juridicalPersonInterface.GetJuridicalPersons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(juridicalPersons);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(JuridicalPerson))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetJuridicalPerson(int id)
        {
            if (!_juridicalPersonInterface.JuridicalPersonExists(id))
                return NotFound();

            var juridicalPerson = _mapper.Map<JuridicalPersonDto>(_juridicalPersonInterface.GetJuridicalPersonByID(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(juridicalPerson);
        }
        

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(JuridicalPersonDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateJuridicalPerson([FromForm] JuridicalPersonDto juridicalPersonDto)
        {
            if (juridicalPersonDto == null)
                return BadRequest("Invalid data.");

            var validator = _mapper.Map<JuridicalPersonValidator>(juridicalPersonDto);
            var juridicalPerson = _mapper.Map<JuridicalPerson>(validator);
            ICollection<JuridicalPerson> juridicalPersons = _juridicalPersonInterface.GetJuridicalPersons();

            switch (dataExist(juridicalPersons, juridicalPerson.CompanyName, juridicalPerson.Nip, juridicalPerson.Regon, juridicalPerson.Phone, juridicalPerson.Email, juridicalPerson.EntryKRS, juridicalPerson.CompanyAgreement))
            {
                case uniqueConstraintViolation.companyName:
                    return BadRequest("companyName");
                case uniqueConstraintViolation.nip:
                    return BadRequest("nip");
                case uniqueConstraintViolation.regon:
                    return BadRequest("regon");
                case uniqueConstraintViolation.phone:
                    return BadRequest("phone");
                case uniqueConstraintViolation.email:
                    return BadRequest("email");
                default:
                    _juridicalPersonInterface.CreateJuridicalPerson(juridicalPerson);
                    break;
            }
            
            int verificationToken = juridicalPersonDto.VerificationToken;
            int customerNumber = juridicalPersonDto.CustomerNumber;
            long nip = juridicalPersonDto.Nip;
            string ceidgInfo = await CeigdInformationService.CallCeidgApi(nip);
            EmailMessageService.SendConfigurationMessage(verificationToken, customerNumber, juridicalPerson.Email, ceidgInfo);
            return CreatedAtAction(nameof(GetJuridicalPerson), new { juridicalPerson.Id }, juridicalPerson);
        }


        [HttpGet("customerNumber/{customerNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult FindCustomerNumber(int customerNumber)
        {
            JuridicalPerson juridicalPerson = _juridicalPersonInterface.FindCustomerNumber(customerNumber);
            if (juridicalPerson == null)
                return NotFound();
            return Ok(juridicalPerson);
        }


        private uniqueConstraintViolation dataExist(ICollection<JuridicalPerson> juridicalPerson, string companyName, long nip,
            long regon, int phone, string email, byte[] entryKRS, byte[] companyAgreement)
        {
            uniqueConstraintViolation violation = uniqueConstraintViolation.none;

            bool companyNameExists = juridicalPerson.Any(np => np.CompanyName == companyName);
            if (companyNameExists)
                violation = uniqueConstraintViolation.companyName;

            bool nipExists = juridicalPerson.Any(np => np.Nip == nip);
            if (nipExists)
                violation = uniqueConstraintViolation.nip;

            bool regonExists = juridicalPerson.Any(np => np.Regon == regon);
            if (regonExists)
                violation = uniqueConstraintViolation.regon;

            bool companyPhoneExists = juridicalPerson.Any(np => np.Phone == phone);
            if (companyPhoneExists)
                violation = uniqueConstraintViolation.phone;

            bool companyEmailExists = juridicalPerson.Any(np => np.Email == email);
            if (companyEmailExists)
                violation = uniqueConstraintViolation.email;

            return violation;
        }

        private enum uniqueConstraintViolation
        {
            none,
            companyName,
            nip,
            regon,
            phone,
            email
        }
    }
}
