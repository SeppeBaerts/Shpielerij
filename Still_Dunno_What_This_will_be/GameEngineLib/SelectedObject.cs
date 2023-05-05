using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineLib
{
    public static class SelectedObject
    {
        static GameItem gameObject;
        public static bool JustChanged {  get; 
            set; }

        public static void ChangeGameItem(GameItem item)
        {
            if (gameObject == null)
                gameObject = item;
            else
            {
                gameObject.ClearOutline();
                gameObject = item;
            }
            JustChanged = true;
        }
        public static void ClearGameOutline()
        {
            gameObject?.ClearOutline();
            gameObject = null;
        }
        public static GameItem GetSelectedGameItem()
        {
            return gameObject;
        }
        public static void GetPropertys(out List<string> propertyNames,out List<object> propValues)
        {

            propValues = new List<object>();
            propertyNames = new List<string>()
            {
                "Width",
                "Height",
                "Collision detection",
                "Speed",
                "Gravity",
                "Amount movement"
            };
            propValues.Add(gameObject.Width); 
            propValues.Add(gameObject.Height);
            propValues.Add(gameObject.HasCollisionDetection);
            propValues.Add(gameObject.MovementSpeed);
            propValues.Add(gameObject.HasGravity);
            propValues.Add(gameObject.MovingAmount);
            if (gameObject is Player player)
            {
                propertyNames.Add("Has health");                
                propertyNames.Add("health");
                propValues.Add(player.HasHealth);
                propValues.Add(player.Health);
            }

        }
        public static bool HasSelectedGameItem()
        {
            return gameObject != null;
        }
    }
}
