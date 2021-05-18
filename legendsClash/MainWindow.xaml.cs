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

namespace legendsClash
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Ladro p1 = new Ladro("luigi", LimitiPersonaggi.LADRO_VITA_MAX, "img/ladro.jpg", LimitiPersonaggi.LADRO_POSSIBILITA_DANNO_CRITICO_MAX, LimitiPersonaggi.LADRO_DANNO_CRITICO_MAX);
            Ladro p2 = new Ladro("mario", LimitiPersonaggi.LADRO_VITA_MIN, "img/ladro.jpg", LimitiPersonaggi.LADRO_POSSIBILITA_DANNO_CRITICO_MAX, LimitiPersonaggi.LADRO_DANNO_CRITICO_MAX);
            Arma a1 = new Arma('D', "super mega arma", "img/vs.png");
            _asset = new Asset(new List<Arma>() { a1 }, new List<Personaggio>() { p1, p2 }, new Scenari(new List<Sfondo>() { new Sfondo("img/sfondo.png", 0) }));
        }

        public MainWindow(Asset asset)
        {
            InitializeComponent();
            _asset = asset;
        }
        Asset _asset;

        private void btnIniziaBattaglia_Click(object sender, RoutedEventArgs e)
        {
            Gioca g = new Gioca(_asset);
            g.Show();
            this.Close();
        }

        private void btnVisualizza_Click(object sender, RoutedEventArgs e)
        {
            Visualizza v = new Visualizza(_asset);
            v.Show();
            this.Close();
        }

        private void btnAggiungiArma_Click(object sender, RoutedEventArgs e)
        {
            AggiungiArma a = new AggiungiArma(_asset);
            a.Show();
            this.Close();
        }

        private void btnAggiungiPersonaggio_Click(object sender, RoutedEventArgs e)
        {
            AggiungiPersonaggio a = new AggiungiPersonaggio(_asset);
            a.Show();
            this.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            informazioni a = new informazioni(_asset);
            a.Show();
            this.Close();
        }

        private void btnGioca_Click(object sender, RoutedEventArgs e)
        {
            Gioca g = new Gioca(_asset);
            g.Show(); this.Close();
        }
    }
}
