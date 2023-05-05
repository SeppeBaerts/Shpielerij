using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class Player : GameItem
    {
        public override Canvas Parent { 
            get => base.Parent;
            set {
                base.Parent = value;
            }
        }
        public char Direction { get; set; }
        public bool HasHealth { get; set; } //Checks if the player is health-Based
        public bool IsDead { get; set; }
        public int Health { get; set; }
        private bool isRealGame;
        private int jumpAmount;
        public Player(double left, double bottom, int width, int height, int movementSpeed, bool hasHealth, bool isActive = false) : base(left, bottom, width, height, movementSpeed)
        {
            HasHealth = hasHealth;
            isRealGame = isActive; //Do i need this? bcz MovePlayer should only be called on when it's with a dispatcher timer? 
            jumpAmount = 0;
            ObjectElement = new Ellipse()
            {
                Width = width,
                Height = height,
                Fill = Brushes.Black,
                ContextMenu = CMenu,
            };
            isRealGame = isActive;
            ObjectElement.MouseDown += ObjectElement_MouseDown;
        }

        private void ObjectElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetOutline(false);
            SelectedObject.ChangeGameItem(this);
        }
        private bool isJumping = false;
        /// <summary>
        /// Only call this method in a dispatcher timer
        /// </summary>
        public void MovePlayer()
        {
            TemporaryStorage.HasCollided(OutlineRect);
            char blockedUD = TemporaryStorage.BlockedUD;
            char blockedLR = TemporaryStorage.BlockedLR;
            if (TemporaryStorage.PlayerMoves)
            {
                if (Direction == 'L' && blockedLR != 'L') Canvas.SetLeft(ObjectElement, Canvas.GetLeft(ObjectElement) - MovementSpeed);
                else if (Direction == 'R' && blockedLR !='R') Canvas.SetLeft(ObjectElement, Canvas.GetLeft(ObjectElement) + MovementSpeed);
                else if (Direction == 'D' && blockedUD != 'D') Canvas.SetTop(ObjectElement, Canvas.GetTop(ObjectElement) + MovementSpeed);
                else if (Direction == 'U' && blockedUD != 'U') Canvas.SetTop(ObjectElement, Canvas.GetTop(ObjectElement) - MovementSpeed);
            }
            else
            {
                if (Direction == 'L' && blockedLR != 'L') TemporaryStorage.UpdateLayout(MovementSpeed);
                else if (Direction == 'R' && blockedLR != 'R') TemporaryStorage.UpdateLayout(-MovementSpeed);
            }
            if (isJumping && jumpAmount <= 0)
            {
                if (jumpAmount > -15)
                    jumpAmount--;
                else
                    isJumping = false;
            }
            if (HasGravity && jumpAmount <= 0 && Canvas.GetTop(ObjectElement) <= Parent.ActualHeight - Height && blockedUD != 'D')
            {
                Canvas.SetTop(ObjectElement, Canvas.GetTop(ObjectElement) + MovementSpeed);
            }
            else if (HasGravity && jumpAmount > 0)
            {
                if (blockedUD != 'U')
                    Canvas.SetTop(ObjectElement, Canvas.GetTop(ObjectElement) - MovementSpeed);
                else
                    jumpAmount = 0;
                jumpAmount--;
            }
            UpdateRect();
        }
        public void Jump()
        {
            if (!isJumping)
            {
                jumpAmount = 15;
                isJumping = true;
            }
        }
        public override string ToString()
        {
            return $"PP;{base.ToString()}";
        }
    }
}
