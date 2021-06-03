using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logica di interazione per Gioca.xaml
    /// </summary>
    public partial class Gioca : Window
    {
        public Gioca(Asset asset)
        {
            _asset = asset;
            InitializeComponent();
            //inizio il sorteggio dei dadi in grdPopUp
            Sorteggio();
            //popolo tutte le listview
            PopolaList();
            PopolaComboBox();
        }

        public Gioca(int i, int dado)
        {
            //associo alle immagini dei dadi di grdPopUp le source dei dadi estratti in battaglia
            InitializeComponent();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (i)
                {
                    case 0:
                        imgDado11.Source = CambiaImg("img/" + dado + ".png");
                        break;
                    case 1:
                        imgDado12.Source = CambiaImg("img/" + dado + ".png");
                        break;
                    case 2:
                        imgDado13.Source = CambiaImg("img/" + dado + ".png");
                        break;
                    case 3:
                        imgDado21.Source = CambiaImg("img/" + dado + ".png");
                        break;
                    case 4:
                        imgDado22.Source = CambiaImg("img/" + dado + ".png");
                        break;
                    case 5:
                        imgDado23.Source = CambiaImg("img/" + dado + ".png");
                        break;
                }
            }));

            Thread.Sleep(100);
        }

        Asset _asset;
        Battaglia battaglia;
        Personaggio p1; Personaggio p2;
        Arma a1; Arma a2;
        CambioArma cambio;
        int nRound;
        Sfondo s;
        Random r;
        int indexArmaDaTenere;
        //per i controlli di cambio arma con battaglia non istanziata
        int roundCorrente = 0;
        string vincitore;
        bool cambiaVincitore;

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
            cmbCambioArma.ItemsSource = new List<string>() { "cambiano entrambi i giocatori", "cambia solo chi vince", "cambia solo chi perde", "non cambia nessuno" };
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
                if (lstPersonaggi1.SelectedIndex >= 0 && lstPersonaggi2.SelectedIndex >= 0)
                {
                    p1 = (Personaggio)lstPersonaggi1.SelectedItem;
                    p2 = (Personaggio)lstPersonaggi2.SelectedItem;
                    //cambio la grid
                    grdScegliPersonaggio.Visibility = Visibility.Hidden;
                    grdScegliArma.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstPersonaggi1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPersonaggi1.SelectedIndex >= 0)
            {
                if (lstPersonaggi1.SelectedIndex == lstPersonaggi2.SelectedIndex)
                {
                    MessageBox.Show("Un altro giocatore ha già selezionato questo personaggio!");
                    lstPersonaggi1.SelectedIndex = -1;
                }
                else
                {
                    //aggiorno l'immagine 1
                    Personaggio p = (Personaggio)lstPersonaggi1.SelectedItem;

                    imgPersonaggio1.Source = CambiaImg(p.SourceImmagine);
                }
            }
        }

        private void lstPersonaggi2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPersonaggi2.SelectedIndex >= 0)
            {
                if (lstPersonaggi2.SelectedIndex == lstPersonaggi1.SelectedIndex)
                {
                    MessageBox.Show("Un altro giocatore ha già selezionato questo personaggio!");
                    lstPersonaggi2.SelectedIndex = -1;
                }
                else
                {
                    //aggiorno l'immagine 2
                    Personaggio p = (Personaggio)lstPersonaggi2.SelectedItem;
                    imgPersonaggio2.Source = CambiaImg(p.SourceImmagine);
                }
            }
        }


        //---------------------------------------------------------------------------------------------------------
        //Sezione per selezionare le armi

        private void btnImpostazioni_Click(object sender, RoutedEventArgs e)
        {
            //salvo le due arme selezionate
            try
            {
                if (lstArma1.SelectedIndex >= 0 && lstArma2.SelectedIndex >= 0)
                {
                    a1 = (Arma)lstArma1.SelectedItem;
                    a2 = (Arma)lstArma2.SelectedItem;
                    //cambio la grid
                    grdScegliArma.Visibility = Visibility.Hidden;
                    if (roundCorrente == 1)
                        grdGioco.Visibility = Visibility.Visible;
                    else
                        grdImpostazioni.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstArma1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roundCorrente == 0)
            {
                if (lstArma1.SelectedIndex >= 0)
                {
                    Arma appoggio = (Arma)lstArma1.SelectedItem;

                    if ((appoggio.Classe == 'C' && p1.NumeroVittorie >= 5) || (appoggio.Classe == 'B' && p1.NumeroVittorie >= 10) || (appoggio.Classe == 'A' && p1.NumeroVittorie >= 15) || (appoggio.Classe == 'S' && p1.NumeroVittorie >= 25) || appoggio.Classe == 'D')
                    {
                        //aggiorno l'immagine arma 1
                        Arma a = (Arma)lstArma1.SelectedItem;
                        imgArma1.Source = CambiaImg(a.SourceImmagine);
                    }
                    else
                    {
                        MessageBox.Show("Il personagio selezionato dal giocatore 1 non ha abbastanza vittorie per selezionare l'arma!");
                        lstArma1.SelectedIndex = -1;
                    }
                }
            }
            else
            {
                if ((cambiaVincitore == true && vincitore == "Personaggio 1") || (cambiaVincitore == false && vincitore == "Personaggio 2"))
                {
                    if (lstArma1.SelectedIndex >= 0)
                    {
                        Arma appoggio = (Arma)lstArma1.SelectedItem;

                        if ((appoggio.Classe == 'C' && p1.NumeroVittorie >= 5) || (appoggio.Classe == 'B' && p1.NumeroVittorie >= 10) || (appoggio.Classe == 'A' && p1.NumeroVittorie >= 15) || (appoggio.Classe == 'S' && p1.NumeroVittorie >= 25) || appoggio.Classe == 'D')
                        {
                            //aggiorno l'immagine arma 1
                            Arma a = (Arma)lstArma1.SelectedItem;
                            imgArma1.Source = CambiaImg(a.SourceImmagine);
                        }
                        else
                        {
                            MessageBox.Show("Il personagio selezionato dal giocatore 1 non ha abbastanza vittorie per selezionare l'arma!");
                            lstArma1.SelectedIndex = -1;
                        }
                    }
                }
                else
                {
                    lstArma1.SelectedIndex = indexArmaDaTenere;
                }
            }
        }

        private void lstArma2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roundCorrente == 0)
            {
                if (lstArma2.SelectedIndex >= 0)
                {
                    Arma appoggio = (Arma)lstArma2.SelectedItem;

                    if ((appoggio.Classe == 'C' && p1.NumeroVittorie >= 5) || (appoggio.Classe == 'B' && p1.NumeroVittorie >= 10) || (appoggio.Classe == 'A' && p1.NumeroVittorie >= 15) || (appoggio.Classe == 'S' && p1.NumeroVittorie >= 25) || appoggio.Classe == 'D')
                    {
                        //aggiorno l'immagine arma 2
                        Arma a = (Arma)lstArma2.SelectedItem;
                        imgArma2.Source = CambiaImg(a.SourceImmagine);
                    }
                    else
                    {
                        MessageBox.Show("Il personagio selezionato dal giocatore 2 non ha abbastanza vittorie per selezionare l'arma!");
                        lstArma2.SelectedIndex = -1;
                    }
                }
            }
            else
            {
                if ((cambiaVincitore == true && vincitore == "Personaggio 2") || (cambiaVincitore == false && vincitore == "Personaggio 1"))
                {
                    if (lstArma1.SelectedIndex >= 0)
                    {
                        Arma appoggio = (Arma)lstArma1.SelectedItem;

                        if ((appoggio.Classe == 'C' && p1.NumeroVittorie >= 5) || (appoggio.Classe == 'B' && p1.NumeroVittorie >= 10) || (appoggio.Classe == 'A' && p1.NumeroVittorie >= 15) || (appoggio.Classe == 'S' && p1.NumeroVittorie >= 25) || appoggio.Classe == 'D')
                        {
                            //aggiorno l'immagine arma 1
                            Arma a = (Arma)lstArma1.SelectedItem;
                            imgArma1.Source = CambiaImg(a.SourceImmagine);
                        }
                        else
                        {
                            MessageBox.Show("Il personagio selezionato dal giocatore 1 non ha abbastanza vittorie per selezionare l'arma!");
                            lstArma1.SelectedIndex = -1;
                        }
                    }
                }
                else
                {
                    lstArma2.SelectedIndex = indexArmaDaTenere;
                }
            }
            
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
                battaglia = new Battaglia(p1, p2, a1, a2, cambio, nRound, s);
                p1.PuntiFerita += a1.VitaAggiunta;
                p2.PuntiFerita += a2.VitaAggiunta;
                lblNome1.Content = "Punti ferita di " + battaglia.Personaggio1.Nome + ": " + battaglia.Personaggio1.PuntiFerita;
                lblNome2.Content = "Punti ferita di " + battaglia.Personaggio2.Nome + ": " + battaglia.Personaggio2.PuntiFerita;
                lblDannoArma1.Content = "Danno massimo: " + battaglia.ArmaGiocatore1.DannoMassimo;
                lblDannoArma2.Content = "Danno massimo: " + battaglia.ArmaGiocatore1.DannoMassimo;
                AggiornaInterfacciaCombattimento();
                //inizializzo la battaglia e gli elementi grafici

                //inserisco i valori nell'interfaccia

                imgG1.Source = CambiaImg(battaglia.Personaggio1.SourceImmagine);
                imgG2.Source = CambiaImg(battaglia.Personaggio2.SourceImmagine);
                grdImpostazioni.Visibility = Visibility.Hidden;
                grdGioco.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AggiornaInterfacciaCombattimento()
        {
            lblNome1.Content = "Punti ferita di " + battaglia.Personaggio1.Nome + ": " + battaglia.Personaggio1.PuntiFerita;
            lblNome2.Content = "Punti ferita di " + battaglia.Personaggio2.Nome + ": " + battaglia.Personaggio2.PuntiFerita;
        }
        //---------------------------------------------------------------------------------------------------------
        //Sezione per la battaglia

        private void btnAttacca_Click(object sender, RoutedEventArgs e)
        {
            //mostra solo il popup con i dadi
            MostraPopUp();
        }

        private void btnContinua_Click(object sender, RoutedEventArgs e)
        {
            //vero bottone attacca
            grdPopUp.Visibility = Visibility.Hidden;
            grdGioco.Visibility = Visibility.Visible;

            int dado; int dado1; int dado2;
            bool dannoCritico;
            int percentualeDannoCritico;
            Personaggio vincitore = battaglia.Attacco(out dado, out dado1, out dado2, out dannoCritico, out percentualeDannoCritico);

            //se vincitore == null allora non c'è un vincitore
            //se i dadi sono pari mostro il bottone continua, quindi vincitore == null
            //non c'è vincitore
            //cosa faccio :
            ///aggiorno l'interfaccia
            ///mostro i dadi
            ///animo i dadi
            ///se c'è danno critico mostro per 1 secondo un pop up che dice che c'è stato danno critico
            ///mostro il bottone continua

            AggiornaInterfacciaCombattimento();

            if (dado1 > dado2)
            {
                MessageBox.Show("Il giocatore 2 subisce " + dado + " danni");
            }
            else
            {
                if (dado1 < dado2)
                {
                    MessageBox.Show("Il giocatore 1 subisce " + dado + " danni");
                }
                else
                {
                    MessageBox.Show("Nessun giocatore subisce danni");
                }
            }

            if (dannoCritico == true)
            {
                MostraDannoCritico();
            }


            if (vincitore != null)
            {
                battaglia.RoundCorrente++;
                roundCorrente = battaglia.RoundCorrente;//per il controllo di grdImpostazioni
                Thread.Sleep(2000);
                grdPopUp.Visibility = Visibility.Hidden;
                //mostro la grid perchè c'è un vincitore
                //controllo se la partita è finita
                if (battaglia.RoundCorrente == battaglia.NumeroRound)
                {
                    //la partita è finita
                    grdFinePartita.Visibility = Visibility.Visible;
                    grdGioco.Visibility = Visibility.Hidden;

                    lblRoundGiocatore1.Content = battaglia.Vincitore;
                    lblRoundGiocatore2.Content = battaglia.Perdente;

                    if (battaglia.Vincitore == "giocatore 1")
                    {
                        imgFineG1.Source = CambiaImg(p1.SourceImmagine);
                        imgFineG2.Source = CambiaImg(p2.SourceImmagine);

                        AggiungiVittoria(p1);
                    }
                    else
                    {
                        imgFineG1.Source = CambiaImg(p2.SourceImmagine);
                        imgFineG2.Source = CambiaImg(p1.SourceImmagine);

                        AggiungiVittoria(p2);
                    }

                    //serializzo l'asset con le vittorie
                    //MainWindow mw = new MainWindow(_asset);
                }
                else
                {
                    //mostro il fine round
                    grdVincitoreRound.Visibility = Visibility.Visible;
                    grdGioco.Visibility = Visibility.Hidden;

                    lblRoundGiocatore1.Content = battaglia.Vincitore;
                    lblRoundGiocatore2.Content = battaglia.Perdente;

                    if (battaglia.Vincitore == "giocatore 1")
                    {
                        imgRoundG1.Source = CambiaImg(p1.SourceImmagine);
                        imgRoundG2.Source = CambiaImg(p2.SourceImmagine);
                    }
                    else
                    {
                        imgRoundG1.Source = CambiaImg(p2.SourceImmagine);
                        imgRoundG2.Source = CambiaImg(p1.SourceImmagine);
                    }
                }
            }
        }

        private void AggiungiVittoria(Personaggio vincitore)
        {
            foreach(Personaggio p in _asset.Personaggi)
            {
                if (vincitore.Equals(p))
                {
                    p.NumeroVittorie++;
                }
            }
        }

        private async void MostraDannoCritico()
        {
            await Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnAttacca.IsEnabled = false;
                    grdDannoCritico.Visibility = Visibility.Visible;
                }));

                Thread.Sleep(1000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnAttacca.IsEnabled = true;
                    grdDannoCritico.Visibility = Visibility.Hidden;
                }));
            });
        }

        private async void MostraPopUp()
        {
            await Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    grdPopUp.Visibility = Visibility.Visible;
                    grdGioco.Visibility = Visibility.Hidden;
                }));
            });
        }

        private void btnNewRound_Click(object sender, RoutedEventArgs e)
        {
            grdVincitoreRound.Visibility = Visibility.Hidden;
            ///inizio un nuovo round
            //ristoro i giocatori
            battaglia.Personaggio1.Ristora(true);
            battaglia.Personaggio2.Ristora(true);
            //faccio il cambio di armi in base alle impostazioni della partita
            if (cambio == CambioArma.sempre)
            {
                grdScegliArma.Visibility = Visibility.Visible;
            }
            else
            {
                if (cambio == CambioArma.vincitore)
                {
                    grdScegliArma.Visibility = Visibility.Visible;
                    cambiaVincitore = true;

                    if (battaglia.Vincitore.Equals(battaglia.Personaggio1))
                    {
                        indexArmaDaTenere = lstArma2.SelectedIndex;
                        vincitore = "Personaggio 1";
                    }
                    else
                    {
                        indexArmaDaTenere = lstArma1.SelectedIndex;
                        vincitore = "Personaggio 2";
                    }
                }
                else
                {
                    if (cambio == CambioArma.perdente)
                    {
                        grdScegliArma.Visibility = Visibility.Visible;
                        cambiaVincitore = false;

                        if (battaglia.Perdente.Equals(battaglia.Personaggio1))
                        {
                            indexArmaDaTenere = lstArma1.SelectedIndex;
                            vincitore = "Personaggio 1";
                        }
                        else
                        {
                            indexArmaDaTenere = lstArma2.SelectedIndex;
                            vincitore = "Personaggio 2";
                        }
                    }
                    else
                    {
                        grdGioco.Visibility = Visibility.Visible;
                    }
                }
            }

            //aggiungo ai punti ferita massimi quelli dell'arma
            battaglia.Personaggio1.PuntiFerita += battaglia.ArmaGiocatore1.VitaAggiunta;
            battaglia.Personaggio2.PuntiFerita += battaglia.ArmaGiocatore2.VitaAggiunta;

            AggiornaInterfacciaCombattimento();
        }

        private void btnFinePartita_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private async void Sorteggio()
        {
            r = new Random();
            int sorteggiato11, sorteggiato12, sorteggiato13, sorteggiato21, sorteggiato22, sorteggiato23;

            await Task.Run(() =>
            {
                for (; ; )
                {
                    sorteggiato11 = r.Next(1, 7);
                    sorteggiato12 = r.Next(1, 7);
                    sorteggiato13 = r.Next(1, 7);
                    sorteggiato21 = r.Next(1, 7);
                    sorteggiato22 = r.Next(1, 7);
                    sorteggiato23 = r.Next(1, 7);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        imgDado11.Source = CambiaImg("img/" + sorteggiato11 + ".png");
                        imgDado12.Source = CambiaImg("img/" + sorteggiato12 + ".png");
                        imgDado13.Source = CambiaImg("img/" + sorteggiato13 + ".png");
                        imgDado21.Source = CambiaImg("img/" + sorteggiato21 + ".png");
                        imgDado22.Source = CambiaImg("img/" + sorteggiato22 + ".png");
                        imgDado23.Source = CambiaImg("img/" + sorteggiato23 + ".png");
                    }));
                    Thread.Sleep(200);
                }
            });
        }
    }
}
