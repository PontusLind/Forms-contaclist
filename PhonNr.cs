using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ovning16._1
{
    public class PhonNr
    {
        private string type;
        private string nR;

        public PhonNr(string type, string nR)
        {
            Type = type;
            NR = nR;
        }
        public string Type
        {
            get { return type; }
            set {  type = value; }        
        }

        public string NR
        {
            get { return nR; }
            set { nR = value; }
        }
    }
}