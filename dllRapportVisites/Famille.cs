using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllRapportVisites
{
   public  class Famille
    {
        public string id { get; set; }
        public string libelle { get; set; }
        public Famille(string id, string lib)
        {
            this.id = id;
            this.libelle = lib;
        }
    }
}
