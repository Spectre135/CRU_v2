using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppCRU_V2.Classes;
using WpfAppCRU_V2.DataModel;
using WpfAppCRU_V2.UserController;

namespace WpfAppCRU_V2.Windows
{
    /// <summary>
    /// Interaction logic for winUporabnik.xaml
    /// </summary>
    public partial class winUporabnik : Window
    {
        //Base okno
        winCRU wc = (winCRU)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        protected static Uporabniki uporabniki = new Uporabniki();

        public Uporabniki Uporabniki
        {
            get
            {
                return uporabniki;
            }
            set
            {
                uporabniki = value;
            }
        }



        public winUporabnik()
        {
            InitializeComponent();

        }

        //ucUporabniki ucUporabniki = (ucUporabniki)Application.Current.Windows.OfType<UserControl>().SingleOrDefault(x => x.IsInitialized);


        //ucUporabniki._uporabniki.ElementAt<1>;




        private void btnPreklici_Click(object sender, RoutedEventArgs e)
        {
            wc.Opacity = 1;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUporabnikID.Text = uporabniki.UporabnikID;
            txtIme.Text = uporabniki.Ime;
            txtPriimek.Text = uporabniki.Priimek;
            txtWinUsername.Text = uporabniki.WinUsername;
            txtRFID.Text = uporabniki.RFID;
        }

        private void btnShrani_Click(object sender, RoutedEventArgs e)
        {
            uporabniki.UporabnikID = txtUporabnikID.Text;
            uporabniki.Ime = txtIme.Text;
            uporabniki.Priimek = txtPriimek.Text;
            uporabniki.WinUsername = txtWinUsername.Text;
            uporabniki.RFID = txtRFID.Text;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31207/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.PostAsJsonAsync("api/uporabniki/save/", uporabniki).Result;

            if (response.IsSuccessStatusCode)
            {

                using (new ucWaitCursor())
                {
                    ucClass.ucNavigate(wc.mainContent, new ucUporabniki());
                }

                wc.Opacity = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
