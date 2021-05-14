using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{    
    public class Battaglia
    {
        public Personaggio Personaggio2{ get; set; }

        public Personaggio Personaggio1 { get; set; }

        public Arma ArmaGiocatore2 { get; set; }

        public Arma ArmaGiocatore1 { get; set; }
        public Sfondo Sfondo { get; set; }
        public string Vincitore { get; set; }
        public string Perdente { get; set; }

        private int _numeriRound;
        public int NumeroRound
        {
            get { return _numeriRound; }
            set
            {
                if (value <= 0)
                    throw new Exception("numero di round non valido");
                _numeriRound = value;
            }
        }

        public CambioArma CambioArma { get; set; }

        private int _roundCorrente;
        public int RoundCorrente
        {
            get
            {
                return _roundCorrente;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("round corrente non valido");
                _roundCorrente = value;
            }
        }

        public List<Personaggio> Cronologia { get; set; }

        private int _punteggioG1;
        public int PunteggioG1
        {
            get
            {
                return _punteggioG1;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("punteggio del giocatore 1 non valido");
                _punteggioG1 = value;
            }
        }

        private int _punteggioG2;
        public int punteggioG2
        {
            get
            {
                return _punteggioG2;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("punteggio del giocatore 2 non valido");
                _punteggioG2 = value;
            }
        }

        public Battaglia(Personaggio p1, Personaggio p2, Arma a1, Arma a2, CambioArma cambio, int nRound, Sfondo s)
        {
            try
            {
                Sfondo = s;
                Personaggio1 = p1; Personaggio2 = p2;
                ArmaGiocatore1 = a1; ArmaGiocatore2 = a2;
                CambioArma = cambio;
                NumeroRound = nRound;
                RoundCorrente = 0;
                Cronologia = new List<Personaggio>();
                PunteggioG1 = 0; punteggioG2 = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// attacco
        /// </summary>
        /// <returns>il personaggio se c'è un vincitore, null se non c'è nessun vincitore</returns>
        public Personaggio Attacco(out int dado1, out int dado2, out bool dannoCritico, out int percentualeDannoCritico)
        {
            //passo in out i valori dei dadi ed un eventuale danno critico usando anche i bool
            //passo ad attacca dei personaggi il valore del dado, e lui mi dici (con gli out) se c'è danno critico, e se c'è quanto vale
            //io limito il danno con il limite dell'arma, poi aggiungo il danno critico
            dannoCritico = false;
            percentualeDannoCritico = 0;

            Random r1 = new Random();
            dado1 = 0; dado2 = 0;
            for(int i = 0; i < 6; i++)
            {
                int val= r1.Next(1, 7);
                if (i < 3)
                    dado1 += val;
                else
                    dado2 += val;
            }

            if(dado1 > dado2)
            {
                //il personaggio 1 attacca
                int dado = dado1 - dado2;
                int danno = Personaggio1.Attacca(dado, out dannoCritico, out percentualeDannoCritico);
                danno = danno > ArmaGiocatore1.DannoMassimo ? ArmaGiocatore1.DannoMassimo : danno;
                if(ArmaGiocatore1.Classe == 'S')
                {
                    //conta danno extra aggiuntivo
                    int dannoExtra = danno / 100 * ArmaGiocatore1.PercentualeDannoExtra;
                    //il danno massimo è 20
                    danno = danno + dannoExtra > 20 ? 20 : danno + dannoExtra;
                }
                if (dannoCritico)
                {
                    //calcolo il danno extra in base alla percentuale del dado aggiuntivo
                    int dannoExtra = danno / 100 * percentualeDannoCritico;
                    danno = danno + dannoExtra;
                }
                if (Personaggio2.SubisciDanno(danno))
                {
                    return null; //lo scontro va avanti
                }
                else
                {
                    Vincitore = "giocatore 1";
                    Perdente = "giocatore 2";
                    Cronologia.Add(Personaggio1); //aggiorno la cronologia dei vincitori
                    return Personaggio1; //ritorno il vincitore
                }
            }
            else if(dado2 > dado1)
            {
                //il personaggio 2 attacca
                int dado = dado2 - dado2;
                int danno = Personaggio2.Attacca(dado, out dannoCritico, out percentualeDannoCritico);
                danno = danno > ArmaGiocatore2.DannoMassimo ? ArmaGiocatore2.DannoMassimo : danno;
                if (ArmaGiocatore2.Classe == 'S')
                {
                    //conta danno extra aggiuntivo
                    int dannoExtra = danno / 100 * ArmaGiocatore2.PercentualeDannoExtra;
                    //il danno massimo è 20
                    danno = danno + dannoExtra > 20 ? 20 : danno + dannoExtra;
                }
                if (dannoCritico)
                {
                    //calcolo il danno extra in base alla percentuale del dado aggiuntivo
                    int dannoExtra = danno / 100 * percentualeDannoCritico;
                    danno = danno + dannoExtra;
                }
                if (Personaggio1.SubisciDanno(danno))
                {
                    return null; //lo scontro va avanti
                }
                else
                {
                    Vincitore = "giocatore 2";
                    Perdente = "giocatore 1";
                    Cronologia.Add(Personaggio2); //aggiorno la cronologia dei vincitori
                    return Personaggio2; //ritorno il vincitore
                }
            }
            else
            {
                //è un pareggio, ritorno null
                return null; //lo scontro va avanti come se non fosse successo niente
            }

        }

        public void NewRound()
        {
            //ristoro i giocatori
            Personaggio1.ristora(true);
            Personaggio2.ristora(true);
            RoundCorrente++;
        }

        
    }
}