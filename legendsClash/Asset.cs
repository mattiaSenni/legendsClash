using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace legendsClash
{
    [XmlRoot(ElementName = "Asset")]
    public class Asset
    {
        public Asset(List<Arma> a, List<Personaggio> p, Scenari s)
        {
            Armi = a;
            Personaggi = p;
            Scenari = s;
        }

        public Asset() 
        {

        }

        [XmlElement(ElementName = "Personaggi")]
        public List<Personaggio> Personaggi 
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Armi")]
        public List<Arma> Armi
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Scenari")]
        public Scenari Scenari 
        {
            get;
            set;
        }

        public Battaglia CreaPartitta(Personaggio p1, Personaggio p2, Arma a1, Arma a2, CambioArma cambio, int nRound, Sfondo s)
        {
            return new Battaglia(p1, p2, a1, a2, cambio, nRound, s);
        }

        public void CreaPersonaggio(Personaggio p)
        {
            Personaggi.Add(p);
        }

        public void CreaArma(Arma a)
        {
            Armi.Add(a);
        }
    }
}