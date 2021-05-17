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



namespace GsbRapports
{
    /// <summary>
    /// Logique d'interaction pour GererVisiteursWindow.xaml
    /// </summary>
    public partial class GererVisiteurWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        public GererVisiteurWindow(WebClient wb,  string site, Secretaire laSecretaire)
        {
            InitializeComponent();

            this.wb = wb;
            this.laSecretaire = laSecretaire;
            this.site = site;



            string url = this.site + "visiteurs?ticket=" + this.laSecretaire.getHashTicketMdp();
            string reponse = this.wb.DownloadString(url);

            dynamic d = JsonConvert.DeserializeObject(reponse);

            string ticket = d.ticket;
            this.laSecretaire.ticket = ticket;

            string visiteurs = d.visiteurs.ToString();
            List<Visiteur> v = JsonConvert.DeserializeObject<List<Visiteur>>(visiteurs);

            this.cbxVisiteursNom.ItemsSource = v;
            this.cbxVisiteursNom.DisplayMemberPath = "nom";
        }



        

        private void btnValider_Click_1(object sender, RoutedEventArgs e)
        {
                
                    string idVisiteur = ((Visiteur)this.cbxVisiteursNom.SelectedItem).id.ToString();
                    string url = this.site + "visiteur";
                    string ticket1 = this.laSecretaire.getHashTicketMdp();

                    NameValueCollection parametres = new NameValueCollection();
                    parametres.Add("adresse", this.txtAdresse.Text);
                    parametres.Add("cp", this.txtCp.Text);
                    parametres.Add("ville", this.txtVille.Text);
                    parametres.Add("ticket", ticket1);
                    parametres.Add("idVisiteur", idVisiteur);


                    byte[] tabByte = this.wb.UploadValues(url, "POST", parametres);
                    string reponse = UnicodeEncoding.UTF8.GetString(tabByte);
                    string ticket = reponse.Substring(2, reponse.Length - 2);
                    this.laSecretaire.ticket = ticket;

                MessageBox.Show("Modification effectuée");

        
               
            
        }
    }
}