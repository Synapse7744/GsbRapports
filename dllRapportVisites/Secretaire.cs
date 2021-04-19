using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace dllRapportVisites
{
    public class Secretaire
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string ticket { get; set; }
        public string mdp { get; set; }
        public Secretaire()
        {
            this.nom = "";
            this.prenom = "";
            this.ticket = "";
            this.mdp = "";
        }
        public string getHashTicketMdp()
        {
            byte[] data = new byte[160];
            /*Déclaration et construction d'un objet de hashage*/
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result;
            /* On ne peut hasher que des tableaux de byte, on demande donc à convertir le ticket + le mdp en tableau de byte*/
            data = System.Text.Encoding.ASCII.GetBytes(this.ticket + this.mdp);
            /* Le résultat du hash est récupéré dans un tableau de byte*/
            result = sha.ComputeHash(data);
            /*Enfin, on convertit le hash en string !!*/
            string hash = BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();

            return hash;
        }
    }
}
