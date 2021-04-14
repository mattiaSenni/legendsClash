using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace legendsClash
{
    public class Sfondo
    {
        private string _source;
        private int _id;
        public Sfondo(string s, int i)
        {
            _source = s;
            _id = i;
        }
        public string source
        {
            get
            {
                return _source;
            }
            set
            {

                if (String.IsNullOrEmpty(_source))
                {
                    throw new Exception("errore");
                }
                else
                {
                    _source = value;
                }



            }
        }

        public int id
        {
            get
            {
                return _id;
            }
            set
            {

                if (_id > 0)
                {
                    _id = value;
                }
                else
                {
                    throw new Exception("errore");
                }

            }
        }
    }
}