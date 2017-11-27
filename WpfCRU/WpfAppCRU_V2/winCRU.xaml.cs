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
using System.Windows.Shapes;
using WpfAppCRU_V2.Classes;
using WpfAppCRU_V2.UserController;

namespace WpfAppCRU_V2
{
    /// <summary>
    /// Interaction logic for winCRU.xaml
    /// </summary>
    public partial class winCRU : Window
    {
        public winCRU()
        {
            InitializeComponent();
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        #region Header menu
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        #endregion

        #region Window move
        private void brdLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void brdGlava_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

        #region Side menu
        private void btnAplikacije_Click(object sender, RoutedEventArgs e)
        {
            izberiMenu("btnAplikacije");
            ucClass.ucNavigate(mainContent, new ucAplikacije());
        }

        private void btnVloge_Click(object sender, RoutedEventArgs e)
        {
            izberiMenu("btnVloge");
            ucClass.ucNavigate(mainContent, new ucVloge());
        }

        private void btnPravice_Click(object sender, RoutedEventArgs e)
        {
            izberiMenu("btnPravice");
            ucClass.ucNavigate(mainContent, new ucPravice());
        }

        private void btnUporabniki_Click(object sender, RoutedEventArgs e)
        {
            izberiMenu("btnUporabniki");
            ucClass.ucNavigate(mainContent, new ucUporabniki());
        }

        void izberiMenu(string menu)
        {
            if (menu != "btnAplikacije") { btnAplikacije.IsChecked = false; } else { btnAplikacije.IsChecked = true; };
            if (menu != "btnVloge") { btnVloge.IsChecked = false; } else { btnVloge.IsChecked = true; };
            if (menu != "btnPravice") { btnPravice.IsChecked = false; } else { btnPravice.IsChecked = true; };
            if (menu != "btnUporabniki") { btnUporabniki.IsChecked = false; } else { btnUporabniki.IsChecked = true; };
        }
        #endregion

    }
}
