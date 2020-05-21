using AutoMapper;
using Doctor.Models;
using Doctor.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/doctor/{id}/advice")]
    public class AdviceController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IMapper _mapper;

        public AdviceController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepo = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult GetAdvice(int doctorId)
        {
            if (!_doctorRepo.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var advice = _doctorRepo.GetGeneralAdvicesForDoctor(doctorId);
            //var advicesForDoctor = new List<GeneralAdviceDto>();
            //foreach (var adv in advice)
            //{
            //    advicesForDoctor.Add(new GeneralAdviceDto()
            //    {
            //        AdviceId = adv.AdviceId,
            //        AdviceTitle = adv.AdviceTitle,
            //        AdviceContent = adv.AdviceContent
            //    });
            //}
            return Ok(_mapper.Map<IEnumerable<GeneralAdviceDto>>(advice));
        }
        //[HttpGet("{id}")]
        //public IActionResult GetDoctorAdvice(int doctorId, int id)
        //{
        //    if (!_doctorRepo.DoctorExists(doctorId))
        //    {
        //        return NotFound();
        //    }
        //    var advice = _doctorRepo.GetGeneralAdviceForDoctor(doctorId, id);
        //    if (advice == null)
        //    {
        //        return NotFound();
        //    }
        //    //var adviceResult = new GeneralAdviceDto()
        //    //{
        //    //    AdviceId = advice.AdviceId,
        //    //    AdviceTitle = advice.AdviceTitle,
        //    //    AdviceContent = advice.AdviceContent
        //    //};
        //    return Ok(_mapper.Map<GeneralAdviceDto>(advice));
        //}
        [HttpPost]
        public IActionResult CreateAdvice(int doctorId, [FromBody]AdviceForCreation adviceForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_doctorRepo.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var AdviceResult = _mapper.Map<Entities.GeneralAdvice>(adviceForCreation);
            _doctorRepo.AddAdviceForDoctor(doctorId, AdviceResult);
            _doctorRepo.Savechnge();
            var createAdvice = _mapper.Map<Models.GeneralAdviceDto>(AdviceResult);
            return CreatedAtRoute(
                "CreateAdvice",
                new { doctorId, id = createAdvice.AdviceId }, createAdvice
                );
        }
        //[HttpPut("{id}")]
        //public IActionResult UpdateAdvice(int doctorId,int id,[FromBody]AdviceForUpdate adviceForUpdate)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if(!_doctorRepo.DoctorExists(doctorId))
        //    {
        //        return NotFound();
        //    }
        //    var advicefordoctor = _doctorRepo.GetGeneralAdviceForDoctor(doctorId, id);
        //    if(advicefordoctor==null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(adviceForUpdate, advicefordoctor);
        //    _doctorRepo.UpdateAdvice(doctorId, advicefordoctor);
        //    _doctorRepo.Savechnge();
        //    return NoContent();
        //}
        [HttpPatch("{idAdvice}")]
        public IActionResult PartialUpdate(int doctorId, int id, [FromBody] JsonPatchDocument<AdviceForUpdate> jsonPatch)
        {
            if (!_doctorRepo.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var advicefordoctor = _doctorRepo.GetGeneralAdviceForDoctor(doctorId, id);
            if (advicefordoctor == null)
            {
                return NotFound();
            }
            var partialAdvice = _mapper.Map<AdviceForUpdate>(advicefordoctor);
            jsonPatch.ApplyTo(partialAdvice, ModelState);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!TryValidateModel(partialAdvice))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(partialAdvice, advicefordoctor);
            _doctorRepo.UpdateAdvice(doctorId, advicefordoctor);
            _doctorRepo.Savechnge();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAdvice(int doctorId, int id)
        {
            if (!_doctorRepo.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var AdviceEntity = _doctorRepo.GetGeneralAdviceForDoctor(doctorId, id);
            if (AdviceEntity == null)
            {
                return NotFound();
            }
            _doctorRepo.DeleteAdvice(AdviceEntity);
            _doctorRepo.Savechnge();
            return NoContent();
        }
    }
}
