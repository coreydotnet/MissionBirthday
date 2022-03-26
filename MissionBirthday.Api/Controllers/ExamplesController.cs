using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MissionBirthday.Contracts.Events;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamplesController : ControllerBase
    {
        /// <summary>
        /// Directory on server where images exist.
        /// </summary>
        private const string ExamplesDirectory = @"App\assets\examples";

        /// <summary>
        /// Web root directory from which images are served.
        /// </summary>
        private const string WebRoot = @"App";

        private readonly ILogger<ExamplesController> logger;
        private readonly IEventService eventService;

        private static readonly string[] ExampleExtensions = new[] { "*.png", "*.jpg", "*.jpeg" };

        public ExamplesController(ILogger<ExamplesController> logger, IEventService eventService)
        {
            this.logger = logger;
            this.eventService = eventService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ExampleImage))]
        public IActionResult GetExamples()
        {
            if (!Directory.Exists(ExamplesDirectory))
                return NotFound();

            var webRootDirectory = Path.GetFullPath(WebRoot);

            var images = ExampleExtensions.SelectMany(extensionPattern => Directory.EnumerateFiles(ExamplesDirectory, extensionPattern))
                .Select(fullPath =>
                    new ExampleImage
                    {
                        Name = Path.GetFileNameWithoutExtension(fullPath),
                        Key = Path.GetFileName(fullPath),
                        SourcePath = Path.GetRelativePath(webRootDirectory, fullPath)
                    })
                .ToArray();

            return Ok(images);
        }

        [HttpPost("{key}")]
        [ProducesResponseType(200, Type = typeof(EventDocument))]
        public async Task<IActionResult> PostExampleAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest();

            var invalidChars = Path.GetInvalidFileNameChars();
            if (key.Any(c => invalidChars.Contains(c)))
                return BadRequest();            

            try
            {
                var path = Path.Combine(ExamplesDirectory, key);

                if (!System.IO.File.Exists(path))
                    return NotFound();

                using var imageStream = new FileStream(path, FileMode.Open);

                var mbEvent = await eventService.CreateEventFromImageAsync(imageStream);
                return mbEvent != null
                    ? Ok(mbEvent)
                    : UnprocessableEntity();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to process example image: {key}");
                return UnprocessableEntity();
            }
        }
    }
}
