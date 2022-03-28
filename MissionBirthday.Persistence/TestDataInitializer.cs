using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Persistence.Db;

namespace MissionBirthday.Persistence
{
    public class TestDataInitializer
    {
        private readonly MbDataContext context;

        public TestDataInitializer(MbDataContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            var events = new Event[]
            {
                new Event
                {
                    Organization = "Sierra Bible Church",
                    Title = "Food Pantry",
                    PhoneNumber = "775-747-1217",
                    Url = "www.sierrabible.org",
                    Date = "Every Wednesday",
                    Time = "5:00pm - 8:00pm",
                    Address = new Address
                    {
                        Street1 = "3195 Everett Drive",
                        City = "Reno",
                        State = "NV",
                        Zip = "89503"
                    }
                },
                new Event
                {
                    Organization = "Eastside Church",
                    Title = "Free Food Market",
                    PhoneNumber = "541-447-3791",
                    Url = "",
                    Date = "Second Monday of each month",
                    Time = "3:00 p.m.",
                    Details = @"",
                    Address = new Address
                    {
                        Street1 = "3174 N.E. 3rd Street",
                        City = "Prineville",
                        State = "OR",
                        Zip = "97754"
                    }
                },
                new Event
                {
                    Organization = "First Baptist Church",
                    Title = "Food Pantry",
                    PhoneNumber = "541-447-5238",
                    Url = "fbcprineville.org",
                    Date = "Last two Mondays every month",
                    Time = "10:00am-12:00pm",
                    Details = @"",
                    Address = new Address
                    {
                        Street1 = "450 SE Fairview Street",
                        City = "Prineville",
                        State = "OR",
                        Zip = "97754"
                    }
                },
                new Event
                {
                    Organization = "Prineville Mobile Pantry",
                    Title = "Mobile Food Pantry",
                    PhoneNumber = "",
                    Url = "",
                    Date = "second and fourth Thursday of every month",
                    Time = "11 AM-12:30 PM",
                    Details = @"Free food and fresh produce from NeighborImpact's mobile food pantry! Text FRESH2PRINEVILLE to (541) 577-1673 for a reminder text whenever the Fresh 2 You truck is headed to Prineville.",
                    Address = new Address
                    {
                        Street1 = "375 NW Beaver St",
                        City = "Prineville",
                        State = "OR",
                        Zip = "97754"
                    }
                },
                new Event
                {
                    Organization = "St. Vincent De Paul – Prineville",
                    Title = "Food Pantry",
                    PhoneNumber = "(541) 447-7662",
                    Url = "svdpofcc.org",
                    Date = "",
                    Time = "Tuesday 1-2pm, Wednesday 5-6pm, Thursday 1-2pm",
                    Details = @"Low-income families and individuals are eligible every two weeks for a food box with enough groceries for 5 days of meals. Emergency Assistance is available to those needing help with shelter, rent/utilities, transportation, propane, etc., within Crook County. Please call and leave message regarding your emergency at above phone number.",
                    Address = new Address
                    {
                        Street1 = "1103 NE Elm St.",
                        Street2 = "Suite 140",
                        City = "Prineville",
                        State = "OR",
                        Zip = "97754"
                    }
                }
            };

            var existingOrganizations = context.Events.Select(e => e.Organization).ToList();

            foreach (var newEvent in events.Where(e => !existingOrganizations.Contains(e.Organization)))
            {
                context.Events.Add(newEvent);
            }

            context.SaveChanges();
        }
    }
}
