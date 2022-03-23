namespace MissionBirthday.Contracts.AzureAi
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/named-entity-recognition/concepts/named-entity-categories
    /// </summary>
    public enum EntityCategory
    {
        Unknown,

        /// <summary>
        /// Specifies that the entity corresponds to a Person.
        /// </summary>
        Person,

        /// <summary>
        /// Specifies that the entity contains an address.
        /// </summary>
        Address,

        /// <summary>
        /// Specifies that the entity contains a number or numeric quantity.
        /// </summary>
        Quantity,

        /// <summary>
        /// Specifies that the entity contains an Internet Protocol Address.
        /// </summary>
        IPAddress,

        /// <summary>
        /// Specifies that the entity contains an email address.
        /// </summary>
        Email,

        /// <summary>
        /// Specifies that the entity contains a phone number (US phone numbers only).
        /// </summary>
        PhoneNumber,

        /// <summary>
        /// Specifies that the entity contains a date, time or duration.
        /// </summary>
        DateTime,

        /// <summary>
        /// Specifies that the entity contains an Internet URL.
        /// </summary>
        Url,

        /// <summary>
        /// Specifies that the entity contains physical objects of various categories.
        /// </summary>
        Product,

        /// <summary>
        /// Specifies that the entity contains historical, social and natural-occuring events.
        /// </summary>
        Event,

        /// <summary>
        /// Specifies that the entity contains the name of an organization, corporation,
        /// </summary>
        ///     agency, or other group of people.
        Organization,

        /// <summary>
        /// Specifies that the entity contains natural or human-made landmarks, structures,
        /// </summary>
        ///     or geographical features.
        Location,

        /// <summary>
        /// Specifies that the entity corresponds to a job type or role held by a person.
        /// </summary>
        PersonType,

        /// <summary>
        /// Specifies an entity describing a capability or expertise.
        /// </summary>
        Skill
    }
}
