using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public class Scenari
    {
        public List<Sfondo> Sfondi { get; set; }
        public Scenari()
        {
            Sfondi = new List<Sfondo>();
        }
        public Scenari(List<Sfondo> s)
        {
            Sfondi = s;
        }
    }
}