using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Globalization;

namespace Metronome
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            rate = 60;
            InitializeComponent();
            textboxAmount.Text = rate.ToString();
            slider1.Value = rate;
            sliderVolume.Value = 0.75;

            this.DataContext = this;
        }

        Storyboard sbPanFWD, sbPanBCK, sbElLeft, sbElRight, sbLineThick, sbColor;
        MediaElement media = new MediaElement();
        double rate, perMsec;
        const int secInMin = 60;

        bool flag1 = false;
        int flag2 = 0;
        bool flag3 = false;
        bool flag4 = false;
       

        public void buttonTop_Click(object sender, RoutedEventArgs e)
        {
            if (flag1 == false)
            {
                flag1 = !flag1;
                sbPanFWD = this.Resources["SBpanelForward"] as Storyboard;
                sbPanFWD.Begin();                                
            }
            else
            {
                flag1 = !flag1;
                sbPanBCK = this.Resources["SBpanelBack"] as Storyboard;
                sbPanBCK.Begin();
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (textBl_borderStart.Text == "START")
            {
                textBl_borderStart.Text = "STOP";
                rate = Convert.ToDouble(textboxAmount.Text, CultureInfo.InvariantCulture);
                perMsec = (secInMin / rate) * 1000;       //Amount of beats per millisecond
                media.Play();
                this.DataContext = new Duration(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(perMsec)));
                flag2++;

                sbElLeft = this.Resources["LineAnimLeft"] as Storyboard;
                sbElLeft.Begin();
                sbElRight = this.Resources["LineAnimRight"] as Storyboard;
                sbLineThick = this.Resources["LineThicknessAnim"] as Storyboard;
                sbColor = this.Resources["ColorAnim"] as Storyboard;

            }
            else if (textBl_borderStart.Text == "STOP")
            {
                sbElLeft.Stop();
                sbElRight.Stop();
                sbLineThick.Stop();
                sbColor.Stop();
                textBl_borderStart.Text = "START";
            }
        }

        private void minusPath_MouseDown(object sender, MouseButtonEventArgs e)
        {
            slider1.Value--;
        }

        private void plusPath_MouseDown(object sender, MouseButtonEventArgs e)
        {
            slider1.Value++;
        }

        private void StoryboardLeft_Completed(object sender, EventArgs e)
        {
            media.Stop();
            media.Play();

                sbElRight.Stop();
                sbLineThick.Stop();
                sbColor.Stop();

                sbElRight.Begin();
                sbLineThick.Begin();
                sbColor.Begin();

            
        }

        private void StoryboardRight_Completed(object sender, EventArgs e)
        {
            media.Stop();
            media.Play();
      

                sbElLeft.Stop();
                sbLineThick.Stop();
                sbColor.Stop(); 

                sbElLeft.Begin();
                sbLineThick.Begin();
                sbColor.Begin();          
        }

        private void panelStatusChecking()
        {
                sbElLeft.Stop();
                sbElRight.Stop();
                sbLineThick.Stop();

                sbElLeft.Begin();
                sbElRight.Begin();
                sbLineThick.Begin();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            buttonStart_Click(new object(), new RoutedEventArgs());
            buttonStart_Click(new object(), new RoutedEventArgs());
            flag4 = true;
        }


        private void radio1_Checked(object sender, RoutedEventArgs e)
        {
            media = media1;
        }

        private void radio2_Checked(object sender, RoutedEventArgs e)
        {
            media = media2;
        }

        private void radio3_Checked(object sender, RoutedEventArgs e)
        {
            media = media3;
        }

        private void radio4_Checked(object sender, RoutedEventArgs e)
        {
            media = media4;
        }
      
        private void radio5_Checked(object sender, RoutedEventArgs e)
        {
            media = media5;
        }

        private void radio6_Checked(object sender, RoutedEventArgs e)
        {
            media = media6;
        }

        private void gridChild1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (gridChild1.ActualHeight > 0)
            {
                flag3 = true;
            }
             if (gridChild1.ActualHeight == 0)
            {
                flag3 = false;
            }
        }

        

        private void win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {
                e.Handled = true;
                OnClosed(new EventArgs());
            }
        }


        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = sliderVolume.Value;
        }

        //-------------------Closing the App----------------------
        private void CloseBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            
            if (MessageBox.Show("Are you sure to close?", "Closing...", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Close();

        }

        private void MinimizeBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
            e.Handled = true;
        }
        
    }
}
