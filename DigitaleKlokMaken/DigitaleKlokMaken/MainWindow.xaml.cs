using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DigitaleKlokMaken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Path> allPaths;
        List<Path> A0;
        List<Path> A1;
        List<Path> A2;
        List<Path> B0;
        List<Path> B1;
        List<Path> B2;
        List<Path> B3;
        List<Path> B4;
        List<Path> B5;
        List<Path> B6;
        List<Path> B7;
        List<Path> B8;
        List<Path> B9;        
        List<Path> C0;
        List<Path> C1;
        List<Path> C2;
        List<Path> C3;
        List<Path> C4;
        List<Path> C5;
        List<Path> C6;
        List<Path> C7;
        List<Path> C8;
        List<Path> C9;
        List<Path> D0;
        List<Path> D1;
        List<Path> D2;
        List<Path> D3;
        List<Path> D4;
        List<Path> D5;
        List<Path> D6;
        List<Path> D7;
        List<Path> D8;
        List<Path> D9;
        BrushConverter br = new BrushConverter();
        SolidColorBrush colorDisabled;
        SolidColorBrush colorEnabled;
        Effect pathEffect;
        DispatcherTimer timer;
        bool isactive;
        string currentTime;

        public MainWindow()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0,0,1);
            timer.Start();
            pathEffect = new BlurEffect();
            InitializeComponent();
            colorDisabled = (SolidColorBrush)br.ConvertFromString("#FF5F5006");
            colorEnabled = (SolidColorBrush)br.ConvertFromString("#FFFDFF00");
            allPaths= new List<Path> { L1,L2,L3,L4,L5,L6,L7,L8,L9,L10,L11,L12,L13, L14, R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, R13, R14 };
            A0 = new List<Path> { L3 };
            A1 = new List<Path> { L1, L3, L4, L6, L7 };
            A2 = new List<Path> { L4, L5 };
            B0 = new List<Path> { L10 };
            B1 = new List<Path> { L8, L10, L11, L13, L14 };
            B2 = new List<Path> { L11, L12 };
            B3 = new List<Path> { L11, L14 };
            B4 = new List<Path> { L8, L13, L14 };
            B5 = new List<Path> { L9, L14 };
            B6 = new List<Path> { L9 };
            B7 = new List<Path> { L10,L11, L13, L14 };
            B8 = new List<Path>();
            B9 = new List<Path> {L14 };
            C0 = new List<Path> { R3 };
            C1 = new List<Path> { R1, R3, R4, R6, R7 };
            C2 = new List<Path> { R4, R5 };            
            C3 = new List<Path> { R4, R7 };
            C4 = new List<Path> { R1, R6, R7 };
            C5 = new List<Path> { R2, R7 };
            C6 = new List<Path> { R2 };
            C7 = new List<Path> { R4, R3,R7,R6 };
            C8 = new List<Path>();
            C9 = new List<Path> { R7 };
            D0 = new List<Path> { R10 };
            D1 = new List<Path> { R8, R10, R11, R13, R14 };
            D2 = new List<Path> { R11, R12 };
            D3 = new List<Path> { R11, R14 };
            D4 = new List<Path> { R8, R13, R14 };
            D5 = new List<Path> { R9, R14 };
            D6 = new List<Path> { R9 };
            D7 = new List<Path> { R10, R11, R13, R14 };
            D8 = new List<Path>();
            D9 = new List<Path> { R14 };
            SetPath(A1);
            GetTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            PointUpper.Fill = PointLower.Fill = isactive? colorDisabled: colorEnabled;
            PointUpper.Effect = PointLower.Effect = isactive? null: pathEffect;
            PointUpper.Opacity = PointLower.Opacity = isactive ? 0.3 : 1;
            if (currentTime != DateTime.Now.ToShortTimeString()) GetTime();
            isactive = !isactive;
            timer.Start();
        }

        public void SetPath(List<Path> li)
        {
            foreach(Path path in li)
            {
                path.Fill = colorDisabled;
                path.Effect = null;
                path.Opacity = 0.3;
            }
        }
        public void GetTime()
        {
            foreach (Path path in allPaths)
            {
                path.Fill = colorEnabled;
                path.Effect = pathEffect;
                path.Opacity = 1;
            }
            string st = DateTime.Now.ToShortTimeString();
            st = st.PadLeft(5, '0');
            char hour1 = st.First();
            char hour2 = char.Parse(st.Substring(1, 1));
            char minute = char.Parse(st.Substring(3, 1));
            char minute2 = char.Parse(st.Substring(4, 1));
            switch (hour1)
            {
                case '0':
                    SetPath(A0);
                    break;
                case '1': 
                    SetPath(A1);
                    break;
                case '2':
                    SetPath(A2);
                    break;
            }
            switch (hour2)
            {
                case '0':
                    SetPath(B0);
                    break;
                case '1':
                    SetPath(B1);
                    break;
                case '2':
                    SetPath(B2);
                    break;
                case '3':
                    SetPath(B3);
                    break;
                case '4':
                    SetPath(B4);
                    break;
                case '5':
                    SetPath(B5);
                    break;
                case '6':
                    SetPath(B6);
                    break;
                case '7':
                    SetPath(B7);
                    break;
                case '8':
                    SetPath(B8);
                    break;
                case '9':
                    SetPath(B9);
                    break;
            }
            switch (minute)
            {
                case '0':
                    SetPath(C0);
                    break;
                case '1':
                    SetPath(C1);
                    break;
                case '2':
                    SetPath(C2);
                    break;
                case '3':
                    SetPath(C3);
                    break;
                case '4':
                    SetPath(C4);
                    break;
                case '5':
                    SetPath(C5);
                    break;
                case '6':
                    SetPath(C6);
                    break;
                case '7':
                    SetPath(C7);
                    break;
                case '8':
                    SetPath(C8);
                    break;
                case '9':
                    SetPath(C9);
                    break;
            }
            switch (minute2)
            {
                case '0':
                    SetPath(D0);
                    break;
                case '1':
                    SetPath(D1);
                    break;
                case '2':
                    SetPath(D2);
                    break;
                case '3':
                    SetPath(D3);
                    break;
                case '4':
                    SetPath(D4);
                    break;
                case '5':
                    SetPath(D5);
                    break;
                case '6':
                    SetPath(D6);
                    break;
                case '7':
                    SetPath(D7);
                    break;
                case '8':
                    SetPath(D8);
                    break;
                case '9':
                    SetPath(D9);
                    break;
            }
        }
    }
}
