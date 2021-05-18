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
using System.Threading.Tasks;


namespace legendsClash
{
    /// <summary>
    /// Logica di interazione per Gioca.xaml
    /// </summary>
    public partial class Gioca : Window
    {
        public Gioca(Asset asset)
        {
            _asset = asset;
            InitializeComponent();
            //popolo tutte le listview
            PopolaList();
            PopolaComboBox();
        }

        Asset _asset;
        Battaglia _battaglia;
        Personaggio p1; Personaggio p2;
        Arma a1; Arma a2;
        CambioArma cambio;
        int nRound;
        Sfondo s;

        private void PopolaList()
        {
            //MessageBox.Show(_asset.Personaggi.ToString());
            lstPersonaggi1.ItemsSource = _asset.Personaggi;
            lstPersonaggi2.ItemsSource = _asset.Personaggi;
            lstArma1.ItemsSource = _asset.Armi;
            lstArma2.ItemsSource = _asset.Armi;
        }

        private void PopolaComboBox()
        {
            cmbCambioArma.ItemsSource = new List<string>() { "entrambi i giocatori", "cambia solo chi vince", "cambia " };
            cmbNumeroRound.ItemsSource = new List<int>() { 1, 2, 3, 4, 5 };
            cmbScenario.ItemsSource = _asset.Scenari.Sfondi;
        }

        private ImageSource CambiaImg(string s)
        {
            Uri a = new Uri(s, UriKind.Relative);
            return new BitmapImage(a);
        }

        //----------------------------------------------------------------------------------------------------------
        //Sezione per selezionare i personaggi
        private void btnCambiaArma_Click(object sender, RoutedEventArgs e)
        {
            //salvo i 2 personaggi selezionati
            try
            {
                p1 = (Personaggio)lstPersonaggi1.SelectedItem;
                p2 = (Personaggio)lstPersonaggi2.SelectedItem;
                //cambio la grid
                grdScegliPersonaggio.Visibility = Visibility.Hidden;
                grdScegliArma.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void lstPersonaggi1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'immagine 1
            Personaggio p = (Personaggio)lstPersonaggi1.SelectedItem;
            
            imgPersonaggio1.Source = CambiaImg(p.sourceImmagine);
        }

        private void lstPersonaggi2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'immagine 2
            Personaggio p = (Personaggio)lstPersonaggi2.SelectedItem;
            imgPersonaggio2.Source = CambiaImg(p.sourceImmagine);
        }


        //---------------------------------------------------------------------------------------------------------
        //Sezione per selezionare le armi

        private void btnImpostazioni_Click(object sender, RoutedEventArgs e)
        {
            //salvo le due arme selezionate
            try
            {
                a1 = (Arma)lstArma1.SelectedItem;
                a2 = (Arma)lstArma2.SelectedItem;
                //cambio la grid
                grdScegliArma.Visibility = Visibility.Hidden;
                grdImpostazioni.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstArma1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'immagine arma 1
            Arma a = (Arma)lstArma1.SelectedItem;
            imgArma1.Source = CambiaImg(a.SourceImmagine);
        }

        private void lstArma2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'immagine arma 2
            Arma a = (Arma)lstArma2.SelectedItem;
            imgArma2.Source = CambiaImg(a.SourceImmagine);
        }

        //---------------------------------------------------------------------------------------------------------
        //Sezione per le impostazioni

        private void btnInizioCombattimento_Click(object sender, RoutedEventArgs e)
        {
            //devo prendere tutti i dati
            try
            {
                
                cambio = (CambioArma)cmbCambioArma.SelectedIndex;
                nRound = (int)cmbNumeroRound.SelectedItem;
                s = (Sfondo)cmbScenario.SelectedItem;
                _battaglia = new Battaglia(p1, p2, a1, a2, cambio, nRound, s);
                lblNome1.Content = _battaglia.Personaggio1.nome;
                lblNome2.Content = _battaglia.Personaggio2.nome;
                lblDannoArma1.Content = _battaglia.ArmaGiocatore1.DannoMassimo;
                lblDannoArma2.Content = _battaglia.ArmaGiocatore2.DannoMassimo;
                AggiornaInterfacciaCombattimento();
                //inizializzo la battaglia e gli elementi grafici
                
                //inserisco i valori nell'interfaccia
                
                imgG1.Source = CambiaImg(_battaglia.Personaggio1.sourceImmagine);
                imgG2.Source = CambiaImg(_battaglia.Personaggio2.sourceImmagine);
                grdImpostazioni.Visibility = Visibility.Hidden;
                grdGioco.Visibility = Visibility.Visible;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AggiornaInterfacciaCombattimento()
        {   
            lblPuntiVita1.Content = _battaglia.Personaggio1.PuntiFerita;
            lblPuntiVita2.Content = _battaglia.Personaggio2.PuntiFerita;
        }
        //---------------------------------------------------------------------------------------------------------
        //Sezione per la battaglia

        private void btnAttacca_Click(object sender, RoutedEventArgs e)
        {
            int dado1;
            int dado2;
            bool dannoCritico;
            int percentualeDannoCritico;
            Personaggio vincitore = _battaglia.Attacco(out dado1, out dado2, out dannoCritico, out percentualeDannoCritico);
            //se vincitore == null allora non c'è un vincitore
            //se i dadi sono pari mostro il bottone continua, quindi vincitore == null
            //non c'è vincitore
            //cosa faccio :
            ///aggiorno l'interfaccia
            ///mostro i dadi
            ///animo i dadi
            ///se c'è danno critico mostro per 2 secondi un pop up che dice che c'è stato danno critico
            ///mostro il bottone continua
            AggiornaInterfacciaCombattimento();

            imgDado1.Source = CambiaImg("dado" + dado1);
            imgDado2.Source = CambiaImg("dado" + dado2);

            _movimentoDado1 = true;
            _movimentoDado2 = true;
            AnimaCombattente d1 = new AnimaCombattente(imgDado1.Margin);
            AnimaCombattente d2 = new AnimaCombattente(imgDado1.Margin);
            AnimaDado1(d1);
            AnimaDado2(d2);

            if (dannoCritico)
            {
                MostraDannoCritico();
            }
            if (vincitore != null)
            {
                System.Threading.Thread.Sleep(2000);
                grdPopUp.Visibility = Visibility.Hidden;
                //mostro la grid perchè c'è un vincitore
                //controllo se la partita è finita
                if(_battaglia.RoundCorrente == _battaglia.NumeroRound)
                {
                    //la partita è finita
                    //TODO : mostra la grid di fine partita
                }
                else
                {
                    //mostro il fine round

                }
                    
            }
        }

        private bool _movimentoDado1;
        private bool _movimentoDado2;

        private async void AnimaDado1(AnimaCombattente a)
        {
            await Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgDado1.Margin = a.Mossa();
                }));
                if(_movimentoDado1)
                    AnimaDado1(a);
               
            });
        }
        private async void AnimaDado2(AnimaCombattente a)
        {
            await Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgDado2.Margin = a.Mossa();
                }));
                if(_movimentoDado2)
                    AnimaDado2(a);
            });
        }

        private async void MostraDannoCritico()
        {
            await Task.Run(() =>
            {
                grdDannoCritico.Visibility = Visibility.Visible;
                System.Threading.Thread.Sleep(2000);
                grdDannoCritico.Visibility = Visibility.Hidden;
            });
        }

        private void btnContinua_Click(object sender, RoutedEventArgs e)
        {
            grdPopUp.Visibility = Visibility.Hidden;
        }

        private void btnNewRound_Click(object sender, RoutedEventArgs e)
        {
            grdVincitoreRound.Visibility = Visibility.Hidden;
            grdGioco.Visibility = Visibility.Visible;
            //inizio un nuovo round
            _battaglia.NewRound();
            AggiornaInterfacciaCombattimento();
        }
    }
}
