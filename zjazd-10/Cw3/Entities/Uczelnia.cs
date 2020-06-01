using System;
using System.Collections.Generic;

namespace Cw4
{
    public partial class Uczelnia
    {
        public Uczelnia()
        {
            Uczen = new HashSet<Uczen>();
            Wymiana = new HashSet<Wymiana>();
        }

        public decimal IdUczelnia { get; set; }
        public string Nazwa { get; set; }
        public string Kierunek { get; set; }
        public decimal IdKraj { get; set; }

        public virtual Kraj IdKrajNavigation { get; set; }
        public virtual ICollection<Uczen> Uczen { get; set; }
        public virtual ICollection<Wymiana> Wymiana { get; set; }
    }
}
