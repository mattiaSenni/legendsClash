using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace legendsClash
{
    public class Cavaliere : Personaggio
    {
        int puntiFerita;
        int percentualeDannoAggiuntivo;
        public Cavaliere(string _nome, int _puntiFerita, string sourceImage, int _percentualeDannoAumentato, int _numeroVittorie = 0) : base(_nome, sourceImage, _numeroVittorie)
        {
            PuntiFerita = _puntiFerita;
            PercentualeDannoAggiuntivo = _percentualeDannoAumentato;
        }

        public Cavaliere()
        {

        }

        public override int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)
        {
            Random r = new Random();
            int appoggio = r.Next(1, 101);

            if (appoggio <= 10)
            {
                dannoCritico = true;
                valoreDannoCritico = 30;

            }
            else
            {
                dannoCritico = false;
                valoreDannoCritico = 0;
            }
            return dado + (dado * PercentualeDannoAggiuntivo / 100);
        }
        [XmlAttribute(attributeName: "Vita")]
        public override int PuntiFerita
        {
            get
            {
                return puntiFerita;
            }
            set
            {
                if (value >= 45 || value <= 55)
                {
                    _puntiFeritaMassimiCheIlPersonaggioHa = value;
                    _puntiFeritaDelPersonaggio = value;
                }
                else
                    throw new Exception("punti ferita non validi");
            }
        }

        public int PercentualeDannoAggiuntivo
        {
            get
            {
                return percentualeDannoAggiuntivo;
            }
            set
            {
                if (value >= 10 || value <= 20)
                    percentualeDannoAggiuntivo = value;
                else
                    throw new Exception("percentuale danno aggiuntivo non valido");

            }
        }
    }
}