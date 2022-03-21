using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Repositories;
using MissionBirthday.Contracts.Events;

namespace MissionBirthday.Logic.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        private readonly IOcrService ocrService;
        private readonly IEntityExtractionService entityExtractionService;
        private readonly IEntitiesToEventConverter converter;

        public EventService(IEventRepository repository, IOcrService ocrService, IEntityExtractionService entityExtractionService, IEntitiesToEventConverter converter)
        {
            this.repository = repository;
            this.ocrService = ocrService;
            this.entityExtractionService = entityExtractionService;
            this.converter = converter;
        }

        public async Task<EventDocument> CreateEventFromImageAsync(Stream imageStream)
        {
            Event mbEvent = null;

            var document = await ReadDocumentAsync(imageStream);
            var entities = await entityExtractionService.AnalyzeAsync(document);
            mbEvent = converter.ConvertToEvent(entities);

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
