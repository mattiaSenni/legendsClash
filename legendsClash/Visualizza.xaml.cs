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
    /// Logica di interazione per VisualizzaArma.xaml
    /// </summary>
    public partial class Visualizza : Window
    {
        Asset asset;
        public Visualizza(Asset a)
        {
            InitializeComponent();

            asset = a;

            RiempiListaPersonaggi();
            RiempiListaArmi();
        }


        private void RiempiListaPersonaggi()
        {
            lstPersonaggi.Items.Clear();
            foreach (Personaggio p in asset.Personaggi)
            {
                lstPersonaggi.Items.Add(p.ToString());
            }
        }

        private void RiempiListaArmi()
        {
            lstArmi.Items.Clear();
            foreach (Arma a in asset.Armi)
            {
                lstArmi.Items.Add(a.ToString());
            }
        }

        private void btnAggPersonaggio_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AggiungiPersonaggio aggpers = new AggiungiPersonaggio(asset);
            aggpers.Show();
        }

        private void btnAggArma_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AggiungiArma aggar = new AggiungiArma(asset);
            aggar.Show();
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
