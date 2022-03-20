using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.AzureAi
{
    public class Entity
    {
        public Entity(string text, EntityCategory category, string subCategory, double confidenceScore)
        {
            Text = text;
            Category = category;
            SubCategory = subCategory;
            ConfidenceScore = confidenceScore;
        }

        public string Text { get; }

        /// <summary>
        /// https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/named-entity-recognition/concepts/named-entity-categories
        /// </summary>
        public EntityCategory Category { get; }

        public string SubCategory { get; }
        
        public double ConfidenceScore { get; }
    }
}
