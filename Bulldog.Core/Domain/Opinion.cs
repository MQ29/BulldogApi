using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Opinion
    {
        public Guid Id { get; protected set; }
        public User Author { get; protected set; }
        public string Content { get; protected set; }
        public int Rating { get; protected set; }

    }
}
