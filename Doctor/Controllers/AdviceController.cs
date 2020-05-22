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
        private readonly IAdviceRepository _adviceRepo;
        private readonly IMapper _mapper;

        public AdviceController(IAdviceRepository adviceRepository, IMapper mapper)
        {
            _adviceRepo = adviceRepository ?? throw new ArgumentNullException(nameof(adviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet("{idGet}")]
        public IActionResult GetAdvice(int id)
        {
            if (!_adviceRepo.AdviceExists(id))
            {
                return NotFound();
            }
            var advice = _adviceRepo.GetGeneralAdvicesForDoctor(id);
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
            if (!_adviceRepo.AdviceExists(doctorId))
            {
                return NotFound();
            }
            var AdviceResult = _mapper.Map<Entities.GeneralAdvice>(adviceForCreation);
            _adviceRepo.AddAdviceForDoctor(doctorId, AdviceResult);
            _adviceRepo.Savechnge();
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
        [HttpPatch("{idUpdate}")]
        public IActionResult PartialUpdate(int doctorId, int id, [FromBody] JsonPatchDocument<AdviceForUpdate> jsonPatch)
        {
            if (!_adviceRepo.AdviceExists(doctorId))
            {
                return NotFound();
            }
            var advicefordoctor = _adviceRepo.GetGeneralAdviceForDoctor(doctorId, id);
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
            _adviceRepo.UpdateAdvice(doctorId, advicefordoctor);
            _adviceRepo.Savechnge();
            return NoContent();
        }
        [HttpDelete("{idDelete}")]
        public IActionResult DeleteAdvice(int doctorId, int id)
        {
            if (!_adviceRepo.AdviceExists(doctorId))
            {
                return NotFound();
            }
            var AdviceEntity = _adviceRepo.GetGeneralAdviceForDoctor(doctorId, id);
            if (AdviceEntity == null)
            {
                return NotFound();
            }
            _adviceRepo.DeleteAdvice(AdviceEntity);
            _adviceRepo.Savechnge();
            return NoContent();
        }
    }
}
