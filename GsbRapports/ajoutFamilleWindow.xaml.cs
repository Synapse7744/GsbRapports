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
    /// Logique d'interaction pour ajoutFamilleWindow.xaml
    /// </summary>
    public partial class ajoutFamilleWindow : Window
    {
        private WebClient wb;
        private string site;
        private Secretaire laSecretaire;
        public ajoutFamilleWindow(WebClient wb, string site, Secretaire laSecretaire)
        {
            InitializeComponent();
            this.wb = wb;
            this.laSecretaire = laSecretaire;
            this.site = site;


        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            string id = this.txtId.Text;
            string libelle = this.txtLibelle.Text;
            string ticket1 = this.laSecretaire.getHashTicketMdp();

            
            try
            {
                string url = this.site + "familles";
                NameValueCollection parametres = new NameValueCollection();
                parametres.Add("ticket", ticket1);
                parametres.Add("idFamille", id);
                parametres.Add("libelle", libelle);


                byte[] tabByte = this.wb.UploadValues(url, "POST", parametres);
                string reponse = UnicodeEncoding.UTF8.GetString(tabByte);
                string ticket = reponse.Substring(2, reponse.Length - 2);
                this.laSecretaire.ticket = ticket;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                    MessageBox.Show(((HttpWebResponse)ex.Response).StatusCode.ToString());
            }





        }
    }
}
