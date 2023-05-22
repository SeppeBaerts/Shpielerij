using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class PowerUp : GameItem
    {
        //This will be a general powerup item; the effect will be different per derrived powerup class;
        public int Seconds { get; set; }
        public bool isActive { get; set; }
        protected object oldValue;
        public Player AffectedPlayer { get; set; }
        public PowerUp(Color col, double left, double top) : base(left, top, 20, 20, false, 0)
        {
            Width = Height = 20;
            ObjectElement = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(col)
            };
            Seconds = 5;
            ObjectElement.MouseDown += ObjectElement_MouseDown;
        }

        private void ObjectElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetOutline();
            SelectedObject.ChangeGameItem(this);
        }

        public virtual void ActivateEffect(Player player)
		{
            AffectedPlayer = player;
            Parent.Children.Remove(ObjectElement);
            TemporaryStorage.PowerUps.Add(this);
            TemporaryStorage.StartPowerup();
        }
        public void Refresh()
        {
            if (Seconds > 0)
                Seconds--;
            else
                EndPowerUp();
        }
        public virtual void EndPowerUp()
        {
            TemporaryStorage.EndPowerUps.Add(this);
        }

	}
}
