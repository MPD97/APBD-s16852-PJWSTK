using System;
using System.Collections.Generic;

namespace Cw4
{
    public partial class Rodzina
    {
        public Rodzina()
        {
            Wymiana = new HashSet<Wymiana>();
        }

        public decimal IdRodzina { get; set; }
        public string Nazwisko { get; set; }
        public decimal IdKraj { get; set; }

        public virtual Kraj IdKrajNavigation { get; set; }
        public virtual ICollection<Wymiana> Wymiana { get; set; }
    }
}
