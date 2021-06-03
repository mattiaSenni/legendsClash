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
        private string _nome;
        protected int _puntiFeritaMassimi;
        protected int _puntiFerita;
        private int _numeroVittorie;
        private string _sourceImmagine;

        public Personaggio()
        {
            
        }

        public Personaggio(string nome, string sourceImage, int numeroVittorie = 0)
        {
            Nome = nome;
            NumeroVittorie = numeroVittorie;
            SourceImmagine = sourceImage;
        }

        [XmlAttribute(attributeName: "Nome")]
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _nome = value;
                }
            }
        }

        [XmlAttribute(attributeName: "Vita")]
        public int PuntiFerita
        {
            get => default;
            set
            {
                if (value >= 35 || value <= 85)
                {
                    _puntiFerita = value;
                    _puntiFeritaMassimi = value;
                }
            }
        }

        [XmlAttribute(attributeName: "Vittorie")]
        public int NumeroVittorie
        {
            get
            {
                return _numeroVittorie;
            }
            set
            {
                if (value >= 0)
                {
                    _numeroVittorie = value;
                }
                else
                    throw new Exception("vittorie non valide");
            }
        }

        public string SourceImmagine
        {
            get
            {
                return _sourceImmagine;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _sourceImmagine = value;
                else
                    throw new Exception("Source immagine non valido");
            }
        }

        public void Ristora(bool ristoraTuttaLaVita, int percentualeDiVitaDaRecuperare = 50)
        {
            if (ristoraTuttaLaVita == true)
            {
                PuntiFerita = _puntiFeritaMassimi;
            }
            else
            {
                PuntiFerita += _puntiFeritaMassimi * percentualeDiVitaDaRecuperare / 100;
            }
        }

        public virtual int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)
        {
            //return dado+%danno critico+%danno cavaliere     inizializzo danno critico e valore danno critico in base a che personaggio è
            throw new System.NotImplementedException();
        }

        public bool SubisciDanno(int dannoSubito)
        {
            PuntiFerita -= dannoSubito;
            if (PuntiFerita <= 0)
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