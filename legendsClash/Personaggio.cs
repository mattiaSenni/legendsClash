using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public class Personaggio : IComparable<Personaggio>
    {
        public int nome
        {
            get => default;
            set
            {
            }
        }

        public int puntiFerita
        {
            get => default;
            set
            {
            }
        }

        public int numeroVittoria
        {
            get => default;
            set
            {
            }
        }

        public int sourceImmagine
        {
            get => default;
            set
            {
            }
        }

        public void aggiungiVittoria()
        {
            throw new System.NotImplementedException();
        }

        public void ristora()
        {
            throw new System.NotImplementedException();
        }

        public void attacca()
        {
            throw new System.NotImplementedException();
        }

        public int CompareTo(Personaggio other)
        {
            throw new NotImplementedException();
        }
    }
}