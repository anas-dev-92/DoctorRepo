using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doctor.Models.Admin;
using Doctor.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult GetAdmins()
        {
            var adminEntity = _adminRepository.GetAdmins();
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
            return Ok(_mapper.Map<IEnumerable<GetAdmin>>(adminEntity));
        }
        [HttpGet("{id}")]
        public IActionResult GetAdmin(int id)
        {
            var admin = _adminRepository.GetAdmin(id);
            if (admin == null)
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
            var adminResult = _mapper.Map<GetAdmin>(admin);
            return Ok(adminResult);
        }
        [HttpPost]
        public IActionResult CreateAdmin([FromBody] AdminForCreate adminForCreation)
        {

            try
            {
                Console.WriteLine(ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var Result = _mapper.Map<Entities.Admins>(adminForCreation);
                _adminRepository.CreateAdmin(Result);
                _adminRepository.Savechnge();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status404NotFound, "3dl");

            }

        }
        [HttpPatch]
        public IActionResult AdminUpdate(int AdminId, [FromBody] JsonPatchDocument<AdminForUpdate> jsonPatch)
        {
            if (!_adminRepository.AdminExists(AdminId))
            {
                return NotFound();
            }
            var admin = _adminRepository.GetAdmin(AdminId);
            if (admin == null)
            {
                return NotFound();
            }
            var partialadmin = _mapper.Map<AdminForUpdate>(admin);
            jsonPatch.ApplyTo(partialadmin, ModelState);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!TryValidateModel(partialadmin))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(partialadmin, admin);
            _adminRepository.UpdateAdmin(AdminId, admin);
            _adminRepository.Savechnge();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteAdmin(int adminId)
        {
            if (!_adminRepository.AdminExists(adminId))
            {
                return NotFound();
            }
            var deladmin = _adminRepository.GetAdmin(adminId);
            if (deladmin == null)
            {
                return NotFound();
            }
            _adminRepository.DeleteAdmin(deladmin);
            _adminRepository.Savechnge();
            return NoContent();
        }
    }
}