using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;
using bankingApplication_API.Services;

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
            Func<IFormFile, byte[]> convertIFormFileToByteArray = (file) =>
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            };

            if (juridicalPersonDto == null)
                return BadRequest("Invalid data.");

            JuridicalPerson juridicalPerson = new JuridicalPerson(
                juridicalPersonDto.companyName,
                juridicalPersonDto.companyAddress,
                juridicalPersonDto.correspondenceAddress,
                juridicalPersonDto.nip,
                juridicalPersonDto.regon,
                juridicalPersonDto.phone,
                juridicalPersonDto.email,
                convertIFormFileToByteArray(juridicalPersonDto.entryKRS),
                convertIFormFileToByteArray(juridicalPersonDto.companyAgreement),
                juridicalPersonDto.representativeFirstName,
                juridicalPersonDto.representativeLastName,
                juridicalPersonDto.representativeBirthDate,
                juridicalPersonDto.representativeBirthPlace,
                juridicalPersonDto.representativeAddress,
                juridicalPersonDto.representativePesel,
                juridicalPersonDto.representativePhone,
                juridicalPersonDto.representativeEmail,
                juridicalPersonDto.representativeIdNumber,
                convertIFormFileToByteArray(juridicalPersonDto.representativeIdScan),
                juridicalPersonDto.password,
                juridicalPersonDto.verificationToken,
                juridicalPersonDto.customerNumber
            );

            ICollection<JuridicalPerson> juridicalPersons = _juridicalPersonInterface.GetJuridicalPersons();

            switch (dataExist(juridicalPersons, juridicalPerson.companyName, juridicalPerson.nip, juridicalPerson.regon, juridicalPerson.phone, juridicalPerson.email, juridicalPerson.entryKRS, juridicalPerson.companyAgreement))
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
            
            int verificationToken = juridicalPersonDto.verificationToken;
            int customerNumber = juridicalPersonDto.customerNumber;
            long nip = juridicalPersonDto.nip;
            string ceidgInfo = await CeigdInformationService.CallCeidgApi(nip);
            EmailMessageService.SendConfigurationMessage(verificationToken, customerNumber, ceidgInfo);
            return CreatedAtAction(nameof(GetJuridicalPerson), new { juridicalPerson.id }, juridicalPerson);
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

            bool companyNameExists = juridicalPerson.Any(np => np.companyName == companyName);
            if (companyNameExists)
                violation = uniqueConstraintViolation.companyName;

            bool nipExists = juridicalPerson.Any(np => np.nip == nip);
            if (nipExists)
                violation = uniqueConstraintViolation.nip;

            bool regonExists = juridicalPerson.Any(np => np.regon == regon);
            if (regonExists)
                violation = uniqueConstraintViolation.regon;

            bool companyPhoneExists = juridicalPerson.Any(np => np.phone == phone);
            if (companyPhoneExists)
                violation = uniqueConstraintViolation.phone;

            bool companyEmailExists = juridicalPerson.Any(np => np.email == email);
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
