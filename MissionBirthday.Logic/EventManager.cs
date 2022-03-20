using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Repositories;

namespace MissionBirthday.Logic
{
    public class EventManager : IEventManager
    {
        private readonly IEventRepository events;
        private readonly IOcrService ocrService;
        private readonly IEntityExtractionService entityExtractionService;

        public EventManager(IEventRepository events, IOcrService ocrService, IEntityExtractionService entityExtractionService)
        {
            this.events = events;
            this.ocrService = ocrService;
            this.entityExtractionService = entityExtractionService;
        }

        public async Task<ICollection<Event>> GetAllAsync()
        {
            return await events.GetAll().ToArrayAsync();
        }

        public Task<Event> GetAsync(int eventId)
        {
            return events.GetAsync(eventId);
        }

        public Task<int> CreateAsync(Event mbEvent)
        {
            return events.CreateAsync(mbEvent);
        }

        public async Task<Event> CreateEventFromImageAsync(Stream imageStream)
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

            var newId = await CreateAsync(mbEvent);
            mbEvent.Id = newId;

            return mbEvent;
        }

        public Task UpdateAsync(Event mbEvent)
        {
            return events.UpdateAsync(mbEvent);
        }

        public Task DeleteAsync(int id)
        {
            return events.DeleteAsync(id);
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
