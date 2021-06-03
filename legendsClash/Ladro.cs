using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace legendsClash
{
    [XmlRoot(ElementName = "Ladro")]
    public class Ladro : Personaggio
    {
        int _percentualeDannoCritico;
        int _aumentoDannoCritico;

        public Ladro(string nome, int puntiFerita, string sourceImage, int percentualeDannoCritico, int aumentoDannoCritico, int numeroVittorie = 0) : base(nome, sourceImage, numeroVittorie)
        {
            PercentualeDannoCritico = percentualeDannoCritico;
            AumentoDannoCritico = aumentoDannoCritico;
            PuntiFerita = puntiFerita;
            _puntiFeritaMassimi = puntiFerita;
        }

        public Ladro()
        {

        }

        [XmlElement(ElementName = "PercentualeDannoCritico")]
        public int PercentualeDannoCritico
        {
            get
            {
                return _percentualeDannoCritico;
            }
            set
            {
                if (value >= 15 || value <= 25)
                    _percentualeDannoCritico = value;
                else
                    throw new Exception("percentuale danno critico non valido");
            }
        }

        [XmlAttribute(attributeName: "Vita")]
        public new int PuntiFerita
        {
            get
            {
                return _puntiFerita;
            }
            set
            {
                if (value >= 35 || value <= 45)
                {
                    _puntiFerita = value;
                }
                else
                    throw new Exception("punti ferita non validi");
            }
        }

        [XmlElement(ElementName = "AumentoDannoCritico")]
        public int AumentoDannoCritico
        {
            get
            {
                return _aumentoDannoCritico;
            }
            set
            {
                if (value >= 45 || value <= 60)
                    _aumentoDannoCritico = value;
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

        public override string ToString()
        {
            return "Ladro " + Nome + " con " + NumeroVittorie + " vitorie";
        }
    }
}