using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class Win
    {
        public double WinMoney { get => winMoney; set => winMoney = value; }
        public Symbole Symbole { get => symbole; set => symbole = value; }
        public Winline Winline { get => winline; set => winline = value; }
        public int SymbolesCount { get => symbolesCount; set => symbolesCount = value; }

        private double winMoney;
        private Symbole symbole;
        private Winline winline;
        private int symbolesCount;

        public Win(int symbolesCount, Symbole symbole, Winline winline)
        {
            this.SymbolesCount = symbolesCount;
            this.symbole = symbole;
            this.winline = winline;
            this.winMoney = CalculateWin();
        }

        private double CalculateWin()
        {
            switch (this.symbolesCount)
            {
                case 3:
                    return this.symbole.Value;
                case 4:
                    return this.symbole.Value * 3;
                case 5:
                    return this.symbole.Value * 6;
                case 6:
                    return this.symbole.Value * 9;
                default:
                    return 0;
            }
        }
    }
}
