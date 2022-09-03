using Casino.Classes;
using Casino.Games.SizBizz;
using Casino.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Casino
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectGameControl selectGameControl;

        private SixBizzControl sixBizzControl;

        private Controller controller;

        public MainWindow()
        {
            InitializeComponent();

            this.controller = new Controller(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.controller.Stop();
        }

        public void SetSelectGameControl(GameSelector gameSelector)
        {
            this.Dispatcher.Invoke(() =>
            {
                selectGameControl = new SelectGameControl(gameSelector);

                SetMainView(selectGameControl);
            });
        }

        public void SetGame(Game game)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (game.Id == 0)
                {
                    this.sixBizzControl = new SixBizzControl();
                    this.sixBizzControl.Game = game;
                    SetMainView(sixBizzControl);
                }
            }));
        }

        public void SetGameIntroductionGame(Game game)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (game.Id == 0)
                {
                    Image image = new Image();
                    image.Stretch = Stretch.Fill;
                    image.Source = game.GameIntroductionImageSource;
                    SetMainView(image);
                }
            }));
        }

        public void SetCredit(double credit)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.labelCredit.Content = "Guthaben:\n" + credit.ToString("C");
            }));
        }

        public void SetNoUserSelected()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.labelCredit.Content = "Kein Spieler Angemeldet!";
            }));
        }

        private void SetMainView(UIElement uIElement)
        {
            this.mainGrid.Children.Clear();
            this.mainGrid.Children.Add(uIElement);
        }
    }
}
