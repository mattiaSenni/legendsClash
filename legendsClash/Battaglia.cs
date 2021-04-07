using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{    
    public class Battaglia
    {
        public Personaggio Personaggio2
        {
            get => default;
            set
            {
            }
        }

        public Personaggio Personaggio1
        {
            get => default;
            set
            {
            }
        }

        public Arma ArmaGiocatore2
        {
            get => default;
            set
            {
            }
        }

        public Arma ArmaGiocatore1
        {
            get => default;
            set
            {
            }
        }

        public int numeroTurni
        {
            get => default;
            set
            {
            }
        }

        internal CambioArma CambioArma
        {
            get => default;
            set
            {
            }
        }

        public int roundCorrente
        {
            get => default;
            set
            {
            }
        }

        public List<Personaggio> cronologia
        {
            get => default;
            set
            {
            }
        }

        public int punteggioG1
        {
            get => default;
            set
            {
            }
        }

        public int punteggioG2
        {
            get => default;
            set
            {
            }
        }

        public void attacco()
        {
            //passo in out i valori dei dadi ed un eventuale danno critico usando anche i bool
            throw new System.NotImplementedException();
        }
    }
}