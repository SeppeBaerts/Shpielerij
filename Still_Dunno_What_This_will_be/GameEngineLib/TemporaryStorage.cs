using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace GameEngineLib
{
    public static class TemporaryStorage
    {
        /// <summary>
        /// -1 = true, -2 = false, positive numbers mean ints;
        /// </summary>
        private static List<GameItem> temporaryItemHolder = new List<GameItem>();
        public static bool PlayerMoves { get; set; } = true;
        public static List<int> Antwoorden { get; set; }
        public static List<GameItem> Items = new List<GameItem>();
        public static GameItemEndPoint BigEnd;
        public static Rect EndRect;
        public static Rect GameOverRect;
        public static bool HasCompleted;
        public static bool HasDied;
        public static char BlockedLR { get; private set; } = '\0';
        public static char BlockedUD { get; private set; } = '\0';
        //Tells the GameWindow which direction is Getting blocked. 
        public static bool ToBool(int itemIndex)
        {
            return Antwoorden[itemIndex] == -1;
        }
        public static void UpdateLayout(int x, int y = 0)
        {
            foreach (GameItem item in Items)
            {
                if (!(item is Player))
                    item.MoveBy(x, y);
            }
        }
        public static bool HasCollided(Rect detectableItem)
        {
            if (EndRect.IntersectsWith(detectableItem))
            {
                HasCompleted = true;
                return true;
            }
            else if (GameOverRect.IntersectsWith(detectableItem))
            {
                HasDied = true;
                return true;
            }
            foreach (GameItem item in Items)
            {
                if (!(item is Player) && item.OutlineRect.IntersectsWith(detectableItem))
                {
                    GetCollision(detectableItem, item.OutlineRect);
                    return true;
                }
            }
            BlockedLR = BlockedUD = '0';
            return false;
        }
        private static void GetCollision(Rect rect1, Rect rect2)
        {
            BlockedLR = BlockedUD = '0';
            if (rect1.Right >= rect2.Left && rect1.Right <= rect2.Left+15)
            {
                // The intersection is from the left
                BlockedLR = 'R';
            }
            else if (rect1.Left >= rect2.Right - 15 && rect1.Left <= rect2.Right +15)
            {
                // The intersection is from the right
                BlockedLR = 'L';
            }
            if (rect1.Bottom >= rect2.Top && rect1.Bottom <= rect2.Top +15)
            {
                // The intersection is from the Top
                BlockedUD = 'D';
            }
            else if (rect1.Top >= rect2.Bottom - 15 && rect1.Top <= rect2.Bottom+15)
            {
                // The intersection is from the Bottom
                BlockedUD = 'U';
            }
        }
        public static void Clear()
        {
            temporaryItemHolder.Clear();
            foreach(GameItem item in Items)
                temporaryItemHolder.Add(item);
            Items.Clear();
        }
        public static void Revert()
        {
            Items.Clear();
            foreach (GameItem item in temporaryItemHolder)
                Items.Add(item);
        }
        public static void Move()
        {
            foreach (GameItem item in Items)
                if (item.MovingAmount != 0)
                    item.MoveItem();
        }
    }
}
