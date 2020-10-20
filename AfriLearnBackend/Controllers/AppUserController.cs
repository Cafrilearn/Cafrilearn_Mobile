using AfriLearnBackend;
using AfriLearnBackend.Constants;
using AfriLearnBackend.Models;
using CMapp.Models;
using CMapp_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace ReaiotBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : Controller
    {
        #region fields
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AfriLearnDbContext  _afriLearnDbContext;
        private readonly AppSettings _appSettings;
        #endregion

        public UserController(SignInManager<AppUser> signInManager,  AfriLearnDbContext  afriLearnDbContext, IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _afriLearnDbContext = afriLearnDbContext;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]AppUser userEntity)
        {
            userEntity.TwoFactorEnabled = false;
            var result = await _signInManager.UserManager.CreateAsync(userEntity, userEntity.PasswordHash);
            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(userEntity.Email);
                await _signInManager.UserManager.AddToRoleAsync(user, user.Role);
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            return BadRequest($"Something went wrong, could not Register the User with email {userEntity.Email}");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpGet("getall")]
        public IActionResult GetUsers()
        {
            return Ok(_afriLearnDbContext.Users.Include(s => s.Setting));
        }

        [HttpGet("get/{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _afriLearnDbContext.Users.Include(s =>s.Setting).FirstOrDefault(user => user.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"User with Id {id} not found in the database");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(AppUser appUser)
        {
            _afriLearnDbContext.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _afriLearnDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_afriLearnDbContext.Users.Any(u => u.Id == appUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(appUser);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(string id)
        {
            var user = _afriLearnDbContext.Users.FirstOrDefault(user => user.Id == id);
            if (user != null)
            {
                _afriLearnDbContext.Remove(user);
                _afriLearnDbContext.SaveChanges();
                return Ok(user);
            }
            return BadRequest($"Error, could not delete the user with id {id}. The user is not found in the system.");
        }

        [HttpPost("authenticateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthUser authDto)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(authDto.Email);
            var result = await _signInManager.PasswordSignInAsync(user, authDto.Password, false, false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            return BadRequest("Username or Passsword not correct");
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(30);
            var token = new JwtSecurityToken("Reaiot.com", claims: claims, expires: expires, signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
