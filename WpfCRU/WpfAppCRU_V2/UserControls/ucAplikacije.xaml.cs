using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using WpfAppCRU_V2.DataModel;

namespace WpfAppCRU_V2.UserController
{
    /// <summary>
    /// Interaction logic for ucAplikacije.xaml
    /// </summary>
    public partial class ucAplikacije : UserControl
    {
        public ucAplikacije()
        {
            InitializeComponent();
        }

        private void BindAplikacijeListAsync()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:31207/")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("api/aplikacije2/undefined?asc=null&pageIndex=1&pageSizeSelected=10&sortKey=null").Result;

                if (response.IsSuccessStatusCode)
                {

                    ResponseData data = JsonConvert.DeserializeObject<ResponseData>(response.Content.ReadAsStringAsync().Result);

                    grdAplikacije.ItemsSource = data.DataList;

                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:Message - " + ex.Message);
            }

        }

        private void BindAplikacijeListEmpty()
        {
            ResponseData emptyData = new ResponseData();
            grdAplikacije.ItemsSource = emptyData.DataList;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindAplikacijeListAsync();
        }

        private void btnIskanje_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
