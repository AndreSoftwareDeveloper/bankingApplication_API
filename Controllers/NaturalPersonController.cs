using AutoMapper;
using bankingApplication_API.Dto;
using bankingApplication_API.Interfaces;
using bankingApplication_API.Models;
using bankingApplication_API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetNaturalPersons()
        {
            var naturalPeople = _naturalPersonInterface.GetNaturalPersons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(naturalPeople);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(NaturalPerson))]
        [ProducesResponseType(400)]
        public IActionResult GetNaturalPerson(int id)
        {
            if(!_naturalPersonInterface.NaturalPersonExists(id))
                return NotFound();

            var naturalPerson = _mapper.Map<NaturalPersonDto>(_naturalPersonInterface.GetNaturalPerson(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(naturalPerson);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NaturalPersonDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateNaturalPersonAsync([FromBody] NaturalPersonDto naturalPersonDto)
        {
            if (naturalPersonDto == null)
                return BadRequest("Invalid data.");

            var naturalPerson = _mapper.Map<NaturalPerson>(naturalPersonDto);
            _naturalPersonInterface.CreateNaturalPerson(naturalPerson);
            string message = await CeigdInformationService.CallCeidgApi();
            int verificationToken = naturalPersonDto.verificationToken;
            SendConfigurationMessage(verificationToken);
            return CreatedAtAction(nameof(GetNaturalPerson), new { id = naturalPerson.id }, naturalPerson);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SetupNaturalPersonData([FromBody] NaturalPersonUpdateData updateData)
        {
            var updatedPerson = _naturalPersonInterface.SetupNaturalPersonData(updateData);
            return Ok(updatedPerson);
        }

        private void SendConfigurationMessage(int verificationToken)
        {
            string header = "Prośba o Uzupełnienie Danych w Celu Aktywacji Konta w TwójBank";
            string message = $"Szanowny Kliencie,\n" +
                "Serdecznie witamy Cię w TwójBank! Dziękujemy za założenie konta w naszym banku. Abyś mógł pełnić korzyści z naszych usług, prosimy o kilka dodatkowych informacji.\n" +
                "W celu zabezpieczenia Twojego konta, zalecamy natychmiastową zmianę tymczasowego hasła przydzielonego podczas rejestracji.\n" +
                "Ponadto, w celu dokończenia procesu rejestracji, prosimy o podanie numeru NIP. Ta informacja jest niezbędna do pełnej aktywacji Twojego konta.\n" +
                "Proszę użyj poniższego linku do uzupełnienia powyższych danych:\n" +
                "Link do uzupełnienia danych: http://localhost:8100/set_up_data?verificationToken=" + verificationToken + "\n\n" +
                "Proszę pamiętać, że link będzie aktywny przez 48 godzin od chwili wysłania tego e-maila. Po tym okresie będziesz musiał(a) skontaktować się z nami w celu uzyskania nowego linku.\n\n" +
                "Twoje bezpieczeństwo jest dla nas priorytetem, dlatego wykorzystujemy szyfrowane połączenia, aby zapewnić bezpieczeństwo Twoich danych.\n" +
                "Dziękujemy za zaufanie i wybór TwójBank. Jesteśmy gotowi służyć Ci najlepszymi usługami finansowymi.\n" +
                "W razie pytań lub problemów prosimy o kontakt z naszym działem obsługi klienta pod numerem [numer_telefonu] lub drogą mailową pod adresem [adres_email].\n\n" +
                "Pozdrawiamy,\n" +
                "Zespół TwójBank";

            EmailMessageService.SendEmail(header, message);
        }
    }
}
