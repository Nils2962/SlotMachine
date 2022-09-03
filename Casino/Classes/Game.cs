using Casino.Games.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Casino.Classes
{
    public abstract class Game
    {
        private static int idCount = 0;
        private static double[] Commitments = new double[] { 0.10, 0.20, 0.40, 0.60, 0.80, 1, 2, 4, 6, 8, 10, 20, 40, 60, 80, 100 };

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public ImageSource ImageSource { get => imageSource; set => imageSource = value; }
        public double Commitment { get => commitment; }
        public Symbole[] Symbols { get => symbols; set => symbols = value; }
        public ImageSource GameIntroductionImageSource { get => gameIntroductionImageSource; set => gameIntroductionImageSource = value; }
        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }
        public bool CanRoll { get => canRoll; set => canRoll = value; }
        public Winline[] WinLines { get => winLines; set => winLines = value; }

        public delegate void DelRollSymboles(object sender, int[][] symboleMatrix);
        public event DelRollSymboles RollSymboles;

        public delegate void DelCommitmentChanged(object sender, double newCommitment);
        public event DelCommitmentChanged CommitmentChanged;

        public delegate void DelWon(object sender, double wonMoney, List<Win> wins);
        public event DelWon Won;

        private ImageSource imageSource;

        private string name;
        private ImageSource gameIntroductionImageSource;
        private double commitment;
        private int currentCommitIndex = 5;
        private int id;
        private int rows;
        private int columns;
        private Symbole[] symbols;
        private bool canRoll = true;
        private Winline[] winLines;

        public Game(string name, int rows, int columns)
        {
            this.name = name;

            this.id = idCount;

            this.commitment = Commitments[this.currentCommitIndex];
            this.Rows = rows;
            this.Columns = columns;

            idCount++;
        }

        public void AddCommitment()
        {
            if (this.currentCommitIndex < Commitments.Count() - 1)
            {
                this.currentCommitIndex++;
                this.commitment = Commitments[this.currentCommitIndex];
                CommitmentChanged?.Invoke(this, this.commitment);
            }
        }

        public void RemoveCommitment()
        {
            if (this.currentCommitIndex > 0)
            {
                this.currentCommitIndex--;
                this.commitment = Commitments[this.currentCommitIndex];
                CommitmentChanged?.Invoke(this, this.commitment);
            }
        }

        public abstract void Roll(double winBudget);

        public void TriggerRollEvent(int[][] symboleMatrix)
        {
            this.RollSymboles?.Invoke(this, symboleMatrix);
        }

        public void TriggerWonEvent(double wonMoeny, List<Win> wins)
        {
            Won?.Invoke(this, wonMoeny, wins);
        }
    }
}
