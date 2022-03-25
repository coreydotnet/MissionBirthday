using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Persistence.Db
{
    public class Event : IMapToApi<Contracts.Models.Event>
    {
        private const char ItemsSeparator = ';';

        public int Id { get; set; }

        [Required]
        public string Organization { get; set; }

        [Required]
        public string Title { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Details { get; set; }

        public string Items { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public Contracts.Models.Event ToApi()
        {
            return new Contracts.Models.Event
            {
                Id = Id,
                Organization = Organization,
                Title = Title,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Url = Url,
                Details = Details,
                Location = Address.ToApi(),
                Items = Items?.Split(ItemsSeparator) ?? Array.Empty<string>(),
                Date = Date,
                Time = Time
            };
        }

        public void CopyFromApi(Contracts.Models.Event api)
        {
            Id = api.Id;
            Organization = api.Organization;
            Title = api.Title;
            PhoneNumber = api.PhoneNumber;
            Email = api.Email;
            Url = api.Url;
            Details = api.Details;
            Date = api.Date;
            Time = api.Time;

            Address ??= new Address();
            Address.CopyFromApi(api.Location);

            Items = string.Join(ItemsSeparator, api.Items
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => i.Replace($"{ItemsSeparator}", ",").Trim().ToLowerInvariant())
                .OrderBy(i => i));
        }
    }
}
