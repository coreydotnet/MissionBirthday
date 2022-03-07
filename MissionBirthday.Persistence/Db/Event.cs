using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Persistence.Db
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Organization { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Details { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        /// <summary>
        /// Date of the event and start time of day in local timezone.
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        // Date of event and the end time of day in local timezone.
        public DateTimeOffset EndTime { get; set; }

        public Contracts.Models.Event ToApi()
        {
            return new Contracts.Models.Event
            {
                Id = Id,
                Organization = Organization,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Url = Url,
                Details = Details,
                Location = Address.ToApi(),
                StartTime = StartTime,
                EndTime = EndTime
            };
        }

        public void CopyFromApi(Contracts.Models.Event obj)
        {
            Id = obj.Id;
            Organization = obj.Organization;
            PhoneNumber = obj.PhoneNumber;
            Email = obj.Email;
            Url = obj.Url;
            Details = obj.Details;
            StartTime = obj.StartTime;
            EndTime = obj.EndTime;

            Address ??= new Address();
            Address.CopyFromApi(obj.Location);
        }
    }
}
