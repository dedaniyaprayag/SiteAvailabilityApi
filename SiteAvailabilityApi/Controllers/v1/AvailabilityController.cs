using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityApi.Models;
using SiteAvailabilityApi.Services;
using System;

namespace SiteAvailabilityApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]

    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }


        /// <summary>
        /// Post API Endpoint to send a message to RabbitMq queue
        /// </summary>
        /// <param name="site">Site Model</param>
        /// <returns>Returns the updated customer</returns>
        /// <response code="200">Returned if the message posted successfully.</response>
        /// <response code="400">Returned if the model couldn't be parsed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult PostMessageToQueue([FromBody] Site site)
        {
            try
            {
                return Ok(_availabilityService.SendSiteToQueue(site));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}