using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassLibAnimation
{
    public class AnimationText : Animation<string>
    {
        private static char[] Alphabeth => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890/*-+".ToCharArray();
        private int _index;
        private Random rnd = new Random();
        private char RandomChar => Alphabeth[rnd.Next(Alphabeth.Length)];
        private int toGo;
        private List<char> wordScramble = new List<char>();
        public AnimationText(int time, UIElement animationObject, string newText) : base(time, animationObject)
        {
            targetValue = newText;
            _index = 0;
            wordScramble = GetText();
            if (wordScramble.Count > newText.Length)
                wordScramble = newText.Substring(2).ToList();
            step = Math.Floor((double)(FPS * time) / targetValue.Length);
            toGo = (int)step;
        }
        internal override void Clock_Tick(object sender, EventArgs e)
        {
            clock.Stop();
            toGo--;
            if (toGo == 0 && _index != targetValue.Length)
            {
                wordScramble[_index] = targetValue[_index];
                if (targetValue.Length > wordScramble.Count)
                    wordScramble.Add('0');
                _index++;
                toGo = (int)step;
            }

            if (toGo > 0 &&_index != targetValue.Length)
            {
                for (int i = _index; i < wordScramble.Count; i++)
                    wordScramble[i] = RandomChar;
                SetText(new string(wordScramble.ToArray()));
                clock.Start();
            }
            else
            {
                SetText(new string(wordScramble.ToArray()));
                _index = 0;
            }
        }
        private void SetText(string text)
        {
            if (AnimationObject is TextBox txt)
                txt.Text = text;
            else if (AnimationObject is TextBlock txb)
                txb.Text = text;
            else if (AnimationObject is Label lbl)
                lbl.Content = text;
            else if (AnimationObject is Button btn)
                btn.Content = text;
        }
        private List<char> GetText()
        {
            if (AnimationObject is TextBox txt)
                return txt.Text.ToList();
            else if (AnimationObject is TextBlock txb)
                return txb.Text.ToList();
            else if (AnimationObject is Label lbl)
                return lbl.Content.ToString().ToList();
            else if (AnimationObject is Button btn)
                return btn.Content.ToString().ToList();
            else throw new ArgumentOutOfRangeException("This UiElement does not have a text property or it is not yet implemented.");
        }
    }
}
