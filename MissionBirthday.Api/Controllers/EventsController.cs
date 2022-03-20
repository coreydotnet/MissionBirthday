using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private long MaxImageBytes = 6 * 1024 * 1024;
        private readonly ILogger<EventsController> _logger;
        private readonly IEventManager events;

        public EventsController(ILogger<EventsController> logger, IEventManager eventManager)
        {
            _logger = logger;
            events = eventManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await events.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var mbEvent = await events.GetAsync(id);

            return mbEvent != null
                ? Ok(mbEvent)
                : NotFound();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> PostUploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            if (file.Length > MaxImageBytes)
                return BadRequest();

            // TODO: Validate: JPEG, PNG, BMP, PDF, and TIFF

            using var imageStream = new MemoryStream();
            await file.CopyToAsync(imageStream);
            imageStream.Position = 0;

            var mbEvent = await events.CreateEventFromImageAsync(imageStream);
            return mbEvent != null
                ? Ok(mbEvent)
                : UnprocessableEntity();
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody]Event mbEvent)
        {
            if (mbEvent == null)
                return BadRequest();

            var newId = await events.CreateAsync(mbEvent);

            return Ok(newId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]Event mbEvent)
        {
            if (mbEvent == null || mbEvent.Id != id)
                return BadRequest();

            await events.UpdateAsync(mbEvent);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await events.DeleteAsync(id);

            return Ok();
        }
    }
}
