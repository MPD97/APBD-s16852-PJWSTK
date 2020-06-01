using System;
using System.Collections.Generic;

namespace Cw4
{
    public partial class Wymiana
    {
        public decimal IdWymiana { get; set; }
        public decimal IdUczen { get; set; }
        public decimal IdUczelnia { get; set; }
        public decimal IdRodzina { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        public virtual Rodzina IdRodzinaNavigation { get; set; }
        public virtual Uczelnia IdUczelniaNavigation { get; set; }
        public virtual Uczen IdUczenNavigation { get; set; }
    }
}
