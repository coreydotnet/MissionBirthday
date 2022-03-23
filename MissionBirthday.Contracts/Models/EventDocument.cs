namespace MissionBirthday.Contracts.Models
{
    public class EventDocument
    {
        public EventDocument(string originalDocument, Event mbEvent)
        {
            OriginalDocument = originalDocument;
            Event = mbEvent;
        }

        public string OriginalDocument { get; }

        public Event Event { get; }
    }
}
