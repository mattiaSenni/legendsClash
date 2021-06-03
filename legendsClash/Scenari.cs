using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace legendsClash
{
    [XmlRoot(ElementName = "Scenari")]
    public class Scenari
    {   
        public Scenari()
        {
            Sfondi = new List<Sfondo>();
        }

        public Scenari(List<Sfondo> s)
        {
            Sfondi = s;
        }

        [XmlElement(ElementName ="Sfondo")]
        public List<Sfondo> Sfondi { get; set; }
    }
}