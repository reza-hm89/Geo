using AutoMapper;
using DataContract.Auth;
using DataContract.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Authentication;
using Model.Users;
using Newtonsoft.Json;
using ServiceContract.Authentication;
using ServiceContract.Users;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFramework.Jwt;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;      
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        public AuthController(IUserService userService,                             
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,                           
                              IJwtFactory jwtFactory,
                              IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;         
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDto model)
        {


            if (ModelState.IsValid)
            {
                var result = new LoginResponse();

                var user = _userService.GetByEmail(model.Email);

                if (user == null)
                {
                    return NotFound("Couldn't find your Account. Your Email does not exist.");
                }

                var result1 = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                var identity = await GetClaimsIdentity(user.UserName, model.Password);

                if (result1.Succeeded)
                {
                    result.IsSucceeded = true;

                    var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions,
                        new JsonSerializerSettings { Formatting = Formatting.Indented });

                    result.auth_token = jwt;                    

                    return Ok(result);

                }


            }

            return BadRequest(ModelState);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(username);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(username, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}