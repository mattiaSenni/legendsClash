using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace legendsClash
{


    public class Ladro : Personaggio
    {
        int percentualeDannoCritico;
        int aumentoDannoCritico;
        int puntiFerita;
        public Ladro(string _nome, int _puntiFerita, string sourceImage, int _percentualeDannoCritico, int _aumentoDannoCritico, int _numeroVittorie = 0) : base(_nome, sourceImage, _numeroVittorie)
        {
            PercentualeDannoCritico = _percentualeDannoCritico;
            AumentoDannoCritico = _aumentoDannoCritico;
            PuntiFerita = _puntiFerita;
        }

        public Ladro()
        {

        }

        public int PercentualeDannoCritico
        {
            get
            {
                return percentualeDannoCritico;
            }
            set
            {
                if (value >= 15 || value <= 25)
                    percentualeDannoCritico = value;
                else
                    throw new Exception("percentuale danno critico non valido");
            }
        }
        [XmlAttribute(attributeName: "Vita")]
        public int PuntiFerita
        {
            get
            {
                return puntiFerita;
            }
            set
            {
                if (value >= 35 || value <= 45)
                {
                    PuntiFeritaMassimiCheIlPersonaggioHa = value;
                    PuntiFeritaDelPersonaggio = value;
                }
                else
                    throw new Exception("punti ferita non validi");
            }
        }
        public int AumentoDannoCritico
        {
            get
            {
                return aumentoDannoCritico;
            }
            set
            {
                if (value >= 45 || value <= 60)
                    aumentoDannoCritico = value;
                else
                    throw new Exception("aumento danno critico non valido");
            }
        }

        public override int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)
        {
            Random r = new Random();
            int appoggio = r.Next(1, 101);
            if (appoggio <= PercentualeDannoCritico)
            {
                dannoCritico = true;
                valoreDannoCritico = PercentualeDannoCritico;
            }
            else
            {
                dannoCritico = false;
                valoreDannoCritico = 0;
            }
            return dado;
        }
    }
}