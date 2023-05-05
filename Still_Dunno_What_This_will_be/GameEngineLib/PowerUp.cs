using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameEngineLib
{
    public class PowerUp : GameItem
    {
		//This will be a general powerup item; the effect will be different per derrived powerup class;
        public PowerUp()
        {
			throw new NotImplementedException();
        }
        public virtual void ActivateEffect(Player player)
		{
			throw new NotImplementedException();
		}

	}
}
