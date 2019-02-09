using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Ball : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        private bool _movingRight;
        private int _leftResult;
        private int _rightResult;

        public double X {
            get => _x;
            set {
                _x = value;
               OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
               OnPropertyChanged("Y");
            }
        }

        public Ball(int x, int y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
