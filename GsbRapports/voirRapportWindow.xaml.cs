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
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.IO;

namespace GsbRapports
{
    /// <summary>
    /// Logique d'interaction pour voirRapportWindow.xaml
    /// </summary>
    public partial class voirRapportWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        private List<Rapport>lesRapports;
        public voirRapportWindow(WebClient wb, string site, Secretaire laSecretaire)
        {
            InitializeComponent();
            this.wb = wb;
            this.site = site;
            this.laSecretaire = laSecretaire;

            string url = this.site + "visiteurs?ticket=" + this.laSecretaire.getHashTicketMdp();
            string reponse = this.wb.DownloadString(url);

            dynamic d = JsonConvert.DeserializeObject(reponse);

            string ticket = d.ticket;
            this.laSecretaire.ticket = ticket;

            string visiteurs = d.visiteurs.ToString();             


            List<Visiteur> v = JsonConvert.DeserializeObject<List<Visiteur>>(visiteurs);
            
            this.cbxVisiteurs.ItemsSource = v;
            this.cbxVisiteurs.DisplayMemberPath = "nomPrenom";


        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            string idVisiteur = ((Visiteur)this.cbxVisiteurs.SelectedItem).id.ToString();
            string date1 = this.date1.SelectedDate.Value.ToString("yyyy-MM-dd");
            string date2 = this.date2.SelectedDate.Value.ToString("yyyy-MM-dd");
            




            try
            {
                string url = this.site + "rapports?ticket=" + this.laSecretaire.getHashTicketMdp() + "&idVisiteur=" + idVisiteur + "&dateDebut=" + date1 + "&dateFin=" + date2;
                string reponse = this.wb.DownloadString(url);
                dynamic d = JsonConvert.DeserializeObject(reponse);

                string ticket = d.ticket;
                this.laSecretaire.ticket = ticket;

                string rapports = d.rapports.ToString();
                List<Rapport> rapportList = JsonConvert.DeserializeObject<List<Rapport>>(rapports);
                this.dtg.ItemsSource = rapportList;
                this.lesRapports = rapportList;

            }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse)
                        MessageBox.Show(((HttpWebResponse)ex.Response).StatusCode.ToString());

                }
        }


        private void btnXml_Click(object sender, RoutedEventArgs e)
        {
           


            ListeRapport r = new ListeRapport();
            r.lesRapports = this.lesRapports;

            FileStream f = new FileStream("listeRapport.xml", FileMode.Create);
            XmlSerializer x = new XmlSerializer(r.GetType());
            x.Serialize(f, r);
            MessageBox.Show("Importation effectuée.");

        }
    }
}
