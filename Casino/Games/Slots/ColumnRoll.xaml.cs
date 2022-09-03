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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino.Games.Slots
{
    /// <summary>
    /// Interaktionslogik für ThreeRowRoll.xaml
    /// </summary>
    public partial class ColumnRoll : UserControl
    {
        public SymbolControl[] UsingSymbols { get => usingSymbols; set => usingSymbols = value; }
        public int Size { get => size; }
        public int Rows { get => rows; set => rows = value; }
        public int Items { get => items; set => items = value; }
        public int[] CurrentSymboles { get => currentSymboles; }

        public delegate void DelRollFinished(object sender);
        public event DelRollFinished RollFinished;

        private SymbolControl[] usingSymbols;
        private int size = 50;
        private int rows = 3;
        private int items = 10;
        private int[] currentSymboles;

        private Grid grid;

        public ColumnRoll()
        {
            this.InitializeComponent();
        }

        public void RollTo(int[] endIds)
        {
            if (endIds.Length == this.rows)
            {
                Random random = new Random();
                TimeSpan seconds = TimeSpan.FromSeconds(this.items / 10 * 0.8);

                this.currentSymboles = endIds;
                this.size = Convert.ToInt32(this.ActualHeight / rows);
                SetGrid();

                for (int i = 0; i < rows; i++)
                {
                    SymbolControl[] avaiableSymboles = this.usingSymbols.Where(s => s.Symbole.ID == endIds[i]).ToArray();

                    SymbolControl useSymbol;

                    if (avaiableSymboles.Length > 1)
                        useSymbol = avaiableSymboles[random.Next(0, avaiableSymboles.Length)];
                    else
                        useSymbol = avaiableSymboles[0];

                    SymbolControl endsymbol = new SymbolControl() { Symbole = useSymbol.Symbole };
                    this.grid.Children.Add(endsymbol);
                    Grid.SetRow(endsymbol, i);
                }

                for (int i = 0; i < this.items - rows; i++)
                {
                    int randomID = random.Next(0, this.usingSymbols.Length - 1);

                    SymbolControl[] avaiableSymboles = this.usingSymbols.Where(s => s.Symbole.ID == this.usingSymbols[randomID].Symbole.ID).ToArray();

                    SymbolControl useSymbol;

                    if (avaiableSymboles.Length > 1)
                        useSymbol = avaiableSymboles[random.Next(0, avaiableSymboles.Length)];
                    else
                        useSymbol = avaiableSymboles[0];

                    SymbolControl endsymbol = new SymbolControl() { Symbole = useSymbol.Symbole };
                    this.grid.Children.Add(endsymbol);
                    Grid.SetRow(endsymbol, this.rows + i);
                }

                Storyboard storyboard = new Storyboard();

                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                thicknessAnimation.BeginTime = new TimeSpan(0);

                Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(MarginProperty));

                thicknessAnimation.From = new Thickness(0, grid.Margin.Top, 0, 0);
                thicknessAnimation.To = new Thickness(0, 0, 0, 0);
                thicknessAnimation.Duration = new Duration(seconds);

                storyboard.Children.Add(thicknessAnimation);
                storyboard.Completed += Storyboard_Completed;
                storyboard.Begin(grid);
                StartTimer((int)seconds.TotalSeconds);
            }
        }

        public void SetCurrentItems(int[] ids)
        {
            Random random = new Random();

            SetGrid();

            for (int i = 0; i < ids.Length; i++)
            {
                int randomID = random.Next(0, this.usingSymbols.Length - 1);

                SymbolControl[] avaiableSymboles = this.usingSymbols.Where(s => s.Symbole.ID == ids[i]).ToArray();

                SymbolControl useSymbol;

                if (avaiableSymboles.Length > 1)
                    useSymbol = avaiableSymboles[random.Next(0, avaiableSymboles.Length)];
                else
                    useSymbol = avaiableSymboles[0];

                SymbolControl endsymbol = new SymbolControl() { Symbole = useSymbol.Symbole };
                this.grid.Children.Add(endsymbol);
                Grid.SetRow(endsymbol, this.items - this.rows + i);
            }
        }

        private void SetGrid()
        {
            this.userControl.Content = null;

            this.grid = new Grid();
            this.userControl.Content = this.grid;
            this.grid.Height = this.ActualHeight;
            this.grid.Margin = new Thickness(0, 0, 0, 0);
            this.grid.Children.Clear();
            this.grid.RowDefinitions.Clear();

            for (int i = 0; i < this.items; i++)
            {
                this.grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(this.size, GridUnitType.Pixel) });

                if (i + 1 > rows)
                {
                    this.grid.Height += this.size;
                    this.grid.Margin = new Thickness(0, this.grid.Margin.Top - this.size, 0, 0);
                }
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            SetCurrentItems(this.currentSymboles);
        }

        private void StartTimer(int seconds)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    Thread.Sleep(seconds * 1000);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        RollFinished?.Invoke(this);
                    }));
                }));
                thread.Start();
            }
            catch (Exception)
            {
                RollFinished?.Invoke(this);
            }
        }
    }
}
