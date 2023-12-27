using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class AvailabilityCalendar
    {
        private List<AvailabilitySlot> availabilitySlots;

        public AvailabilityCalendar()
        {
            availabilitySlots = new List<AvailabilitySlot>();
        }

        public void SetAvailability(DateTime startDateTime, DateTime endDateTime)
        {
            if (availabilitySlots.Exists(x => x.Overlap(startDateTime, endDateTime)))
            {
                throw new Exception("Selected time slot is already occupied");
            }

            availabilitySlots.Add(new AvailabilitySlot(startDateTime, endDateTime));
        }

        public bool IsAvailable(DateTime startDateTime, DateTime endDateTime)
        {
            return !availabilitySlots.Exists(x => x.Overlap(startDateTime, endDateTime));
        }
    }
}
