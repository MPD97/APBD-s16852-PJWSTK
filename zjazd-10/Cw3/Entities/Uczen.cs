using System;
using System.Collections.Generic;

namespace Cw4
{
    public partial class Uczen
    {
        public Uczen()
        {
            Wymiana = new HashSet<Wymiana>();
        }

        public decimal IdUczen { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public decimal IdUczelnia { get; set; }
        public decimal IdKraj { get; set; }

        public virtual Kraj IdKrajNavigation { get; set; }
        public virtual Uczelnia IdUczelniaNavigation { get; set; }
        public virtual ICollection<Wymiana> Wymiana { get; set; }
    }
}
