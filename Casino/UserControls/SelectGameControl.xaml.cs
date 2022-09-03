using Casino.Classes;
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

namespace Casino.UserControls
{
    /// <summary>
    /// Interaktionslogik für SelectGameControl.xaml
    /// </summary>
    public partial class SelectGameControl : UserControl
    {
        private List<Game> games;

        public SelectGameControl(GameSelector gameSelector)
        {
            InitializeComponent();

            this.games = gameSelector.Games;
            gameSelector.SelectedGameChanged += GameSelector_SelectedGameChanged;

            FillGames();
        }

        private void GameSelector_SelectedGameChanged(object sender, int selectedGameID)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.flipview.SelectedIndex = selectedGameID;
            }));
        }

        private void FillGames()
        {
            foreach (Game game in this.games)
            {
                GamePreviewControl gamePreviewControl = new GamePreviewControl()
                {
                    ImageSource = game.ImageSource,
                    Title = game.Name,
                    ID = game.Id,
                    Width = 550
                };

                this.flipview.Items.Add(gamePreviewControl);
            }
        }
    }
}
