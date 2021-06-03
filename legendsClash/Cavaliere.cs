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
        int _percentualeDannoAggiuntivo;

        public Cavaliere(string nome, int puntiFerita, string sourceImage, int percentualeDannoAumentato, int numeroVittorie = 0) : base(nome, sourceImage, numeroVittorie)
        {
            PuntiFerita = puntiFerita;
            _puntiFeritaMassimi = puntiFerita;
            PercentualeDannoAggiuntivo = percentualeDannoAumentato;
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
        public new int PuntiFerita
        {
            get
            {
                return _puntiFerita;
            }
            set
            {
                if (value >= 45 || value <= 55)
                {
                    _puntiFerita = value;
                }
                else
                    throw new Exception("punti ferita non validi");
            }
        }

        public int PercentualeDannoAggiuntivo
        {
            get
            {
                return _percentualeDannoAggiuntivo;
            }
            set
            {
                if (value >= 10 || value <= 20)
                    _percentualeDannoAggiuntivo = value;
                else
                    throw new Exception("percentuale danno aggiuntivo non valido");
            }
        }

        public override string ToString()
        {
            return "Cavaliere " + Nome + " con " + NumeroVittorie + " vitorie";
        }
    }
}