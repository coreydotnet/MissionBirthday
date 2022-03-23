using System.Collections.Generic;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Contracts.Events
{
    public interface IEntitiesToEventConverter
    {
        Event ConvertToEvent(ICollection<Entity> entities);
    }
}