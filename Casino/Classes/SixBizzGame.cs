using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class SixBizzGame : Game
    {
        private Random symboleRandom;

        public SixBizzGame(string name, int rows, int columns) : base(name, rows, columns)
        {
            this.symboleRandom = new Random();

            this.WinLines = new Winline[] {
                new Winline(0, new int[] { 0, 0, 0, 0, 0, 0 }),
                new Winline(1, new int[] { 1, 1, 1, 1, 1, 1 }),
                new Winline(2, new int[] { 2, 2, 2, 2, 2, 2 }),
                new Winline(3, new int[] { 2, 2, 1, 1, 0, 0 }),
                new Winline(4, new int[] { 0, 0, 1, 1, 2, 2 }),
                new Winline(5, new int[] { 1, 1, 0, 0, 1, 1 }),
                new Winline(6, new int[] { 1, 1, 2, 2, 1, 1 }),
                new Winline(7, new int[] { 2, 1, 2, 2, 1, 2 }),
                new Winline(8, new int[] { 0, 1, 0, 0, 1, 0 }),
                new Winline(9, new int[] { 2, 0, 2, 2, 0, 2 }),
                new Winline(10, new int[] { 0, 2, 0, 0, 2, 0 }),
                new Winline(11, new int[] { 1, 0, 0, 0, 0, 1 }),
                new Winline(12, new int[] { 1, 2, 2, 2, 2, 1 }),
                new Winline(13, new int[] { 2, 1, 0, 0, 0, 0 }),
                new Winline(14, new int[] { 0, 1, 2, 2, 2, 2 }),
                new Winline(15, new int[] { 2, 0, 1, 1, 0, 2 }),
                new Winline(16, new int[] { 0, 2, 1, 1, 2, 0 })
            };
        }

        public override void Roll(double winBudget)
        {
            int[][] matrix = new int[this.Columns][];
            int maxID = this.Symbols.Max(s => s.ID);
            double completeWin = 0;
            List<Win> wins = new List<Win>();

            do
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    matrix[c] = new int[this.Rows];

                    for (int r = 0; r < this.Rows; r++)
                    {
                        matrix[c][r] = this.symboleRandom.Next(0, maxID);
                    }
                }

                wins = GetWinLines(matrix);

                completeWin = 0;

                foreach (Win win in wins)
                {
                    completeWin += win.WinMoney;
                }

                completeWin *= this.Commitment;

            } while (winBudget < completeWin);
            
            if(completeWin > 0)
            {
                completeWin = completeWin / 10; 

                TriggerWonEvent(completeWin, wins);
            }

            TriggerRollEvent(matrix);
        }

        private List<Win> GetWinLines(int[][] matrix)
        {
            List<Win> winLines = new List<Win>();

            for (int l = 0; l < this.WinLines.Count(); l++)
            {
                Winline winline = this.WinLines[l];

                int firstSymboleID = -1;
                bool isWinLine = false;
                int symbolesInLine = 0;

                for (int c = 0; c < this.Columns; c++)
                {
                    int symboleID = matrix[c][winline.LineIndexs[c]];

                    if (firstSymboleID == -1)
                    {
                        firstSymboleID = symboleID;
                        symbolesInLine++;
                    }
                    else
                    {
                        if(symboleID == firstSymboleID)
                        {
                            symbolesInLine++;

                            if (symbolesInLine >= 3)
                                isWinLine = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                
                if(isWinLine)
                {
                    winLines.Add(new Win(symbolesInLine, this.Symbols.FirstOrDefault(s => s.ID == firstSymboleID), winline));
                }
            }

            return winLines;
        }
    }
}
