using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Ocr;
using MissionBirthday.Contracts.Repositories;

namespace MissionBirthday.Logic
{
    public class EventManager : IEventManager
    {
        private readonly IEventRepository events;
        private readonly IOcrService ocrService;

        public EventManager(IEventRepository events, IOcrService ocrService)
        {
            this.events = events;
            this.ocrService = ocrService;
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

            var ocrResults = await ocrService.ReadAsync(imageStream);

            if (ocrResults != null)
            {
                mbEvent = new Event()
                {
                    Details = string.Join(Environment.NewLine, ocrResults.TextLines)
                };

                var newId = await events.CreateAsync(mbEvent);
                mbEvent.Id = newId;
            }

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
    }
}
