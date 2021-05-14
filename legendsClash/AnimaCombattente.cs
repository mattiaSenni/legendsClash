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
        public double TopOriginale { get; set; }
        public double GrandezzaMossa { get; set; }
        private bool _sale { get; set; }
        
        public AnimaCombattente(Thickness thickness, double mossa = 2)
        {
            Thickness = thickness;
            GrandezzaMossa = mossa;
            _sale = true;
        }

        

        public Thickness Mossa()
        {
            if(_sale)
            {
                if(Thickness.Top + GrandezzaMossa < TopOriginale + 10)
                {
                    //posso fare un'altra mossa in su
                    ModificaThikness(Thickness.Top + GrandezzaMossa);
                }
                else
                {
                    _sale = false;
                }
            }
            else
            {
                if(Thickness.Top - GrandezzaMossa > TopOriginale - 10)
                {
                    //posso fare un'altra mossa in giù
                    ModificaThikness(Thickness.Top - GrandezzaMossa);
                }
                else
                {
                    _sale = true;
                }
            }
            return Thickness;
        }

        private void ModificaThikness(double top)
        {
            Thickness t = new Thickness(Thickness.Left, top, Thickness.Right, Thickness.Bottom);
            Thickness = t;
        }
    }
}