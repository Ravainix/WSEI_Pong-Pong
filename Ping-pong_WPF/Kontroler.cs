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
    /// <summary>
    /// Klasa odpowiadajaca za logike gry 
    /// </summary>
    public class Kontroler
    {
        private Canvas canv;
        private Score score;
        private Ball ball;
        private Pad paddlePlayer;
        private Pad paddleAI;
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Konstruktor klasy kontroler
        /// </summary>
        public Kontroler (Canvas canv, Ball ball, Pad padelPlayer, Pad padelAI, Score score)
        {
            this.ball = ball;
            this.paddlePlayer = padelPlayer;
            this.paddleAI = padelAI;
            this.canv = canv;
            this.score = score;
        }

        private double VelocityX;
        private double VelocityY;
        private double paddelV;
        private bool aiStatus = true;

        public double PaddelVelocity { get => paddelV; set => paddelV = value; }
        public bool AiTurnOn { get => aiStatus; set => aiStatus = value; }

        /// <summary>
        /// Ustawia poczatkowe wartosci predkosci pilki oraz petle(Game Loop) odpowiadajaca za poruszanie sie elementow
        /// </summary>
        public void SetUpGame()
        {
            VelocityX = 0.8;
            VelocityY = 0.001;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Zatrzymuje petle
        /// </summary>
        public void StopGame()
        {
            timer.Stop();
        }

        /// <summary>
        /// Wznawia petle
        /// </summary>
        public void StartGame()
        {
            timer.Start();
        }

        /// <summary>
        /// Metoda wykonywana przy kazdej iteracji petli
        /// Zmienia pozycje paletki oraz pozycje pilki
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            ProcessAI(AiTurnOn);
            UpdateBallPosition();
        }

        /// <summary>
        /// Zmienia pozycje pilki
        /// Podstawowa fizyka odbicia pilki
        /// Koncepcja oraz kod skopiowano z:
        /// https://gamedev.stackexchange.com/questions/4253/in-pong-how-do-you-calculate-the-balls-direction-when-it-bounces-off-the-paddl
        /// </summary>
        private void UpdateBallPosition()
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

            //top and bottom bounce 
            if (newBallY < 0)
            {
                newBallY = -newBallY;
                VelocityY = -VelocityY;
            }
            else if (newBallY + 10 > canv.Height)
            {
                newBallY -= 2 * ((newBallY + 10) - (canv.Height));
                VelocityY = -VelocityY;
            }

            //left paddle 
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


            //right and paddle
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


            //left and right, add score
            if (ball.X > canv.Width - 20)
            {
                StopGame();
                ResetGame();
                score.Player1Win();
                return;
            }

            if (ball.X < 0)
            {
                StopGame();
                ResetGame();
                score.Player2Win();
                return;
            }

            //set new position
            ball.X = newBallX;
            ball.Y = newBallY;
        }

        /// <summary>
        /// Ustawia poczatkowe wartosci pilki
        /// </summary>
        public void ResetGame ()
        {
            ball.X = 300;
            ball.Y = 200;

            VelocityX = 0.8;
            VelocityY = 0.001;

            StartGame();
        }

        /// <summary>
        /// Ustawia paletke w srodkowej pozycji wzgledem osi Y 
        /// </summary>
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

        /// <summary>
        /// Prosta "sztuczna" inteligencja odpowiadajaca za poruszanie paletka
        /// Koncepcja zaczerpnieta z: https://stackoverflow.com/questions/23960654/unity-pong-ai-movement-speed
        /// </summary>
        public void ProcessAI(bool state)
        {
            if (state)
            {
                if (VelocityX > 0 && ball.X + (ball.Radius / 2) > canv.Width / 2)
                {
                    if (ball.Y + (ball.Radius / 2) != paddleAI.Y + (paddleAI.Height / 2))
                    {
                        var timeTilCollision = ((canv.Width - paddleAI.X - paddleAI.Width) - ball.X) / (VelocityX);
                        var distanceWanted = (paddleAI.Y + (paddleAI.Height / 2)) - (ball.Y + (ball.Radius / 2));
                        var velocityWanted = -distanceWanted / timeTilCollision;
                        if (velocityWanted > 1)
                        {
                            PaddelVelocity = 1;
                        }
                        else if (velocityWanted < -1)
                        {
                            PaddelVelocity = -1;
                        }
                        else
                        {
                            PaddelVelocity = velocityWanted;
                        }
                    }
                    else
                    {
                        PaddelVelocity = 0;
                    }
                }
                else
                {
                    PaddelVelocity = 0;
                }
                
                //move paddle
                paddleAI.Y += PaddelVelocity * 10;
            }
        }
    }
}
