using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Persistence.Db
{
    public class Address : IMapToApi<Contracts.Models.Address>
    {
        public int Id { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public Contracts.Models.Address ToApi()
        {
            return new Contracts.Models.Address
            {
                Street1 = Street1,
                Street2 = Street2,
                City = City,
                State = State,
                Zip = Zip
            };
        }

        public void CopyFromApi(Contracts.Models.Address obj)
        {
            Street1 = obj.Street1;
            Street2 = obj.Street2;
            City = obj.City;
            State = obj.State;
            Zip = obj.Zip;
        }
    }
}
