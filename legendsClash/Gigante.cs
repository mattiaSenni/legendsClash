﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public class Gigante : Personaggio
    {


        public Gigante(string _nome, int _puntiFerita, string sourceImage, int _numeroVittorie = 0) : base(_nome, sourceImage, _numeroVittorie)
        {
            puntiFerita = _puntiFerita;
        }

        public int puntiFerita
        {
            get => default;
            set
            {
                if (value >= 70 || value <= 85)
                {
                    PuntiFeritaMassimiCheIlPersonaggioHa = value;
                    PuntiFeritaDelPersonaggio = value;
                }
                else
                {
                    throw new Exception("punti ferita personaggio non validi");
                }
            }
        }

        public override int Attacca(int dado, out bool dannoCritico, out int valoreDannoCritico)//10% percentuale e 30% di danno in più
        {
            Random r = new Random();
            int appoggio = r.Next(1, 101);
            if (appoggio <= 10)
            {
                dannoCritico = true;
                valoreDannoCritico = 30;

            }
            else
            {
                dannoCritico = false;
                valoreDannoCritico = 0;
            }
            return dado;
        }

    }
}