using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    /// <summary>
    /// Klasa reprezentujaca paletke
    /// </summary>
    public class Pad : INotifyPropertyChanged
    {
        private double x;
        private double y;
        private double width;
        private double height;

        public Pad(double x, double y,int width, int height)
        {
            this.x = x;
            Y = y;
            this.width = width;
            this.height = height;
        }

        public double Width { get => width; }
        public double Height { get => height; }
        public double X { get => x; }
        public double Y
        {
            get => this.y;
            set
            {
                this.y = value;
                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// Zdarzenie powiadamiajace o zmianie wlasciwosci
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Obsluga zdarzenia PropertyChanged
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
