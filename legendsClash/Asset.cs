using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public class Asset
    {

        public Asset(List<Arma> a, List<Personaggio> p, Scenari s)
        {
            Armi = a;
            Personaggi = p;
            Scenari = s;
        }

        public List<Personaggio> Personaggi { get; set; }

        public List<Arma> Armi { get; set; }

        public Scenari Scenari { get; set; }

        public Battaglia CreaPartitta(Personaggio p1, Personaggio p2, Arma a1, Arma a2, CambioArma cambio, int nRound)
        {
            return new Battaglia(p1, p2, a1, a2, cambio, nRound);
        }

        public void CreaPersonaggio()
        {
            Personaggio personaggio = new Personaggio();
            Personaggi.Add(personaggio);
        }

        public void CreaArma()
        {
            Arma arma = new Arma();
            Armi.Add(arma);
        }
    }
}