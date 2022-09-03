using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class GameSelector
    {
        public List<Game> Games { get => games; set => games = value; }
        public int SelectedGameID { get => selectedGameID; set => selectedGameID = value; }

        public delegate void DelSelectedGameChanged(object sender, int selectedGameID);
        public event DelSelectedGameChanged SelectedGameChanged;

        public delegate void DelGameSelected(object sender, Game game);
        public event DelGameSelected GameSelected;

        private List<Game> games;
        private int selectedGameID = 0;

        public GameSelector(List<Game> games)
        {
            this.games = games;
        }

        public void Left()
        {
            if(this.games.Count > 0 && this.selectedGameID > 0)
            {
                this.selectedGameID--;
                SelectedGameChanged?.Invoke(this, this.selectedGameID);
            }
        }

        public void Right()
        {
            if (this.games.Count > 0 && this.selectedGameID < this.games.Count)
            {
                this.selectedGameID++;
                SelectedGameChanged?.Invoke(this, this.selectedGameID);
            }
        }

        public void Select()
        {
            GameSelected?.Invoke(this, this.games.FirstOrDefault(g => g.Id == this.selectedGameID));
        }
    }
}
