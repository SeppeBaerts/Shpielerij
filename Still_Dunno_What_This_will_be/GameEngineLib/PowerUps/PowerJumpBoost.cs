using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameEngineLib.PowerUps
{
    public class PowerJumpBoost : PowerUp
    {
        public PowerJumpBoost(double left, double top) : base(Color.FromRgb(255, 0,0), left, top)
        {

        }
        public override void ActivateEffect(Player player)
        {
            oldValue = player.baseJumpAmount;
            base.ActivateEffect(player);
            player.baseJumpAmount = 25;
        }
        public override void EndPowerUp()
        {
            base.EndPowerUp();
            AffectedPlayer.baseJumpAmount = (int)oldValue;
        }
        public override string ToString()
        {
            return $"PUJB;{base.ToString()}";
        }
    }
}
