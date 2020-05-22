using AutoMapper;
using Doctor.Models;
using Doctor.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Doctor.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace Doctor.Controllers
{
    
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController: ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly DoctorsDbContext dbContext;
        public Microsoft.Extensions.Configuration.IConfiguration _confg;

        public DoctorController(IDoctorRepository doctorRepository,IMapper mapper, Microsoft.Extensions.Configuration.IConfiguration confg, DoctorsDbContext context)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _confg = confg;
            dbContext = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetDoctor()
        {
            var DoctorEntity = _doctorRepository.GetDoctors();
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
            return Ok(_mapper.Map<IEnumerable<DoctorWithoutAdvice>>(DoctorEntity));
        }
        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _doctorRepository.GetDoctors(id);
            if (doctor == null)
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
            var doctorResult = _mapper.Map<DoctorWithoutAdvice>(doctor);
            return Ok(doctorResult);
        }
       
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorForCreate doctor)
        {

            try
            {
                Console.WriteLine(ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var Result = _mapper.Map<Entities.Doctors>(doctor);
                _doctorRepository.CreateDoctor(Result);
                _doctorRepository.Savechnge();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status404NotFound,"zbook 3dl");

            }

        }
        [HttpPatch]
        public IActionResult DoctorUpdate(int doctorId, [FromBody] JsonPatchDocument<DoctorForUpdate> jsonPatch)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var doctor = _doctorRepository.GetDoctors(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            var partialDoctor = _mapper.Map<DoctorForUpdate>(doctor);
            jsonPatch.ApplyTo(partialDoctor, ModelState);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!TryValidateModel(partialDoctor))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(partialDoctor, doctor);
            _doctorRepository.UpdateDoctor(doctorId, doctor);
            _doctorRepository.Savechnge();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var deldoctor = _doctorRepository.GetDoctors(doctorId);
            if (deldoctor == null)
            {
                return NotFound();
            }
            _doctorRepository.DeleteDoctor(deldoctor);
            _doctorRepository.Savechnge();
            return NoContent();
        }

    }
}
