using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doctor.Models.User;
using Doctor.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var userEntity = _userRepository.GetUsers();
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
            return Ok(_mapper.Map<IEnumerable<GetAllUsers>>(userEntity));
        }
        [HttpGet("{id}")]
        public IActionResult GetUsers(int id)
        {
            var user = _userRepository.GetUsers(id);
            if (user == null)
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
            var userResult = _mapper.Map<GetAllUsers>(user);
            return Ok(userResult);
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserForCreation userForCreation)
        {

            try
            {
                Console.WriteLine(ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var Result = _mapper.Map<Entities.Users>(userForCreation);
                _userRepository.CreateUser(Result);
                _userRepository.Savechnge();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status404NotFound, "3dl");

            }

        }
        [HttpPatch]
        public IActionResult UserUpdate(int userId, [FromBody] JsonPatchDocument<UserForUpdate> jsonPatch)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var user = _userRepository.GetUsers(userId);
            if (user == null)
            {
                return NotFound();
            }
            var partialuser = _mapper.Map<UserForUpdate>(user);
            jsonPatch.ApplyTo(partialuser, ModelState);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!TryValidateModel(partialuser))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(partialuser, user);
            _userRepository.UpdateUser(userId, user);
            _userRepository.Savechnge();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var deluser = _userRepository.GetUsers(userId);
            if (deluser == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(deluser);
            _userRepository.Savechnge();
            return NoContent();
        }
    }
}