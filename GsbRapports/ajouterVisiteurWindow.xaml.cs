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
    /// Logique d'interaction pour ajouterVisiteurWindow.xaml
    /// </summary>
    /// 
    public partial class ajouterVisiteurWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        public ajouterVisiteurWindow(WebClient wb, string site, Secretaire laSecretaire)
        {
            InitializeComponent();
            this.wb = wb;
            this.site = site;
            this.laSecretaire = laSecretaire;
        }




        private void btnAjouter_Click_1(object sender, RoutedEventArgs e)
        {

            string idVisiteur = this.txtId.Text;
            string nom = this.txtNom.Text;
            string prenom = this.txtPrenom.Text;
            string adresse = this.txtAdresse.Text;
            string cp = this.txtCp.Text;
            string ville = this.txtVille.Text;
            string dateEmbauche = this.txtDateEmbauche.SelectedDate.Value.ToString("yyyy-MM-dd");

            bool test = int.TryParse(cp, out int y);
            bool chiffre = false;

            for(int i = 0; i<nom.Length; i++)
            {
                //nom[i] "[0-9]";
            }

            if (cp.Length != 5 || !test)
            {
                MessageBox.Show("NON!");
            }
            else {
                try
                {





                    string ticket1 = this.laSecretaire.getHashTicketMdp();

                    string url = this.site + "visiteurs";

                    NameValueCollection parametres = new NameValueCollection();
                    parametres.Add("ticket", ticket1);
                    parametres.Add("idVisiteur", idVisiteur);
                    parametres.Add("nom", nom);
                    parametres.Add("prenom", prenom);
                    parametres.Add("ville", ville);
                    parametres.Add("adresse", adresse);
                    parametres.Add("cp", cp);
                    parametres.Add("dateEmbauche", dateEmbauche);

                    byte[] tabByte = this.wb.UploadValues(url, "POST", parametres);
                    string reponse = UnicodeEncoding.UTF8.GetString(tabByte);
                    string ticket = reponse.Substring(2, reponse.Length - 2);
                    this.laSecretaire.ticket = ticket;
                    MessageBox.Show("Votre Ajout à été fait");
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse)
                        MessageBox.Show(((HttpWebResponse)ex.Response).StatusCode.ToString());


                }
            }
            

            
        }

      
    }
}
