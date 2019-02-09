using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Ping_pong_WPF
{
    public class Kontroler
    {
        public Ball ball;
        public Pad pad1;
        public Pad pad2;

        private Canvas canv;

        public Kontroler (Canvas canvas, Ball ball, Pad padelPlayer, Pad padelAI)
        {
            this.ball = ball;
            pad1 = padelPlayer;
            pad2 = padelAI;
            canv = canvas; 
        }

        private double _angle = 140;
        private double _speed = 1;
        private int _padSpeed = 7;

        public double Angle { get => _angle; set => _angle = value; }
        public double Speed { get => _speed; set => _speed = value; }

        public void runGame()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (ball.Y <= 0) Angle = Angle + (180 - 2 * Angle);
            if (ball.Y >= canv.ActualHeight - 20) Angle = Angle + (180 - 2 * Angle);

            double radians = (Math.PI / 180) * Angle;
            Vector vector = new Vector { X = Math.Sin(radians), Y = -Math.Cos(radians) };
            ball.X += vector.X * Speed;
            ball.Y += vector.Y * Speed;
        }
    }
}
