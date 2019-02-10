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
        private double x;
        private double y;
        private int width;
        private int height;

        public double X {
            get => this.x;
            set {
                this.x = value;
               OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get => y;
            set
            {
                y = value;
               OnPropertyChanged("Y");
            }
        }

        public double Radius { get => width; }

        public Ball(int x, int y, int radius)
        {
            X = x;
            Y = y;
            this.width = radius;
            this.height = radius;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
