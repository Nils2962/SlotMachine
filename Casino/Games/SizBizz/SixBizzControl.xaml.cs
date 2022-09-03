using Casino.Classes;
using Casino.Games.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino.Games.SizBizz
{
    /// <summary>
    /// Interaktionslogik für SixBizzControl.xaml
    /// </summary>
    public partial class SixBizzControl : UserControl
    {
        public Game Game { get => game; set { game = value; SetGame(); } }

        private Game game;
        private bool canRoll = true;
        private int finishedRolls = 0;

        private double winMoney = 0;
        private List<Win> wins;
        private Controller controller;

        public SixBizzControl()
        {
            this.InitializeComponent();

            this.controller = Controller.Instance;
        }

        private void Game_RollSymboles(object sender, int[][] symboleMatrix)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Roll(symboleMatrix);
            }));
        }

        private void Game_CommitmentChanged(object sender, double newCommitment)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.commitmentLabel.Content = newCommitment + "€";
            }));
        }

        private void Game_Won(object sender, double wonMoney, List<Win> wins)
        {
            this.winMoney = wonMoney;
            this.wins = wins;
        }

        private void Roll(int[][] symboleMatrix)
        {
            if (canRoll)
            {
                labelWinMoney.Visibility = Visibility.Hidden;
                imageWinLine1.Visibility = Visibility.Hidden;
                imageWinLine2.Visibility = Visibility.Hidden;
                imageWinLine3.Visibility = Visibility.Hidden;
                imageWinLine4.Visibility = Visibility.Hidden;
                imageWinLine5.Visibility = Visibility.Hidden;
                imageWinLine6.Visibility = Visibility.Hidden;
                imageWinLine7.Visibility = Visibility.Hidden;
                imageWinLine8.Visibility = Visibility.Hidden;
                imageWinLine9.Visibility = Visibility.Hidden;
                imageWinLine10.Visibility = Visibility.Hidden;
                imageWinLine11.Visibility = Visibility.Hidden;
                imageWinLine12.Visibility = Visibility.Hidden;
                imageWinLine13.Visibility = Visibility.Hidden;
                imageWinLine14.Visibility = Visibility.Hidden;
                imageWinLine15.Visibility = Visibility.Hidden;
                imageWinLine16.Visibility = Visibility.Hidden;
                imageWinLine17.Visibility = Visibility.Hidden;

                rollOne.RollTo(symboleMatrix[0]);
                rollTwo.RollTo(symboleMatrix[1]);
                rollThree.RollTo(symboleMatrix[2]);
                rollFour.RollTo(symboleMatrix[3]); 
                rollFive.RollTo(symboleMatrix[4]);
                rollSix.RollTo(symboleMatrix[5]);

                this.canRoll = false;
                this.game.CanRoll = false;
                this.finishedRolls = 0;
            }
        }

        private void Roll_RollFinished(object sender)
        {
            this.finishedRolls++;

            if (this.finishedRolls == 6)
            {
                this.canRoll = true;
                this.game.CanRoll = true;

                if(this.winMoney != 0)
                {
                    ShowWin(this.winMoney, this.wins);
                }
            }
        }

        private void SetGame()
        {
            List<SymbolControl> symbolControls = new List<SymbolControl>();

            foreach (Symbole symbole in this.game.Symbols)
            {
                symbolControls.Add(new SymbolControl() { Symbole = symbole });
            }

            rollOne.UsingSymbols = symbolControls.ToArray();
            rollTwo.UsingSymbols = symbolControls.ToArray();
            rollThree.UsingSymbols = symbolControls.ToArray();
            rollFour.UsingSymbols = symbolControls.ToArray();
            rollFive.UsingSymbols = symbolControls.ToArray();
            rollSix.UsingSymbols = symbolControls.ToArray();

            rollOne.SetCurrentItems(new int[] { 0, 0, 0 });
            rollTwo.SetCurrentItems(new int[] { 1, 1, 1 });
            rollThree.SetCurrentItems(new int[] { 2, 2, 2 });
            rollFour.SetCurrentItems(new int[] { 3, 3, 3 });
            rollFive.SetCurrentItems(new int[] { 4, 4, 4 });
            rollSix.SetCurrentItems(new int[] { 5, 5, 5 });

            this.commitmentLabel.Content = this.game.Commitment + "€";

            this.game.RollSymboles += Game_RollSymboles;
            this.game.CommitmentChanged += Game_CommitmentChanged;
            this.game.Won += Game_Won;
        }

        private void ShowWin(double wonMoney, List<Win> wins)
        {
            ShowWinMoneyAsync(wonMoney);
            ShowWinLinesAsync(wins);
            this.controller.CurrentProfil.AddCredit(wonMoney);
        }

        private void ShowWinLinesAsync(List<Win> wins)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                foreach (Win win in wins)
                {
                    Thread.Sleep(250);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        switch (win.Winline.ID)
                        {
                            case 0:
                                imageWinLine1.Visibility = Visibility.Visible;
                                break;
                            case 1:
                                imageWinLine2.Visibility = Visibility.Visible;
                                break;
                            case 2:
                                imageWinLine3.Visibility = Visibility.Visible;
                                break;
                            case 3:
                                imageWinLine4.Visibility = Visibility.Visible;
                                break;
                            case 4:
                                imageWinLine5.Visibility = Visibility.Visible;
                                break;
                            case 5:
                                imageWinLine6.Visibility = Visibility.Visible;
                                break;
                            case 6:
                                imageWinLine7.Visibility = Visibility.Visible;
                                break;
                            case 7:
                                imageWinLine8.Visibility = Visibility.Visible;
                                break;
                            case 8:
                                imageWinLine9.Visibility = Visibility.Visible;
                                break;
                            case 9:
                                imageWinLine10.Visibility = Visibility.Visible;
                                break;
                            case 10:
                                imageWinLine11.Visibility = Visibility.Visible;
                                break;
                            case 11:
                                imageWinLine12.Visibility = Visibility.Visible;
                                break;
                            case 12:
                                imageWinLine13.Visibility = Visibility.Visible;
                                break;
                            case 13:
                                imageWinLine14.Visibility = Visibility.Visible;
                                break;
                            case 14:
                                imageWinLine15.Visibility = Visibility.Visible;
                                break;
                            case 15:
                                imageWinLine16.Visibility = Visibility.Visible;
                                break;
                            case 16:
                                imageWinLine17.Visibility = Visibility.Visible;
                                break;
                        }
                    }));
                }
            }));

            thread.Start();
        }

        private void ShowWinMoneyAsync(double winMoney)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                labelWinMoney.Visibility = Visibility.Visible;
                labelWinMoney.Content = winMoney.ToString("C");
                this.winMoney = 0;
            }));
        }
    }
}
