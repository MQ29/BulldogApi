using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public class UpdateAvailableHours
    {
        public int Duration { get; set; }
        public DateTime? SelectedHour { get; set; }
    }
}
