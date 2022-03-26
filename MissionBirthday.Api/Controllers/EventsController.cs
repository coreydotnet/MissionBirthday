using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MissionBirthday.Contracts.Events;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Repositories;

namespace MissionBirthday.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private long MaxImageBytes = 6 * 1024 * 1024;
        private readonly ILogger<EventsController> logger;
        private readonly IEventRepository repository;
        private readonly IEventService eventService;

        public EventsController(ILogger<EventsController> logger, IEventRepository repository, IEventService eventService)
        {
            this.logger = logger;
            this.repository = repository;
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await repository.GetAllAsync());
        }

        [HttpGet("search/{zipCode}")]
        public async Task<IActionResult> SearchByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                return BadRequest();

            return Ok(await eventService.GetAllAsync(zipCode));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var mbEvent = await repository.GetAsync(id);

            return mbEvent != null
                ? Ok(mbEvent)
                : NotFound();
        }

        [HttpPost("upload")]
        [ProducesResponseType(200, Type = typeof(EventDocument))]
        public async Task<IActionResult> PostUploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            if (file.Length > MaxImageBytes)
                return BadRequest();

            // TODO: Validate: JPEG, PNG, BMP, PDF, and TIFF

            try
            {

                using var imageStream = new MemoryStream();
                await file.CopyToAsync(imageStream);
                imageStream.Position = 0;

                var mbEvent = await eventService.CreateEventFromImageAsync(imageStream);
                return mbEvent != null
                    ? Ok(mbEvent)
                    : UnprocessableEntity();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to upload an image: ContentDisposition={file.ContentDisposition}; ContentType={file.ContentType}; Length={file.Length}");
                return UnprocessableEntity();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Event mbEvent)
        {
            if (mbEvent == null)
                return BadRequest();

            var newId = await repository.CreateAsync(mbEvent);

            return Ok(newId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]Event mbEvent)
        {
            if (mbEvent == null || mbEvent.Id != id)
                return BadRequest();

            await repository.UpdateAsync(mbEvent);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);

            return Ok();
        }
    }
}
