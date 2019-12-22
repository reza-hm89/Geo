using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataContract.Geographical;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using ServiceContract.Geographical;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DistanceController : ControllerBase
    {

        private readonly IDistanceService _distanceService;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _caller;

        public DistanceController(IDistanceService distanceService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _distanceService = distanceService;
            _mapper = mapper;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CalculateDistanceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var userId = _caller.Claims.Single(c => c.Type.Equals("id")).Value;

                var source = new Point(dto.SourceX, dto.SourceY);
                var destination = new Point(dto.DestinationX, dto.DestinationY);

                var distance = source.Distance(destination);

                var userDistance = new Model.Geographical.UserDistance() { Distance = distance, UserId = userId };

                // You can hust save data in sql database by use this parameter useInMemory = false

                var result = await _distanceService.Insert(userDistance);

                if (result != null)
                {
                    return Ok();
                }

                return StatusCode(500, "Internal Error");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetHistoryRequests()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var userId = _caller.Claims.Single(c => c.Type.Equals("id")).Value;

                // You can get data from sql database by use this parameter useInMemory = false

                var result = _distanceService.GetByUserId(userId);

                if (result != null)
                {
                    return Ok(result);
                }

                return StatusCode(500, "Internal Error");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}