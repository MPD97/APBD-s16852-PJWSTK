using System;
using System.Collections.Generic;

namespace Cw4
{
    public partial class Kraj
    {
        public Kraj()
        {
            Rodzina = new HashSet<Rodzina>();
            Uczelnia = new HashSet<Uczelnia>();
            Uczen = new HashSet<Uczen>();
        }

        public decimal IdKraj { get; set; }
        public string Nazwa { get; set; }
        public string CzyUe { get; set; }

        public virtual ICollection<Rodzina> Rodzina { get; set; }
        public virtual ICollection<Uczelnia> Uczelnia { get; set; }
        public virtual ICollection<Uczen> Uczen { get; set; }
    }
}
