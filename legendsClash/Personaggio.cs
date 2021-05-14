using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace legendsClash
{
    public abstract class Personaggio : IEquatable<Personaggio>
    {
        string Nome;
        protected int PuntiFeritaMassimiCheIlPersonaggioHa;
        protected int PuntiFeritaDelPersonaggio;
        int NumeroVittorie;
        string SourceImmaginePersonaggio;

        public Personaggio(string _nome, string sourceImage, int _numeroVittorie = 0)
        {
            nome = _nome;
            numeroVittoria = _numeroVittorie;
            sourceImmagine = sourceImage;
        }

        public Personaggio()
        {

        }
        [XmlAttribute(attributeName: "Nome")]
        public string nome
        {
            get => default;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Nome = value;
                }
            }
        }

        /*
        public virtual int puntiFerita
        {
            get => default;
            set
            {
                if (value >= 35 || value <= 85)
                {
                    PuntiFeritaMassimiCheIlPersonaggioHa = value;
                    PuntiFeritaDelPersonaggio = value;
                }
            }
        }
        */
        [XmlAttribute(attributeName: "Vittorie")]
        public int numeroVittoria
        {
            get => default;
            set
            {
                if (value >= 0)
                {
                    NumeroVittorie = value;
                }
                else
                    throw new Exception("vittorie non valide");
            }
        }

        public string sourceImmagine
        {
            get => default;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    SourceImmaginePersonaggio = value;
                else
                    throw new Exception("source immagine non valido");
            }
        }

        public void aggiungiVittoria()
        {
            numeroVittoria++;
        }

        public void ristora(bool ristoraTuttaLaVita, int percentualeDiVitaDaRecuperare = 50)
        {
            if (ristoraTuttaLaVita)
            {
                PuntiFeritaDelPersonaggio = PuntiFeritaMassimiCheIlPersonaggioHa;
            }
            else
            {
                PuntiFeritaDelPersonaggio += PuntiFeritaMassimiCheIlPersonaggioHa * percentualeDiVitaDaRecuperare / 100;
            }
        }

        public virtual int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)
        {
            //return dado+%danno critico+%danno cavaliere     inizializzo danno critico e valore danno critico in base a che personaggio è
            throw new System.NotImplementedException();
        }

        public bool SubisciDanno(int dannoSubito)
        {
            PuntiFeritaDelPersonaggio -= dannoSubito;
            if (PuntiFeritaDelPersonaggio <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Equals(Personaggio other)
        {
            if (this.Nome == other.Nome)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return nome + " " + numeroVittoria;
        }
    }
}