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

namespace MediaPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }


        //Play Video
        private void playerPlay(object sender, RoutedEventArgs e)
        {
            Player.Play();
        }
        
        //Pause Video
        private void playerPause(object sender, RoutedEventArgs e)
        {
            Player.Pause();
            if (timer != null)
                timer.Stop();
        }
        
        //Stop Video
        private void playerStop(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void playerMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        //After Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Player.ScrubbingEnabled = true;
            Player.Stop();
        }


        //preparation time
        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimerSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerSlider.Value = Player.Position.TotalSeconds;
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void TimerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            Player.Pause();
            if(timer != null)
            timer.Stop();
        }
    }
}
