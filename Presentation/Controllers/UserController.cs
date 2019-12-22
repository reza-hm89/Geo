using AutoMapper;
using DataContract.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using ServiceContract.Users;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
       
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
       
        public UserController(IUserService userService,
                              UserManager<User> userManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;           
        }

        [HttpGet]
       
        public async Task<IActionResult> SelectAll()
        {
            try
            {
                return Ok(await _userService.SelectAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpPost]
        [ActionName("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _mapper.Map<User>(model);

                user.UserName = model.Email;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok();                  
                }

                return BadRequest(result.Errors);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }     
    }
}

