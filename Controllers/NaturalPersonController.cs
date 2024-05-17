using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using bankingApplication_API.Dto;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;
using bankingApplication_API.Services;
using bankingApplication_API.Validators;

namespace bankingApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaturalPersonController : Controller
    {
        public static INaturalPersonInterface _naturalPersonInterface; //should be private and non static
        private readonly IMapper _mapper;

        public NaturalPersonController(INaturalPersonInterface naturalPersonInterface, IMapper mapper)
        {
            _naturalPersonInterface = naturalPersonInterface;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NaturalPerson>))]
        [ProducesResponseType(400)]
        public IActionResult GetNaturalPersons()
        {
            var naturalPersons = _naturalPersonInterface.GetNaturalPersons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(naturalPersons);
        }


        [HttpGet( "{id}" )]
        [ProducesResponseType(200, Type = typeof(NaturalPerson))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNaturalPerson(int id)
        {
            if(!_naturalPersonInterface.NaturalPersonExists(id))
                return NotFound();

            var naturalPerson = _mapper.Map<NaturalPersonDto>(_naturalPersonInterface.GetNaturalPersonByID(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(naturalPerson);
        }
        

        [HttpGet("customerNumber/{customerNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult FindCustomerNumber(int customerNumber)
        {
            NaturalPerson naturalPerson = _naturalPersonInterface.FindCustomerNumber(customerNumber);
            if (naturalPerson == null)
                return NotFound();
            return Ok(naturalPerson);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NaturalPersonDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateNaturalPerson([FromForm] NaturalPersonDto naturalPersonDto)
        {
            if (naturalPersonDto == null)
                return BadRequest("Invalid data.");

            var validator = _mapper.Map<NaturalPersonValidator>(naturalPersonDto);
            var naturalPerson = _mapper.Map<NaturalPerson>(validator);
            ICollection<NaturalPerson> naturalPersons = _naturalPersonInterface.GetNaturalPersons();

            switch ( dataExist(naturalPersons, naturalPerson.Email, naturalPerson.IdCardNumber, naturalPerson.Pesel, naturalPerson.PhoneNumber) ) {
                case uniqueConstraintViolation.email:
                    return BadRequest("email");
                case uniqueConstraintViolation.idCard:
                    return BadRequest("idCard");
                case uniqueConstraintViolation.pesel:
                    return BadRequest("pesel");
                case uniqueConstraintViolation.phone:
                    return BadRequest("phone");
                default:
                    _naturalPersonInterface.CreateNaturalPerson(naturalPerson);
                    break;
            }
            
            int verificationToken = naturalPersonDto.VerificationToken;
            int customerNumber = naturalPersonDto.CustomerNumber;
            EmailMessageService.SendConfigurationMessage(verificationToken, customerNumber, naturalPerson.Email);
            return CreatedAtAction(nameof(GetNaturalPerson), new { naturalPerson.Id }, naturalPerson);
        }


        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SetupNaturalPersonData([FromBody] NaturalPersonUpdateData updateData)
        {
            var updatedPerson = _naturalPersonInterface.SetupNaturalPersonData(updateData);
            if (updatedPerson == null)
                return BadRequest("Failed to update natural person data.");            
            return Ok(updatedPerson);
        }


        private uniqueConstraintViolation dataExist(ICollection<NaturalPerson> naturalPersons, string email, string idCardNumber, string pesel, int phoneNumber)
        {
            uniqueConstraintViolation violation = uniqueConstraintViolation.none;

            bool emailExists = naturalPersons.Any(np => np.Email == email);
            if (emailExists)
                violation = uniqueConstraintViolation.email;                

            bool idCardExists = naturalPersons.Any(np => np.IdCardNumber == idCardNumber);
            if (idCardExists)
                violation = uniqueConstraintViolation.idCard;

            bool peselExists = naturalPersons.Any(np => np.Pesel == pesel);
            if (peselExists)
                violation = uniqueConstraintViolation.pesel;

            bool phoneNumberExists = naturalPersons.Any(np => np.PhoneNumber == phoneNumber);
            if (phoneNumberExists)
                violation = uniqueConstraintViolation.phone;

            return violation;
        }


        private enum uniqueConstraintViolation
        {
            none,
            email,
            idCard,
            pesel,
            phone
        }
    }
}
