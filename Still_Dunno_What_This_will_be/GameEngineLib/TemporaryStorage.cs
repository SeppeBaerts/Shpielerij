using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public static Rect EndPoint;
        public static bool HasCompleted;
        public static List<Rect> GameRects = new List<Rect>();
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
            if (EndPoint.IntersectsWith(detectableItem))
            {
                HasCompleted = true;
                return true;
            }
            foreach (GameItem item in Items)
            {
                if (!(item is Player) && item.OutlineRect.IntersectsWith(detectableItem))
                    return true;

            }
            //foreach (Rect rect in GameRects)
            //{
            //    if (rect != detectableItem && rect.IntersectsWith(detectableItem))
            //    { return true; }
            //}
            return false;
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
                item.MoveItem();
        }
    }
}
