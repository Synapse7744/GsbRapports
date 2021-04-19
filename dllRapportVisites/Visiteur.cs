using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllRapportVisites
{
    public class Visiteur
    {
        public string id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string ville { get; set; }
        public string adresse { get; set; }
        public string cp { get; set; }
        public DateTime dateEmbauche { get; set; }
        public Visiteur(string id, string nom, string ville, string adresse, string cp, string prenom, DateTime date)
        {
            this.adresse = adresse;
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.cp = cp;
            this.ville = ville;
            this.dateEmbauche = date;

        }
    }
}
