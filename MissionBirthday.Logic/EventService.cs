using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Repositories;

namespace MissionBirthday.Logic
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        private readonly IOcrService ocrService;
        private readonly IEntityExtractionService entityExtractionService;

        public EventService(IEventRepository repository, IOcrService ocrService, IEntityExtractionService entityExtractionService)
        {
            this.repository = repository;
            this.ocrService = ocrService;
            this.entityExtractionService = entityExtractionService;
        }

        public async Task<EventDocument> CreateEventFromImageAsync(Stream imageStream)
        {
            Event mbEvent = null;

            var document = await ReadDocumentAsync(imageStream);
            var entities = await entityExtractionService.AnalyzeAsync(document);

            string FindEntity(EntityCategory category)
            {
                return entities.FirstOrDefault(e => e.Category == category)?.Text ?? string.Empty;
            }

            // TODO: Fill out event
            mbEvent = new Event()
            {
                Organization = FindEntity(EntityCategory.Organization),
                PhoneNumber = FindEntity(EntityCategory.PhoneNumber),
                Email = FindEntity(EntityCategory.Email),
                Url = FindEntity(EntityCategory.Url)                
            };

            var timeEntities = entities.Where(e => e.Category == EntityCategory.DateTime)
                .ToList();
            // TODO: convert to start and end time

            var addressString = FindEntity(EntityCategory.Address);
            // TODO: convert to address class and assign to location

            var newId = await repository.CreateAsync(mbEvent);
            mbEvent.Id = newId;

            return new EventDocument(document, mbEvent);
        }

        private async Task<string> ReadDocumentAsync(Stream imageStream)
        {
            var ocrResults = await ocrService.ReadAsync(imageStream);
            var document = ocrResults != null
                ? string.Join(Environment.NewLine, ocrResults.TextLines)
                : string.Empty;
            return document;
        }
    }
}
