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
        private Pad paddlePlayer;
        private Pad paddleAI;
        private DispatcherTimer timer = new DispatcherTimer();


        public Kontroler (Canvas canv, Ball ball, Pad padelPlayer, Pad padelAI, Score score)
        {
            this.ball = ball;
            this.paddlePlayer = padelPlayer;
            this.paddleAI = padelAI;
            this.canv = canv;
            this.score = score;
        }

        private double _angle = 140;
        private double _speed = 2;

        public double Angle { get => _angle; set => _angle = value; }
        public double Speed { get => _speed; set => _speed = value; }

        private double VelocityX;
        private double VelocityY;

        public void RunGame()
        {
            VelocityX = 0.8;
            VelocityY = 0.001;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void StopGame()
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double newBallX = ball.X + VelocityX * 10;
            double newBallY = ball.Y + VelocityY * 10;
            double MAXBOUNCEANGLE = Math.PI / 12;

            double intersectX;
            double intersectY;
            double relativeIntersectY;
            double bounceAngle;
            double ballSpeed;
            double ballTravelLeft;

            if(newBallY < 0)
            {
                newBallY = -newBallY;
                VelocityY = -VelocityY;
            } else if(newBallY + 10 > canv.Height)
            {
                newBallY -= 2 * ((newBallY + 10) - (canv.Height));
                VelocityY = -VelocityY;
            }

            //lewa 
            if (newBallX < paddlePlayer.X + paddlePlayer.Width && ball.X >= paddlePlayer.X + paddlePlayer.Width)
            {
                intersectX = paddlePlayer.X + paddlePlayer.Width;
                intersectY = ball.Y - ((ball.X - (paddlePlayer.X + paddlePlayer.Width)) * (ball.Y - newBallY)) / (ball.X - newBallX);
                if (intersectY >= paddlePlayer.Y && intersectY <= paddlePlayer.Y + paddlePlayer.Height)
                {
                    relativeIntersectY = (paddlePlayer.Y + (paddlePlayer.Height / 2)) - intersectY;
                    bounceAngle = (relativeIntersectY / (paddlePlayer.Height / 2)) * (Math.PI / 2 - MAXBOUNCEANGLE);
                    ballSpeed = Math.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
                    ballTravelLeft = (newBallY - intersectY) / (newBallY - ball.Y);
                    VelocityX = ballSpeed * Math.Cos(bounceAngle);
                    VelocityY = ballSpeed * -Math.Sin(bounceAngle);
                    newBallX = intersectX + ballTravelLeft * ballSpeed * Math.Cos(bounceAngle);
                    newBallY = intersectY + ballTravelLeft * ballSpeed * Math.Sin(bounceAngle);
                }
            }


            //prawa
            if (newBallX > canv.Width - paddleAI.X - paddleAI.Width - paddleAI.Width && ball.X <= canv.Width - paddleAI.X - paddleAI.Width - paddleAI.Width)
            {
                intersectX = canv.Width - paddleAI.X - paddleAI.Width - paddleAI.Width;
                intersectY = ball.Y - ((ball.X - (canv.Width - paddleAI.X - paddleAI.Width)) * (ball.Y - newBallY)) / (ball.X - newBallX);
                if (intersectY >= paddleAI.Y && intersectY <= paddleAI.Y + paddleAI.Height)
                {
                    relativeIntersectY = (paddleAI.Y + (paddleAI.Height / 2)) - intersectY;
                    bounceAngle = (relativeIntersectY / (paddleAI.Height / 2)) * (Math.PI / 2 - MAXBOUNCEANGLE);
                    ballSpeed = Math.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
                    ballTravelLeft = (newBallY - intersectY) / (newBallY - VelocityY);
                    VelocityX = ballSpeed * Math.Cos(bounceAngle) * -1;
                    VelocityY = ballSpeed * Math.Sin(bounceAngle) * -1;
                    newBallX = intersectX - ballTravelLeft * ballSpeed * Math.Cos(bounceAngle);
                    newBallY = intersectY - ballTravelLeft * ballSpeed * Math.Sin(bounceAngle);
                }
            }


            //SpeedUp();

            if (ball.X > canv.Width - 20)
            {
                score.Player1Win();
                StopGame();
            }

            if (ball.X < 0)
            {
                score.Player2Win();
                StopGame();
            }

            ball.X = newBallX;
            ball.Y = newBallY;
        }

        private void ResetGame ()
        {
            ball.X = canv.Width / 2;
            ball.Y = canv.Height / 2;
        }

        public void MovePad(double Y, Pad pad)
        {
            double middlePad = Y - pad.Height / 2;

            pad.Y = middlePad;

            if (middlePad > canv.ActualHeight - pad.Height)
            {
                pad.Y = canv.ActualHeight - pad.Height;
            }

            if (middlePad <= 0)
            {
                pad.Y = 0;
            }
        }
    }
}
