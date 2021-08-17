using Contatos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        public AuthorizeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _configuration = configuration;
        }

        // GET: api/<AuthorizeController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"{this} accessed at {DateTime.Now.ToLongDateString()}";
        }

        // POST api/<AuthorizeController>
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserLogin model)
        {

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signManager.SignInAsync(user, false);
            return Ok(CreateToken(model));

        }

        // PUT api/<AuthorizeController>/5
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLogin userInfo)
        {
            var result = await _signManager.PasswordSignInAsync(userInfo.Email,
                    userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(CreateToken(userInfo));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido...");
                return BadRequest(ModelState);
            }
        }

        private UserToken CreateToken(UserLogin userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("myAge", "14"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera uma chave com base em um algoritimo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["jwt:key"]));

            // gera uma assunatura digital do token usando o algoritimo hmac e a chave privada
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiracao do token
            var expireHours = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expireHours));

            //classe que representa um token JWT e gera o token
            JwtSecurityToken token = new(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            //retorna os dados com o token e informaoes
            return new UserToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "JWT Token OK"
            };
        }
    }
}
