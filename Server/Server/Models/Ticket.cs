using System;
using System.Collections.Generic;

#nullable disable

namespace Server
{
    public partial class Ticket
    {
        public int Variant { get; set; }
        public int СirculationNum { get; set; }
        public string SelectedNum { get; set; }

        public virtual Circulation СirculationNumNavigation { get; set; }
    }
}
