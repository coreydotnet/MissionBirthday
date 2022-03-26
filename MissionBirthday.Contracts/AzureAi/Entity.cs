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

        public override string ToString() => $"{Text}, {Category}{(!string.IsNullOrEmpty(SubCategory) ? $" - {SubCategory}" : "")}, {ConfidenceScore}";
    }

    public static class EntityExtensions
    {
        public static bool IsAddress(this Entity e) => e.Category == EntityCategory.Address;

        public static bool IsGpeLocation(this Entity e) => e.Category == EntityCategory.Location && e.IsSubCategory("GPE");

        public static bool IsNumberQuantity(this Entity e) => e.Category == EntityCategory.Quantity && e.IsSubCategory("Number");
        
        public static string TextOrDefault(this Entity entity, string defaultValue = "") => entity?.Text ?? defaultValue;

        public static bool IsDateOrDateRange(this Entity e) => e.Category == EntityCategory.DateTime && (e.IsSubCategory("Date") || e.IsSubCategory("DateRange"));
        
        public static bool IsTimeOrTimeRange(this Entity e) => e.Category == EntityCategory.DateTime && (e.IsSubCategory("Time") || e.IsSubCategory("TimeRange"));

        public static bool IsSubCategory(this Entity e, string subCategory) => string.Equals(e.SubCategory, subCategory, StringComparison.Ordinal);
    }
}
