using ClassLibAnimation;
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

namespace AnimationTryout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AnimationFade animFade;
        AnimationText animText;
        AnimationPulse animPulse;
        AnimationText animTextUselessButton;
        AnimationPulse animSmallPulse;
        public MainWindow()
        {
            InitializeComponent();

            animFade = new AnimationFade(1, RectFade);
            animPulse = new AnimationPulse(2, BtnPulse,3);
        }

        private void BtnFade_Click(object sender, RoutedEventArgs e)
        {
            animFade.Start();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            animText?.Stop();
            animText = new AnimationText(2, LblChange, TxtTarget.Text);
            animText.Start();
        }

        private void BtnPulse_Click(object sender, RoutedEventArgs e)
        {
            animSmallPulse?.Stop();
            animPulse.Start();
        }

        private void BtnTest_Useless_MouseEnter(object sender, MouseEventArgs e)
        {
            animTextUselessButton?.Stop();
            animTextUselessButton = new AnimationText(3, (Button)sender, "You can click me \nBut i will not do anything.");
            animTextUselessButton.Start();
        }

        private void BtnTest_Useless_MouseLeave(object sender, MouseEventArgs e)
        {
            animTextUselessButton?.Stop();
            animTextUselessButton = new AnimationText(1, (Button)sender, "Try to hover me!");
            animTextUselessButton.Start();
        }

        private void BtnPulse_MouseEnter(object sender, MouseEventArgs e)
        {
            Button sendButton = sender as Button;
            animSmallPulse?.Stop();
            animSmallPulse = new AnimationPulse(1, sendButton, 2, 1.12);
            animSmallPulse.Start();
        }
    }
}
