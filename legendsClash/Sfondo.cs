using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace legendsClash
{
    [XmlRoot(ElementName = "sfondo")]
    public class Sfondo
    {
        private string _source;
        private int _id;

        public Sfondo() { }
        public Sfondo(string s, int i)
        {
            _source = s;
            _id = i;
        }

        [XmlElement(ElementName = "Source")]
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("errore");
                }
                else
                {
                    _source = value;
                }
            }
        }

        [XmlElement(ElementName = "id")]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {

                if (value >= 0)
                {
                    _id = value;
                }
                else
                {
                    throw new Exception("errore");
                }

            }
        }

        public override string ToString()
        {
            return Source;
        }
    }
}