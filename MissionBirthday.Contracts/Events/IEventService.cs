using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Contracts.Events
{
    public interface IEventService
    { 
        Task<EventDocument> CreateEventFromImageAsync(Stream imageStream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if can't convert <paramref name="zipCode"/> into a number.</exception>
        Task<ICollection<Event>> GetAllAsync(string zipCode);
    }
}
