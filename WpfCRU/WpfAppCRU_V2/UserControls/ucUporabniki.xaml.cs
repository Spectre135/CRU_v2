using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http;
using System.Net.Http.Headers;
using WpfAppCRU_V2.DataModel;
using Newtonsoft.Json;
using WpfAppCRU_V2.Windows;
using WpfAppCRU_V2.Classes;
using System.Data;

namespace WpfAppCRU_V2.UserController
{
    /// <summary>
    /// Interaction logic for ucUporabniki.xaml
    /// </summary>
    public partial class ucUporabniki : UserControl
    {
        public ucUporabniki()
        {
            InitializeComponent();
        }

        //public DataView DV { get; set; }

        protected static List<Uporabniki> _uporabniki = new List<Uporabniki>();

        //public List<Uporabniki> _Uporabniki
        //{
        //    get
        //    {
        //        return _uporabniki;
        //    }
        //    set
        //    {
        //        _uporabniki = value;
        //    }
        //}

        public void BindUporabnikiList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31207/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/uporabniki").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                _uporabniki = JsonConvert.DeserializeObject<List<Uporabniki>>(responseData);

                grdUporabniki.ItemsSource = _uporabniki;
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (new ucWaitCursor())
            {
                BindUporabnikiList();
            }
        }



        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            winCRU wc = (winCRU)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            winUporabnik winUpo = new winUporabnik();
            winUpo.Owner = wc;
            winUpo.Uporabniki = new Uporabniki();
            wc.Opacity = 0.5;
            winUpo.ShowDialog();
        }

        private void btnSpremeni_Click(object sender, RoutedEventArgs e)
        {
            winCRU wc = (winCRU)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            winUporabnik winUpo = new winUporabnik();
            winUpo.Owner = wc;
            winUpo.Uporabniki = _uporabniki[grdUporabniki.SelectedIndex];
            wc.Opacity = 0.5;
            winUpo.ShowDialog();
        }

        private void grdUporabniki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bc = new BrushConverter();

            if (grdUporabniki.SelectedItems.Count > 1)
            {

                btnSpremeni.IsEnabled = false;
                btnSpremeni.Foreground = (Brush)bc.ConvertFrom("#663691D1");
            }
            else if (grdUporabniki.SelectedItems.Count == 1)
            {
                btnSpremeni.IsEnabled = true;
                btnSpremeni.Foreground = (Brush)bc.ConvertFrom("#FF3691D1");
            }


        }

        private void btnBrisi_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31207/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sMessageBoxText = "Ali res želite izbrisati izbrane uporabnike?";
            string sCaption = "Uporabniki";


            var rsltMessageBox = Windows.winMessageBox.Show (sCaption, sMessageBoxText, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    foreach (var data in grdUporabniki.SelectedItems)
                    {

                        Uporabniki upo = data as Uporabniki;

                        var response = client.PostAsJsonAsync("api/uporabniki/delete/", upo).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                        }
                    }
                    using (new ucWaitCursor())
                    {
                        winCRU wc = (winCRU)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                        ucClass.ucNavigate(wc.mainContent, new ucUporabniki());
                    }
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }





        }
    }
}
