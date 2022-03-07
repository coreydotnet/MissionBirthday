using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Contracts.Repositories
{
    public interface IEventRepository
    {
        IQueryable<Event> GetAll();

        Task<Event> GetAsync(int id);

        Task<int> CreateAsync(Event mbEvent);

        Task UpdateAsync(Event mbEvent);

        Task DeleteAsync(int id);
    }
}
