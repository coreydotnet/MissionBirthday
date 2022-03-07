using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Organization { get; set; } = string.Empty;

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Details { get; set; }

        [Required]
        public Address Location { get; set; } = new Address();

        /// <summary>
        /// Date of the event and start time of day in local timezone.
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        // Date of event and the end time of day in local timezone.
        public DateTimeOffset EndTime { get; set; }
    }
}
