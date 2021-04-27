using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace legendsClash
{
    [XmlRoot(ElementName = "arma")]
    public class Arma : IEquatable<Arma>
    {
        private const int DMAX_AS = 15;
        private const int DMAX_B = 12;
        private const int DMAX_C = 9;
        private const int DMAX_D = 6;

        private const int VAGGMAX_S = 20;
        private const int VAGGMAX_A = 15;
        private const int VAGGMAX_B = 10;
        private const int VAGGMAX_C = 5;

        private const int PERC_EXTRA_MIN = 5;
        private const int PERC_EXTRA_MAX = 15;

        private char _classe;
        private int _dannoMassimo;
        private int _vitaAggiunta;
        private int _percentualeDannoExtra;
        private string _nome;
        private string _sourceImmagine;

        public Arma()
        {

        }

        public Arma(char classe, string nome, string source)
        {
            //nuova arma
            Classe = classe;
            Nome = nome;
            SourceImmagine = source;

            //=-1 solo per chiamare la proprietà dato che non gli passo valori
            DannoMassimo = -1;
            VitaAggiunta = -1;
            PercentualeDannoExtra = -1;
        }

        public Arma(char classe, string nome, string source, int vitaAgg, int percDanno)
        {
            //arma creata in precedenza
            Classe = classe;
            Nome = nome;
            SourceImmagine = source;
            VitaAggiunta = vitaAgg;
            PercentualeDannoExtra = percDanno;

            //=-1 solo per chiamare la proprietà dato che non gli passo valori
            DannoMassimo = -1;
        }

        [XmlAttribute(AttributeName = "classe")]
        public char Classe
        {
            get => _classe;
            private set
            {
                if (value != 'D' && value != 'C' && value != 'B' && value != 'A' && value != 'S')
                {
                    throw new Exception("Classe non esistente.");
                }
                _classe = value;
            }
        }

        [XmlElement(ElementName = "dannoMassimo")]
        public int DannoMassimo
        {
            get => _dannoMassimo;
            private set
            {
                if (Classe == 'A' || Classe == 'S')
                {
                    _dannoMassimo = DMAX_AS;
                }
                else
                {
                    if (Classe == 'B')
                    {
                        _dannoMassimo = DMAX_B;
                    }
                    else
                    {
                        if (Classe == 'C')
                        {
                            _dannoMassimo = DMAX_C;
                        }
                        else
                        {
                            _dannoMassimo = DMAX_D;
                        }
                    }
                }
            }
        }

        [XmlElement(ElementName = "vitaAggiunta")]
        public int VitaAggiunta
        {
            get => _vitaAggiunta;
            private set
            {
                if (value < 0)
                {
                    if (Classe == 'S')
                    {
                        Random r = new Random();
                        _vitaAggiunta = r.Next(0, VAGGMAX_S + 1); ;
                    }
                    else
                    {
                        if (Classe == 'A')
                        {
                            Random r = new Random();
                            _vitaAggiunta = r.Next(0, VAGGMAX_A + 1); ;
                        }
                        else
                        {
                            if (Classe == 'B')
                            {
                                Random r = new Random();
                                _vitaAggiunta = r.Next(0, VAGGMAX_B + 1);
                            }
                            else
                            {
                                Random r = new Random();
                                _vitaAggiunta = r.Next(0, VAGGMAX_C + 1);
                            }
                        }
                    }
                }
                else
                {
                    if ((Classe == 'S' && value > VAGGMAX_S) || (Classe == 'A' && value > VAGGMAX_A) || (Classe == 'B' && value > VAGGMAX_B) || (Classe == 'C' && value > VAGGMAX_C))
                    {
                        throw new Exception("Vita aggiunta non accettabile");
                    }

                    _vitaAggiunta = value;
                }
            }
        }

        [XmlElement(ElementName = "percDannoExtra")]
        public int PercentualeDannoExtra
        {
            get => _percentualeDannoExtra;
            private set
            {
                if (value < 0)
                {
                    if (Classe == 'S')
                    {
                        Random r = new Random();
                        _percentualeDannoExtra = r.Next(PERC_EXTRA_MIN, PERC_EXTRA_MAX + 1);
                    }
                    else
                    {
                        _percentualeDannoExtra = 0;
                    }
                }
                else
                {
                    if (value < 5 && value > 15)
                    {
                        throw new Exception("Danno extra non accettabile");
                    }
                    _percentualeDannoExtra = value;
                }

            }
        }

        [XmlAttribute(AttributeName = "nome")]
        public string Nome
        {
            get => _nome;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Nome non accettabile");
                }
                _nome = value;
            }
        }

        [XmlElement(ElementName = "sourceImmagine")]
        public string SourceImmagine
        {
            get => _sourceImmagine;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Source non accettabile");
                }
                _sourceImmagine = value;
            }
        }

        public bool Equals(Arma other)
        {
            if (this.Nome == other.Nome)
            {
                return true;
            }

            return false;
        }
    }
}