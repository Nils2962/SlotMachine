using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    class MoneyManager
    {
        public double Kapital { get => kapital; }
        public double MinKaptial { get => minKaptial; set => minKaptial = value; }
        public double Qoute { get => qoute; set => qoute = value; }
        public double WinQoute { get => winQoute; set => winQoute = value; }

        private double kapital = 510;
        private double minKaptial = 500;
        private double qoute = 95;
        private double winQoute = 20;

        public double GetWinKapital()
        {
            return this.kapital - this.minKaptial;
        }
    }
}
