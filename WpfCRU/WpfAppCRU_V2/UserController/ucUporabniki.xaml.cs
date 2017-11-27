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

        private void BindUporabnikiList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31207/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/uporabniki").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var uporabniki = JsonConvert.DeserializeObject<List<Uporabniki>>(responseData);

                grdUporabniki.ItemsSource = uporabniki;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindUporabnikiList();
        }
    }
}
