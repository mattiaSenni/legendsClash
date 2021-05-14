using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace legendsClash
{
    /// <summary>
    /// Logica di interazione per AggiungiPersonaggio.xaml
    /// </summary>
    public partial class AggiungiPersonaggio : Window
    {
        public AggiungiPersonaggio()
        {

        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            //to do: torna alla home
        }

        private void cmb_Classi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Classi.SelectedIndex == 0)
            {
                lbl_puntiVitaMinimi.Content = "Minimo: 70";
                lbl_puntiVitaMassimi.Content = "Massimo: 85";
                GestisciGraficaGigante();
            }
            else if (cmb_Classi.SelectedIndex == 1)
            {
                lbl_puntiVitaMinimi.Content = "Minimo: 35";
                lbl_puntiVitaMassimi.Content = "Massimo: 45";
                GestisciGraficaLadro();
            }
            else
            {
                lbl_puntiVitaMinimi.Content = "Minimo: 45";
                lbl_puntiVitaMassimi.Content = "Massimo: 55";
                GestisciGraficaCavaliere();
            }
        }

        private void btn_EstraiInManieraCasuale_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int classe = r.Next(1, 4);
            switch (classe)
            {
                case 1:
                    cmb_Classi.SelectedIndex = 0;
                    txt_vita.Text = r.Next(70, 86).ToString();
                    GestisciGraficaGigante();
                    break;
                case 2:
                    cmb_Classi.SelectedIndex = 1;
                    txt_vita.Text = r.Next(35, 46).ToString();
                    GestisciGraficaLadro();
                    txt_percentualeDannoCriticoLadro.Text = r.Next(15, 26).ToString();
                    txt_aumentoDannoCritico.Text = r.Next(45, 61).ToString();
                    break;
                case 3:
                    cmb_Classi.SelectedIndex = 2;
                    txt_vita.Text = r.Next(45, 56).ToString();
                    GestisciGraficaCavaliere();
                    txt_percentualeDannoAumentatoCavaliere.Text = r.Next(10, 21).ToString();
                    break;
            }
            int appoggio = r.Next(0, 4);
            string nome = "0";

            switch (appoggio)
            {
                case 0:
                    nome = "albero bello";
                    break;
                case 1:
                    nome = "xx_Pr0Gam3r_xx";
                    break;
                case 2:
                    nome = "radicchio-selvatico";
                    break;
                case 3:
                    nome = "nightmarebringer";
                    break;

            }

            nome += DateTime.Now.Day.ToString() + DateTime.Now.Second.ToString();

            txt_nome.Text = nome;

        }

        public void GestisciGraficaLadro()
        {
            //parti che servono per la creazione del ladro
            img_personaggio.Source = new BitmapImage(new Uri(@"\img\assassino.png", UriKind.Relative));
            lbl_percentualeDannoCritico.Visibility = Visibility.Visible;
            lbl_TitoloStatLadro.Visibility = Visibility.Visible;
            lbl_aumentoDannoCritico.Visibility = Visibility.Visible;
            txt_aumentoDannoCritico.Visibility = Visibility.Visible;
            txt_percentualeDannoCriticoLadro.Visibility = Visibility.Visible;
            btn_EstraiInManieraCasuale.Margin = new Thickness(258, 523, 0, 0);
            btn_crea.Margin = new Thickness(258, 584, 0, 0);
            //parti da non visualizzare
            lbl_dannoCavaliere.Visibility = Visibility.Hidden;
            txt_percentualeDannoAumentatoCavaliere.Visibility = Visibility.Hidden;
            lbl_TitoloStatCavaliere.Visibility = Visibility.Hidden;
        }

        public void GestisciGraficaCavaliere()
        {
            //parti che servono al cavaliere
            img_personaggio.Source = new BitmapImage(new Uri(@"\img\cavaliere.png", UriKind.Relative));
            lbl_TitoloStatCavaliere.Visibility = Visibility.Visible;
            lbl_dannoCavaliere.Visibility = Visibility.Visible;
            txt_percentualeDannoAumentatoCavaliere.Visibility = Visibility.Visible;
            btn_EstraiInManieraCasuale.Margin = new Thickness(258, 445, 0, 0);
            btn_crea.Margin = new Thickness(258, 510, 0, 0);
            //parti da non visualizzare
            lbl_TitoloStatLadro.Visibility = Visibility.Hidden;
            lbl_aumentoDannoCritico.Visibility = Visibility.Hidden;
            lbl_percentualeDannoCritico.Visibility = Visibility.Hidden;
            txt_aumentoDannoCritico.Visibility = Visibility.Hidden;
            txt_percentualeDannoCriticoLadro.Visibility = Visibility.Hidden;
        }

        public void GestisciGraficaGigante()
        {
            img_personaggio.Source = new BitmapImage(new Uri(@"\img\gigante.png", UriKind.Relative));
            btn_EstraiInManieraCasuale.Margin = new Thickness(258, 320, 0, 0);
            btn_crea.Margin = new Thickness(258, 389, 0, 0);
            txt_percentualeDannoCriticoLadro.Visibility = Visibility.Hidden;
            lbl_aumentoDannoCritico.Visibility = Visibility.Hidden;
            txt_aumentoDannoCritico.Visibility = Visibility.Hidden;
            lbl_percentualeDannoCritico.Visibility = Visibility.Hidden;
            lbl_TitoloStatCavaliere.Visibility = Visibility.Hidden;
            lbl_TitoloStatLadro.Visibility = Visibility.Hidden;
            lbl_dannoCavaliere.Visibility = Visibility.Hidden;
            txt_percentualeDannoAumentatoCavaliere.Visibility = Visibility.Hidden;

        }


        private void btn_crea_Click(object sender, RoutedEventArgs e)
        {
            //da aggiustare il source delle immagini
            try
            {
                if (cmb_Classi.SelectedIndex == 0)
                {
                    Gigante nuovo = new Gigante(txt_nome.Text, Convert.ToInt32(txt_vita.Text), @"\img\gigante.png");
                }
                else if (cmb_Classi.SelectedIndex == 1)
                {
                    Ladro nuovo = new Ladro(txt_nome.Text, Convert.ToInt32(txt_vita.Text), @"\img\assassino.png", Convert.ToInt32(txt_percentualeDannoCriticoLadro.Text), Convert.ToInt32(txt_aumentoDannoCritico.Text));
                }
                else
                {
                    Cavaliere nuovo = new Cavaliere(txt_nome.Text, Convert.ToInt32(txt_vita.Text), @"\img\cavaliere.jpg", Convert.ToInt32(txt_percentualeDannoAumentatoCavaliere.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
