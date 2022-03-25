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

        [Required]
        public string Title { get; set; } = string.Empty;

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Details { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        [Required]
        public Address Location { get; set; }

        [Required]
        public IList<string> Items { get; set; }
    }
}
