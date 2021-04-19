using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllRapportVisites
{
    public class Medicament
    {
        public string id { get; set; }
        public string nomCommercial { get; set; }
        public string composition { get; set; }
        public string contreIndications { get; set; }
        public string effets { get; set; }
        public string idFamille { get; set; }
        public Medicament(string id, string nom, string composition, string contreindic, string effets, string f)
        {
            this.id = id;
            this.nomCommercial = nom;
            this.effets = effets;
            this.composition = composition;
            this.contreIndications = contreindic;
            this.idFamille = f;
        }
    }
}
