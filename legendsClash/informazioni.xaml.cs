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

namespace legendsClash
{
    /// <summary>
    /// Logica di interazione per informazioni.xaml
    /// </summary>
    public partial class informazioni : Window
    {
        public informazioni(Asset asset)
        {
            InitializeComponent();
            _asset = asset;
        }
        Asset _asset;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow(_asset);
            m.Show();
            this.Close();
        }
    }
}
