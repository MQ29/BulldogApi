namespace Bulldog.Core.Domain
{
    public class AvailabilitySlot
    {
        public DateTime StartDateTime { get; protected set; }
        public DateTime EndDateTime { get; protected set; }

        public AvailabilitySlot(DateTime startDateTime, DateTime endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public bool Overlap(DateTime start, DateTime end)
        {
            return StartDateTime < end && EndDateTime > start;
        }

    }
}
