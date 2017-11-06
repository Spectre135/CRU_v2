using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppCRU_V2.Classes
{
    public class ucClass
    {
        public static void ucNavigate(Grid grd, UserControl uc)
        {
            if (grd.Children.Count > 0)
            {
                grd.Children.Clear();
                grd.Children.Add(uc);
            } else {
                grd.Children.Add(uc);
            }
        }

    }
}
