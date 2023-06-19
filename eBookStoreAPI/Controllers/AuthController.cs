using AutoMapper;
using BusinessObject.API.Request;
using BusinessObject.API.Response;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = new BusinessObject.Models.User();
            if (model.Email == _configuration["Admin"] && model.Password == _configuration["Password"])
            {
                user = new BusinessObject.Models.User
                {
                    Email = model.Email,
                    Password = model.Password,
                    RoleId = 1,
                    FirstName = "Admin Ebook Store"
                };
            }
            else user = await _userRepository.Login(model.Email, model.Password);
            if (user == null) return NotFound();

            var role = user.RoleId == 1 ? "admin" : "member";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtIssuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.MiddleName} {user.LastName}"),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = _mapper.Map<LoginResponseModel>(user);
            response.Role = role;
            response.Token = tokenHandler.WriteToken(token);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<GetInfoResponseModel>> GetInfo()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userRepository.GetUserById(id).FirstAsync();
            var response = _mapper.Map<GetInfoResponseModel>(user);
            return Ok(response);
        }
    }
}
