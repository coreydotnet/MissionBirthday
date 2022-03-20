using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.AzureAi
{
    public interface IEntityExtractionService
    {
        Task<ICollection<Entity>> AnalyzeAsync(string document);
    }
}