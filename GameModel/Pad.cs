using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Pad : INotifyPropertyChanged
    {
        private double _y;

        public Pad(double y)
        {
            Y = y;
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
