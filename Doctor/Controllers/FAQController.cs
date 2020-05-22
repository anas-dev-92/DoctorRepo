using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doctor.Models.FAQ;
using Doctor.Services.FAQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        public FAQController(IFAQRepository fAQRepository, IMapper mapper)
        {
            _faqRepository = fAQRepository ?? throw new ArgumentNullException(nameof(fAQRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult GetFAQ()
        {
            var faqEntity = _faqRepository.GetFAQ();
            //var result = new List<DoctorWithoutAdvice>();
            //foreach (var doctor in DoctorEntity)
            //{
            //    result.Add(new DoctorWithoutAdvice
            //    {
            //        DoctorId = doctor.DoctorId,
            //        Name = doctor.Name,
            //        Special = doctor.Special,
            //        Phone = doctor.Phone,
            //        ShortDescription = doctor.ShortDescription,
            //        Age = doctor.Age,
            //        YearOfExperence = doctor.YearOfExperence,
            //        Password = doctor.Password,
            //        ClinicAddress = doctor.ClinicAddress
            //    });
            //}
            return Ok(_mapper.Map<IEnumerable<GetFAQ>>(faqEntity));
        }
        [HttpGet("{id}")]
        public IActionResult GetFAQ(int faqid)
        {
            var faq = _faqRepository.GetFAQs(faqid);
            if (faq == null)
            {
                return NotFound();
            }
            //var Sdoctor = new DoctorWithoutAdvice()
            //{
            //    DoctorId = doctor.DoctorId,
            //    Name = doctor.Name,
            //    Special = doctor.Special,
            //    Phone = doctor.Phone,
            //    ShortDescription = doctor.ShortDescription,
            //    Age = doctor.Age,
            //    YearOfExperence = doctor.YearOfExperence,
            //    Password = doctor.Password,
            //    ClinicAddress = doctor.ClinicAddress
            //};
            var faqResult = _mapper.Map<GetFAQ>(faq);
            return Ok(faqResult);
        }
        [HttpPost]
        public IActionResult CreateFAQ([FromBody] FAQforCreate fAQforCreate)
        {

            try
            {
                Console.WriteLine(ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var Result = _mapper.Map<Entities.FAQs>(fAQforCreate);
                _faqRepository.CreateFAQ(Result);
                _faqRepository.Savechnge();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status404NotFound, "3dl");

            }

        }
        [HttpPatch]
        public IActionResult FAQUpdate(int faqId, [FromBody] JsonPatchDocument<FAQforUpdate> jsonPatch)
        {
            if (!_faqRepository.FAQExists(faqId))
            {
                return NotFound();
            }
            var faq = _faqRepository.GetFAQs(faqId);
            if (faq == null)
            {
                return NotFound();
            }
            var partialFAQ = _mapper.Map<FAQforUpdate>(faq);
            jsonPatch.ApplyTo(partialFAQ, ModelState);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!TryValidateModel(partialFAQ))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(partialFAQ, faq);
            _faqRepository.UpdateFAQ(faqId, faq);
            _faqRepository.Savechnge();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteFAQ(int faqId)
        {
            if (!_faqRepository.FAQExists(faqId))
            {
                return NotFound();
            }
            var delFAQ = _faqRepository.GetFAQs(faqId);
            if (delFAQ == null)
            {
                return NotFound();
            }
            _faqRepository.DeleteFAQ(delFAQ);
            _faqRepository.Savechnge();
            return NoContent();
        }
    }
}