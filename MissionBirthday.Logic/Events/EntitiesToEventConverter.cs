using System;
using System.Collections.Generic;
using System.Linq;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Events;

namespace MissionBirthday.Logic.Events
{
    public class EntitiesToEventConverter : IEntitiesToEventConverter
    {
        public Event ConvertToEvent(ICollection<Entity> entities)
        {
            var organization = FindCategory(entities, EntityCategory.Organization)
                ?? entities.Where(e => e.Category == EntityCategory.Location && !e.IsGpeLocation()).FirstOrDefault();

            var mbEvent = new Event()
            {
                Organization = organization.TextOrDefault(),
                PhoneNumber = FindCategory(entities, EntityCategory.PhoneNumber).TextOrDefault(),
                Email = FindCategory(entities, EntityCategory.Email).TextOrDefault(),
                Url = FindCategory(entities, EntityCategory.Url).TextOrDefault(),
                Details = "",
                Items = entities.Where(e => e.Category == EntityCategory.Product).Select(e => e.Text).ToArray(),
                Location = CreateAddress(entities)
            };

            var timeEntities = entities.Where(e => e.Category == EntityCategory.DateTime)
                .ToList();
            // TODO: convert to start and end time
            return mbEvent;
        }

        private Address CreateAddress(ICollection<Entity> entities)
        {
            Address address = new Address();

            var addressEntities = entities.Where(e => e.IsAddress() || e.IsGpeLocation() || e.IsNumberQuantity()).ToList();

            if (addressEntities.Any())
            {
                var addresses = addressEntities.Where(e => e.IsAddress()).ToList();
                var gpes = addressEntities.Where(e => e.IsGpeLocation()).ToList();

                // Grab any numbers after GPE locations (i.e. city, state)
                var potentialZips = addressEntities.SkipWhile(e => !e.IsGpeLocation()).Where(e => e.IsNumberQuantity()).ToList();

                address.Street1 = addresses.FirstOrDefault().TextOrDefault();
                address.Street2 = addresses.Skip(1).FirstOrDefault().TextOrDefault();
                address.City = gpes.FirstOrDefault().TextOrDefault();
                address.State = gpes.Skip(1).FirstOrDefault().TextOrDefault();
                address.Zip = potentialZips.FirstOrDefault().TextOrDefault();
            }

            return address;
        }

        private Entity FindCategory(ICollection<Entity> entities, EntityCategory category)
        {
            return entities.FirstOrDefault(e => e.Category == category);
        }
    }
}
