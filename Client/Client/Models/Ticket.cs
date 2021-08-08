using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Ticket
    {
        public int Variant { get; set; }
        public int СirculationNum { get; set; }
        public string SelectedNum { get; set; }

        public virtual Circulation СirculationNumNavigation { get; set; }
    }
}
