using Casino.Games.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Casino.Classes
{
    public class Controller
    {
        public static Controller Instance { get => _controller; }

        private static Controller _controller;

        public Profile CurrentProfil { get => currentProfil; set => currentProfil = value; }

        private UDPController uDPController;
        private int port = 2121;
        private MainWindow mainWindow;
        private List<Game> games;
        private GameSelector gameSelector;
        private GameMode gameMode;
        private Game selectedGame;
        private MoneyManager moneyManager;
        private DataController dataController;

        private List<Profile> profiles;
        private Profile currentProfil;

        public Controller(MainWindow mainWindow)
        {
            this.gameMode = GameMode.GameSelction;

            this.mainWindow = mainWindow;
            this.mainWindow.Closed += MainWindow_Closed;
            this.mainWindow.SetNoUserSelected();

            this.dataController = new DataController();
            this.profiles = dataController.LoadProfiles();

            //Profile profile = new Profile(86864523);
            //SetCurrentProfile(profile);

            this.moneyManager = new MoneyManager();

            this.uDPController = new UDPController(this.port);
            this.uDPController.MessageReceived += UDPController_MessageReceived;
            this.uDPController.StartReceiver();

            this.games = new List<Game>()
            { 
                new SixBizzGame("SixBizz", 3, 6) { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/SixBizz.png")),  GameIntroductionImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/IntroductionBackground.png")), Symbols = new Symbole[]
                {
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Andi.png")), ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Finn.png")), ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Nils.png")), ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Sophie.png")),ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Marie.png")), ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Niklas.png")), ID = 0, Value = 100 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Bar.png")), ID = 1, Value = 40 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Beerpong.png")), ID = 2, Value = 30 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Bier.png")), ID = 3, Value = 16 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Caravan.png")),ID = 4, Value = 60 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Lillet.png")), ID = 5, Value = 10 }, 
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/ShishaKohle.png")), ID = 6, Value = 6 },
                    new Symbole() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Casino;component/Assets/Shisha.png")), ID = 7, Value = 20 }
                } }
            };
            this.gameSelector = new GameSelector(this.games);
            this.gameSelector.GameSelected += GameSelector_GameSelected;

            this.mainWindow.SetSelectGameControl(gameSelector);

            //this.CurrentProfil.AddCredit(100);

            _controller = this;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.uDPController.StopReveiver();

            Environment.Exit(0);
        }

        private void UDPController_MessageReceived(string message, string senderIP)
        {
            Console.WriteLine(message);

            switch (message.ToLower())
            {
                case "right":
                    Right();
                    break;
                case "rightcenter":
                    RightCenter();
                    break;
                case "center":
                    Center();
                    break;
                case "leftcenter":
                    LeftCenter();
                    break;
                case "left":
                    Left();
                    break;
                default:
                    break;
            }
        }

        private void GameSelector_GameSelected(object sender, Game game)
        {
            this.selectedGame = game;
            this.mainWindow.SetGameIntroductionGame(game);
        }

        private void CurrentProfil_CreditChanged(object sender, double newcredit)
        {
            this.mainWindow.SetCredit(newcredit);

            this.dataController.SaveProfiles(this.profiles);
        }

        public void Stop()
        {
            this.uDPController.StopReveiver();
        }

        private void Left()
        {
            if(this.gameMode == GameMode.GameSelction)
            {
                this.gameSelector.Left();
            }
            else if (this.gameMode == GameMode.Game)
            {
                this.mainWindow.SetSelectGameControl(this.gameSelector);
                this.gameMode = GameMode.GameSelction;
            }
        }

        private void Right()
        {
            if (this.gameMode == GameMode.GameSelction)
            {
                this.gameSelector.Right();
            }
        }

        private void LeftCenter()
        {
            if (this.gameMode == GameMode.GameSelction)
            {
                this.gameSelector.Left();
            }
            else if (this.gameMode == GameMode.Game)
            {
                this.selectedGame.RemoveCommitment();
            }
        }

        private void RightCenter()
        {
            if (this.gameMode == GameMode.Game)
            {
                this.selectedGame.AddCommitment();
            }
        }

        private void Center()
        {
            if (this.gameMode == GameMode.GameSelction)
            {
                this.gameSelector.Select();
                this.gameMode = GameMode.GameIntroduction;
            }
            else if(this.gameMode == GameMode.GameIntroduction && this.currentProfil != null)
            {
                this.mainWindow.SetGame(this.selectedGame);
                this.gameMode = GameMode.Game;
            }
            else if(this.gameMode == GameMode.Game)
            {
                Roll();
            }
        }

        private void Roll()
        {
            if (this.CurrentProfil.CanRoll(this.selectedGame.Commitment) && this.selectedGame.CanRoll)
            {
                this.CurrentProfil.RemoveCommitment(this.selectedGame.Commitment);
                this.selectedGame.Roll(this.moneyManager.GetWinKapital());
            }
        }

        private void SetCurrentProfile(Profile profile)
        {
            this.CurrentProfil = profile;
            this.CurrentProfil.CreditChanged += CurrentProfil_CreditChanged;

            this.mainWindow.SetCredit(this.CurrentProfil.Credit);
        }
    }
}
