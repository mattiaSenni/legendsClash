using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public static class LimitiPersonaggi
    {
        //classe LADRO
        public const int LADRO_VITA_MAX = 45;
        public const int LADRO_VITA_MIN = 35;
        public const int LADRO_POSSIBILITA_DANNO_CRITICO_MIN = 15;
        public const int LADRO_POSSIBILITA_DANNO_CRITICO_MAX = 25;
        public const int LADRO_DANNO_CRITICO_MIN = 45;
        public const int LADRO_DANNO_CRITICO_MAX = 60;

        //classe CAVALIERE
        public const int CAVALIERE_VITA_MAX = 45;
        public const int CAVALIERE_VITA_MIN = 55;
        public const int CAVALIERE_DANNO_MIN = 10;
        public const int CAVALIERE_DANNO_MAX = 20;

        //classe GIGANTE
        public const int GIGANTE_VITA_MAX = 70;
        public const int GIGANTE_VITA_MIN = 85;
    }
}