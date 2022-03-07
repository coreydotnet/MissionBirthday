using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MissionBirthday.Persistence.Db;

namespace MissionBirthday.Persistence
{
    public class MbDataContext : DbContext
    {
        public MbDataContext(DbContextOptions<MbDataContext> options)
            :base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TODO: Add additional model configuration
        }
    }
}
