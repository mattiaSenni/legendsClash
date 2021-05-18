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
        protected int _puntiFeritaMassimiCheIlPersonaggioHa;
        protected int _puntiFeritaDelPersonaggio;
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
            get
            {
                return Nome;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Nome = value;
                }
            }
        }


        public virtual int PuntiFerita
        {
            get => default;
            set
            {
                if (value >= 35 || value <= 85)
                {
                    _puntiFeritaMassimiCheIlPersonaggioHa = value;
                    _puntiFeritaDelPersonaggio = value;
                }
            }
        }

        [XmlAttribute(attributeName: "Vittorie")]
        public int numeroVittoria
        {
            get
            {
                return NumeroVittorie;
            }
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
            get
            {
                return SourceImmaginePersonaggio;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
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
                _puntiFeritaDelPersonaggio = _puntiFeritaMassimiCheIlPersonaggioHa;
            }
            else
            {
                _puntiFeritaDelPersonaggio += _puntiFeritaMassimiCheIlPersonaggioHa * percentualeDiVitaDaRecuperare / 100;
            }
        }

        public virtual int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)
        {
            //return dado+%danno critico+%danno cavaliere     inizializzo danno critico e valore danno critico in base a che personaggio è
            throw new System.NotImplementedException();
        }

        public bool SubisciDanno(int dannoSubito)
        {
            _puntiFeritaDelPersonaggio -= dannoSubito;
            if (_puntiFeritaDelPersonaggio <= 0)
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
    }
}