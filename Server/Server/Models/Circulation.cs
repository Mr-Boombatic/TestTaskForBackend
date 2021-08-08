using System;
using System.Collections.Generic;

#nullable disable

namespace Server
{
    public partial class Circulation
    {
        public Circulation()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Circulation1 { get; set; }
        public string WinnerPosition { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
