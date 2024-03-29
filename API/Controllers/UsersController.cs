using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>>GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
                       
            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUdateDto memberUdateDto)
        {
            var username =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetAppUserByNameAsync(username);

            if (user == null) return NotFound();

            _mapper.Map(memberUdateDto, user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}