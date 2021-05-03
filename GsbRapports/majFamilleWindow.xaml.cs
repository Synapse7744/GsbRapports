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
    /// Logique d'interaction pour majFamilleWindow.xaml
    /// </summary>
    public partial class majFamilleWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        public majFamilleWindow(WebClient wb, string site, Secretaire laSecretaire)
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
            this.laSecretaire.ticket = ticket;


            List<Famille> f = JsonConvert.DeserializeObject<List<Famille>>(familles);

            this.cmbFamille.ItemsSource = f;
            this.cmbFamille.DisplayMemberPath ="idLibelle";



        }


        private void btnValider_Click_1(object sender, RoutedEventArgs e)
        {
            string newLibelle = txtLibFamille.Text;
            string idFamille = ((Famille)cmbFamille.SelectedItem).id;
            try
            {
                string url = this.site + "famille/";

                NameValueCollection parametres = new NameValueCollection();
                parametres.Add("ticket", this.laSecretaire.getHashTicketMdp());
                parametres.Add("idFamille", idFamille);
                parametres.Add("libelle", newLibelle);

                byte[] tabByte = this.wb.UploadValues(url, "POST", parametres);
                string reponse = UnicodeEncoding.UTF8.GetString(tabByte);
                string ticket = reponse.Substring(2, reponse.Length-2);
                this.laSecretaire.ticket = ticket;

            }
            catch(WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                    MessageBox.Show(((HttpWebResponse)ex.Response).StatusCode.ToString());

            }
            



 


        }
    }
}
