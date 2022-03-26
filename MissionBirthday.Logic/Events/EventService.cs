using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Repositories;
using MissionBirthday.Contracts.Events;
using System.Collections.Generic;
using System.Linq;

namespace MissionBirthday.Logic.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        private readonly IOcrService ocrService;
        private readonly IEntityExtractionService entityExtractionService;
        private readonly IEntitiesToEventConverter converter;

        public EventService(IEventRepository eventRepository, IOcrService ocrService, IEntityExtractionService entityExtractionService, IEntitiesToEventConverter converter)
        {
            this.eventRepository = eventRepository;
            this.ocrService = ocrService;
            this.entityExtractionService = entityExtractionService;
            this.converter = converter;
        }

        public async Task<EventDocument> CreateEventFromImageAsync(Stream imageStream)
        {
            var document = await ReadDocumentAsync(imageStream);
            var entities = await entityExtractionService.AnalyzeAsync(document);
            var mbEvent = converter.ConvertToEvent(entities);
            return new EventDocument(document, mbEvent);
        }

        public async Task<ICollection<Event>> GetAllAsync(string zipCode)
        {
            var events = await eventRepository.GetAllAsync();

            var targetZip = ZipToNumber(zipCode);

            if (targetZip == 0)
                return events;

            return events.OrderBy(e => Distance(ZipToNumber(e.Location.Zip), targetZip))
                .ThenBy(e => e.Organization, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();
        }

        private async Task<string> ReadDocumentAsync(Stream imageStream)
        {
            var ocrResults = await ocrService.ReadAsync(imageStream);
            var document = ocrResults != null
                ? string.Join(Environment.NewLine, ocrResults.TextLines)
                : string.Empty;
            return document;
        }

        private int ZipToNumber(string zipCode)
        {
            int result = 0;

            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                var numericPart = zipCode.Split('-', StringSplitOptions.RemoveEmptyEntries).First();

                if (int.TryParse(numericPart, out int zipNumber))
                    result = zipNumber;
            }

            return result;
        }

        private int Distance(int zipLeft, int zipRight)
        {
            if (zipLeft == 0 || zipRight == 0)
                return int.MaxValue;
            return Math.Abs(zipLeft - zipRight);
        }
    }
}
