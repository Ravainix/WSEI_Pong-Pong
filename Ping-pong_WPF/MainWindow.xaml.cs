using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ping_pong_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ball _ball = new Ball(300, 200, 10);
        Pad _pad1 = new Pad(10, 150, 10, 75);
        Pad _pad2 = new Pad(10, 200, 10, 75);
        Score _score = new Score();
        Kontroler Controller;

        public MainWindow()
        {
            InitializeComponent();

            Controller = new Kontroler(Canvas, _ball, _pad1, _pad2, _score);

            Ball1.DataContext = _ball;
            paddel1.DataContext = _pad1;
            paddel2.DataContext = _pad2;
            Score.DataContext = _score;

        }

        private bool gameStarted = false;

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            Controller.MovePad(e.GetPosition(this).Y, _pad1);
            //Controller.MovePad(e.GetPosition(this).Y, _pad2);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.F1))
                Controller.StopGame();

            if (Keyboard.IsKeyDown(Key.F2))
            {
                Controller.ResetGame();
                Controller.StartGame();
            }

            if(!gameStarted)
            {
                startMessage.Visibility = Visibility.Hidden;
                Controller.RunGame();
                gameStarted = !gameStarted;
            }

        }
    }
}