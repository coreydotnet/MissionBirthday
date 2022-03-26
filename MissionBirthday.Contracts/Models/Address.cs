using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.Models
{
    public class Address
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        public string State { get; set; }

        [Required]
        [MinLength(5)]
        public string Zip { get; set; }
    }
}
