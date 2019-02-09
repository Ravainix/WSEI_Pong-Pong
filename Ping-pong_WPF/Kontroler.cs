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
        private Canvas canv;
        private Score score;
        private Ball ball;
        private Pad padelPlayer;
        private Pad padelAI;
        private DispatcherTimer timer = new DispatcherTimer();


        public Kontroler (Canvas canv, Ball ball, Pad padelPlayer, Pad padelAI, Score score)
        {
            this.ball = ball;
            this.padelPlayer = padelPlayer;
            this.padelAI = padelAI;
            this.canv = canv;
            this.score = score;
        }

        private double _angle = 140;
        private double _speed = 2;
        private int _padSpeed = 7;

        public double Angle { get => _angle; set => _angle = value; }
        public double Speed { get => _speed; set => _speed = value; }

        public void runGame()
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public void stopGame()
        {
            timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            double radian = (Math.PI / 180) * Angle;
            Vector vector = new Vector { X = Math.Sin(radian), Y = -Math.Cos(radian) };

            if (ball.Y <= 0)
                Angle = Angle + (180 - 2 * Angle);

            if (ball.Y >= canv.ActualHeight - 15)
                Angle = Angle + (180 - 2 * Angle);


            ball.X += vector.X * Speed;
            ball.Y += vector.Y * Speed;

            if (ball.X > canv.ActualWidth - 20)
            {
                score.Player1Win();
                stopGame();
            }

            if (ball.X < 0)
            {
                score.Player2Win();
                stopGame();
            }
        }

        private void ResetGame ()
        {
            ball.X = canv.ActualWidth / 2;
            ball.Y = canv.ActualHeight / 2;
        }

        public void movePad(double Y, Pad pad)
        {
            double padY = Y - 100 / 2;

            pad.Y = padY;

            if (padY > canv.ActualHeight - 100)
            {
                pad.Y = canv.ActualHeight - 100;
            }

            if (padY <= 0)
            {
                pad.Y = 0;
            }

        }

    }
}
