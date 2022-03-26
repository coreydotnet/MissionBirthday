using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionBirthday.Contracts.Models;

namespace MissionBirthday.Contracts
{
    public interface IHoundBotService
    {
        Task<HoundBotChatConfig> GetChatConfigAsync();
    }
}
