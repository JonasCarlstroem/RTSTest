using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RTStest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point mouse;
        private bool IsSelected;
        private double NextX, NextY;
        private double moveSpeed = 1;
        private DispatcherTimer GameTime = new DispatcherTimer();
        private Rectangle Player = new Rectangle() { Width = 20, Height = 20, Fill = Brushes.Green, StrokeThickness = 2, Stroke = Brushes.Red, Focusable = true };
        public MainWindow()
        {
            InitializeComponent();
            GameTime.Tick += new EventHandler(MoveTo);
            GameTime.Interval = TimeSpan.FromMilliseconds(1);
        }


        private async void MoveTo(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                NextX = Canvas.GetLeft(Player); NextY = Canvas.GetTop(Player);
                while (true)
                {
                    await Task.Delay(50);
                    if (NextX < mouse.X)
                    {
                        NextX += moveSpeed;
                        Canvas.SetLeft(Player, NextX);
                    }
                    if (NextY < mouse.Y)
                    {
                        NextY += moveSpeed;
                        Canvas.SetTop(Player, NextY);
                    }
                    if (NextX > mouse.X)
                    {
                        NextX -= moveSpeed;
                        Canvas.SetLeft(Player, NextX);
                    }
                    if (NextY > mouse.Y)
                    {
                        NextY -= moveSpeed;
                        Canvas.SetTop(Player, NextY);
                    }
                    if (NextX == mouse.X && NextY == mouse.Y)
                    {
                        GameTime.Stop();
                        break;
                    }
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            
            double PlayerStartX = 10, PlayerStartY = gameField.Height - Player.Height;
            gameField.Children.Add(Player);
            Canvas.SetLeft(Player, PlayerStartX);
            Canvas.SetTop(Player, PlayerStartY);
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouse = Mouse.GetPosition(gameField);
            if(mouse.X >= Canvas.GetLeft(Player) && mouse.X <= Canvas.GetLeft(Player) + Player.Width && mouse.Y >= Canvas.GetTop(Player) && mouse.Y <= Canvas.GetTop(Player) + Player.Height)
            {
                IsSelected = true;
                if (IsSelected)
                {
                    focusDebug.Content = "Is player focused? YES";
                }
            }
            else
            {
                IsSelected = false;
                if (!IsSelected)
                {
                    focusDebug.Content = "Is player focused? NO";
                }
            }
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            GameTime.Stop();
            mouse = Mouse.GetPosition(gameField);
            GameTime.Start();
        }
    }
}
