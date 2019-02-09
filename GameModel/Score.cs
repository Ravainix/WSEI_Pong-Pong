using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Score : INotifyPropertyChanged
    {
        private int player1;
        private int player2;

        public Score()
        {
            player1 = 0;
            player2 = 0;
        }

        public string PlayersScore { get => ToString();  }

        public void Player1Win()
        {
            player1 += 1;
            OnPropertyChanged("PlayersScore");
        }

        public void Player2Win()
        {
            player2 += 1;
            OnPropertyChanged("PlayersScore");
        }

        public void Reset()
        {
            player1 = 0;
            player2 = 0;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", player1, player2);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
