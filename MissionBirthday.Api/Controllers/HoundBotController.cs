using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoundBotController : ControllerBase
    {
        private readonly IHoundBotService houndBotService;
        private readonly ILogger<HoundBotController> logger;

        public HoundBotController(IHoundBotService houndBotService, ILogger<HoundBotController> logger)
        {
            this.houndBotService = houndBotService;
            this.logger = logger;
        }

        [HttpGet("token")]
        [ProducesResponseType(200, Type = typeof(HoundBotChatConfig))]
        public async Task<IActionResult> GetTokenAsync()
        {
            try
            {
                var config = await houndBotService.GetChatConfigAsync();
                return !string.IsNullOrEmpty(config.Token)
                    ? Ok(config)
                    : NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get direct line chat config.");
                return BadRequest();
            }
        }
    }
}
