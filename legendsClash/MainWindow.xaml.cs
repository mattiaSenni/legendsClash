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
using System.IO;
using System.Xml.Serialization;

namespace legendsClash
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Asset _asset;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //_asset = Deserializzazione();

                Ladro p1 = new Ladro("luigi", LimitiPersonaggi.LADRO_VITA_MAX, "img/ladro.jpeg", LimitiPersonaggi.LADRO_POSSIBILITA_DANNO_CRITICO_MAX, LimitiPersonaggi.LADRO_DANNO_CRITICO_MAX);
                Ladro p2 = new Ladro("mario", LimitiPersonaggi.LADRO_VITA_MIN, "img/ladro.jpeg", LimitiPersonaggi.LADRO_POSSIBILITA_DANNO_CRITICO_MAX, LimitiPersonaggi.LADRO_DANNO_CRITICO_MAX);
                Gigante p3 = new Gigante("marcello", LimitiPersonaggi.GIGANTE_VITA_MIN, "img/gigante.jpeg");
                Cavaliere p4 = new Cavaliere("spaghetti", LimitiPersonaggi.CAVALIERE_VITA_MIN, "img/cavaliere.jpeg", LimitiPersonaggi.CAVALIERE_DANNO_MIN);
                /*_asset.Personaggi.Add(p1);
                _asset.Personaggi.Add(p2);
                _asset.Personaggi.Add(p3);
                _asset.Personaggi.Add(p4);*/
                _asset = new Asset(new List<Arma>(), new List<Personaggio>() { p1, p2, p3, p4 }, new Scenari(new List<Sfondo>() { new Sfondo("ciao", 2) }));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile caricare il file, " + ex.Message);
                _asset = new Asset();
            }
            Serializza();
        }

        public MainWindow(Asset asset)
        {
            InitializeComponent();
            _asset = asset;

            Serializza();
        }

        public Asset Deserializzazione()
        {
            Asset asset = new Asset();

            if (!File.Exists("asset.xml"))
            {
                throw new FileNotFoundException("File non esistente");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Asset));

            using (Stream reader = new FileStream("asset.xml", FileMode.Open))
            {
                asset = (Asset)serializer.Deserialize(reader);
            }

            return asset;
        }

        public void Serializza()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Asset));
            TextWriter writer = new StreamWriter("asset.xml");

            serializer.Serialize(writer, _asset);
        }

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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Asset asset = new Asset();
            _asset = asset;
            Serializza();
        }
    }
}
