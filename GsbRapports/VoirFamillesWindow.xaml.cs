using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Dynamic;
using dllRapportVisites;
using Newtonsoft.Json;

namespace GsbRapports
{
    /// <summary>
    /// Logique d'interaction pour VoirFamillesWindow.xaml
    /// </summary>
    public partial class VoirFamillesWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        public VoirFamillesWindow(WebClient wb, string site, Secretaire laSecretaire)
        {
            InitializeComponent();
            this.wb = wb;
            this.site = site;
            this.laSecretaire = laSecretaire;

            string url = this.site + "familles?ticket=" + this.laSecretaire.getHashTicketMdp();
            string reponse = this.wb.DownloadString(url);

            dynamic d = JsonConvert.DeserializeObject(reponse);

            string familles = d.familles.ToString();
            string ticket = d.ticket;

            List<Famille> f = JsonConvert.DeserializeObject<List<Famille>>(familles);
            this.dtg.ItemsSource = f;

            this.laSecretaire.ticket = ticket;



        }
    }
}
