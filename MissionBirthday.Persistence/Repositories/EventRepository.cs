using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Repositories;

namespace MissionBirthday.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MbDataContext context;

        public EventRepository(MbDataContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Event>> GetAllAsync()
        {
            return await QueryEvents()
                .AsNoTracking()
                .OrderBy(e => e.Organization)
                .Select(e => e.ToApi())
                .ToArrayAsync();
        }

        public async Task<Event> GetAsync(int id)
        {
            var dbEvent = await FindAsync(QueryEvents().AsNoTracking(), id);

            return dbEvent?.ToApi();                
        }

        public async Task<int> CreateAsync(Event mbEvent)
        {
            var dbEvent = new Db.Event();
            dbEvent.CopyFromApi(mbEvent);

            await context.Events.AddAsync(dbEvent);
            await context.SaveChangesAsync();

            return dbEvent.Id;
        }

        public async Task UpdateAsync(Event mbEvent)
        {
            var dbEvent = await FindAsync(mbEvent.Id);

            if (dbEvent == null)
                return;

            dbEvent.CopyFromApi(mbEvent);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbEvent = await FindAsync(id);

            if (dbEvent == null)
                return;

            context.Events.Remove(dbEvent);
            await context.SaveChangesAsync();
        }

        private IQueryable<Db.Event> QueryEvents()
        {
            return context.Events.Include(e => e.Address);
        }

        private Task<Db.Event> FindAsync(int id) => FindAsync(QueryEvents(), id);

        private Task<Db.Event> FindAsync(IQueryable<Db.Event> query, int id) => query.FirstOrDefaultAsync(e => e.Id == id);
    }
}
