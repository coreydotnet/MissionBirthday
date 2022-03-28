using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Models;
using AzureEntityCategory = Azure.AI.TextAnalytics.EntityCategory;
using EntityCategory = MissionBirthday.Contracts.AzureAi.EntityCategory;

namespace MissionBirthday.Logic.AzureAi
{
    public class EntityExtractionService : IEntityExtractionService
    {
        private readonly ILogger<EntityExtractionService> logger;
        private readonly LanguageServiceOptions options;

        public EntityExtractionService(ILogger<EntityExtractionService> logger, IOptions<LanguageServiceOptions> options)
        {
            this.logger = logger;
            this.options = options.Value;
        }

        public async Task<ICollection<Entity>> AnalyzeAsync(string document)
        {
            ICollection<Entity> results = null;

            try
            {
                if (!string.IsNullOrEmpty(document))
                {
                    var client = CreateClient();
                    var response = await client.RecognizeEntitiesAsync(document);
                    var entities = response.Value;

                    foreach (var warning in entities.Warnings)
                        logger.LogWarning($"Warning {warning.WarningCode}: {warning.Message}");

                    if (entities.Count > 0)
                    {
                        results = entities.Select(e => new Entity(e.Text.Replace(Environment.NewLine, string.Empty), MapCategory(e.Category), e.SubCategory, e.ConfidenceScore))
                            .ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to extract recognize entities from document");
            }

            return results ?? Array.Empty<Entity>();
        }

        private TextAnalyticsClient CreateClient()
        {
            return new TextAnalyticsClient(new Uri(options.MbLanguageEndpoint), new AzureKeyCredential(options.MbLanguageKey));
        }

        private EntityCategory MapCategory(AzureEntityCategory azureCategory)
        {
            EntityCategory category = EntityCategory.Unknown;

            if (azureCategory == AzureEntityCategory.Person)
                category = EntityCategory.Person;
            else if (azureCategory == AzureEntityCategory.Address)
                category = EntityCategory.Address;
            else if (azureCategory == AzureEntityCategory.Quantity)
                category = EntityCategory.Quantity;
            else if (azureCategory == AzureEntityCategory.IPAddress)
                category = EntityCategory.IPAddress;
            else if (azureCategory == AzureEntityCategory.Email)
                category = EntityCategory.Email;
            else if (azureCategory == AzureEntityCategory.PhoneNumber)
                category = EntityCategory.PhoneNumber;
            else if (azureCategory == AzureEntityCategory.DateTime)
                category = EntityCategory.DateTime;
            else if (azureCategory == AzureEntityCategory.Url)
                category = EntityCategory.Url;
            else if (azureCategory == AzureEntityCategory.Product)
                category = EntityCategory.Product;
            else if (azureCategory == AzureEntityCategory.Event)
                category = EntityCategory.Event;
            else if (azureCategory == AzureEntityCategory.Organization)
                category = EntityCategory.Organization;
            else if (azureCategory == AzureEntityCategory.Location)
                category = EntityCategory.Location;
            else if (azureCategory == AzureEntityCategory.PersonType)
                category = EntityCategory.PersonType;
            else if (azureCategory == AzureEntityCategory.Skill)
                category = EntityCategory.Skill;

            return category;
        }
    }
}
