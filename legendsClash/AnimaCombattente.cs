using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace legendsClash
{
    public class AnimaCombattente
    {
        public Thickness Thickness { get; set; }
        public AnimaCombattente(Thickness thickness)
        {
            Thickness = thickness;
        }

        public Thickness Mossa()
        {
            throw new NotImplementedException();
        }
    }
}