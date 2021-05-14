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
    /// Logica di interazione per AggiungiArma.xaml
    /// </summary>
    public partial class AggiungiArma : Window
    {
        readonly Uri UriArmaS = new Uri("Img/ArmaS.jpeg", UriKind.Relative);
        readonly Uri UriArmaA = new Uri("Img/ArmaA.jpeg", UriKind.Relative);
        readonly Uri UriArmaB = new Uri("Img/ArmaB.jpeg", UriKind.Relative);
        readonly Uri UriArmaC = new Uri("Img/ArmaC.jpeg", UriKind.Relative);
        readonly Uri UriArmaD = new Uri("Img/ArmaD.jpeg", UriKind.Relative);

        private const int VAGGMAX_S = 20;
        private const int VAGGMAX_A = 15;
        private const int VAGGMAX_B = 10;
        private const int VAGGMAX_C = 5;

        public AggiungiArma()
        {
            InitializeComponent();
            this.comboClasse.Items.Add('S');
            this.comboClasse.Items.Add('A');
            this.comboClasse.Items.Add('B');
            this.comboClasse.Items.Add('C');
            this.comboClasse.Items.Add('D');
        }

        private void btnCreaArma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nome = txtNome.Text.ToString();
                char classe = (char)comboClasse.SelectedItem;
                int pfAgg = int.Parse(txtPFAgg.Text);
                string source = imgArma.Source.ToString();
                int percDannoAgg = -1; //il valore verrà assegnato nella classe

                Arma a = new Arma(classe, nome, source, pfAgg, percDannoAgg);

                this.Hide();
                MainWindow mw = new MainWindow(a);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pfMax = CalcolaPFMax();

            lblMaxMinPF.Content = "Max  " + pfMax;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            comboClasse.SelectedIndex = r.Next(0, 5); //5 = numero di classi + 1

            int pfMax = CalcolaPFMax();
            int pfAgg = r.Next(0, pfMax + 1);

            txtPFAgg.Text = pfAgg.ToString();
        }

        private int CalcolaPFMax()
        {
            int pfMax = 0;
            char classe = (char)comboClasse.SelectedItem;

            switch (classe)
            {
                case 'S':
                    pfMax = VAGGMAX_S;
                    ImageSource ArmaS = new BitmapImage(UriArmaS);
                    imgArma.Source = ArmaS;
                    break;

                case 'A':
                    pfMax = VAGGMAX_A;
                    ImageSource ArmaA = new BitmapImage(UriArmaA);
                    imgArma.Source = ArmaA;
                    break;

                case 'B':
                    pfMax = VAGGMAX_B;
                    ImageSource ArmaB = new BitmapImage(UriArmaB);
                    imgArma.Source = ArmaB;
                    break;

                case 'C':
                    pfMax = VAGGMAX_C;
                    ImageSource ArmaC = new BitmapImage(UriArmaC);
                    imgArma.Source = ArmaC;
                    break;

                case 'D':
                    ImageSource ArmaD = new BitmapImage(UriArmaD);
                    imgArma.Source = ArmaD;
                    break;
            }

            return pfMax;
        }
    }
}
