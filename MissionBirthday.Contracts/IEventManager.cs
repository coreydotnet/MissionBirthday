using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Contracts
{
    public interface IEventManager
    {
        Task<ICollection<Event>> GetAllAsync();

        Task<Event> GetAsync(int id);

        Task<Event> CreateEventFromImageAsync(Stream imageStream);

        Task<int> CreateAsync(Event mbEvent);

        Task UpdateAsync(Event mbEvent);

        Task DeleteAsync(int id);
    }
}
