using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class GameItem 
    {
        private readonly string[] contextOptions =
        {
            "Height",
            "Width",
            "(bool)Collision",
        };
        private bool isChanging;
        private Canvas parent;
        public virtual Canvas Parent
        {
            get { return parent; }
            set {
                parent = value;
                Canvas.SetLeft(ObjectElement, Left);
                Canvas.SetTop(ObjectElement, Top);
                parent.Children.Add(ObjectElement);
                if (!parent.Children.Contains(line) && line != null)
                    parent.Children.Add(line);
            }
        }
        public double ActualLeft
        {
            get { return ObjectElement == null? Left : Canvas.GetLeft(ObjectElement); }
        }
        public double ActualTop
        {
            get { return ObjectElement == null? Top : Canvas.GetTop(ObjectElement); }
        }
        public double Left { get; set; } 
        public double Top { get; set; }
        private int width;
        public int Width
        {
            get { return width; }
            set {
                width = value;
                if (ObjectElement != null)
                    ((Shape)ObjectElement).Width = value;
                if (OutlineRect != null)
                    OutlineRect.Width = value;
            }
        }
        private int height;
        public int Height
        {
            get { return height; }
            set { 
                height = value;
                if (ObjectElement!= null)
                    ((Shape)ObjectElement).Height = value;
                if (OutlineRect != null)
                    OutlineRect.Height = value;
            }
        }
        public UIElement ObjectElement { get; set; }
        private int movingAmount;
        public int MovingAmount {
            get { return movingAmount; }
            set
            {
                movingAmount = value;
                if (movingAmount != 0 && (LevelController.IsCreateWindow || LevelController.ShowDevOutlines))
                    CreatePath();
            }
        }
        public int MovementSpeed { get; set; }
        public bool IsEmpty { get; private set; }
        private bool hasCollisionDetection;
        public bool HasCollisionDetection
        {
            get { return hasCollisionDetection; }
            set
            {
                hasCollisionDetection = value;
                if (!(this is Player))
                {
                    if (hasCollisionDetection)
                    {
                        OutlineRect = new Rect
                        {
                            Width = Width,
                            Height = Height,
                            Location = new Point(Left, Top),
                        };
                    }
                }
            }
        }
        internal bool isGoingLeft;
        public bool HasGravity { get; set; }
        public ContextMenu CMenu { get; set; }
        public Rect OutlineRect;
        public GameItem(double left, double top, int width, int height, int movementSpeed = 5, bool hasCollisionDetection = true, int movementAmount = 0, bool hasGravity = true)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            MovementSpeed = movementSpeed;
            HasCollisionDetection = hasCollisionDetection;
            HasGravity = hasGravity;
            MovingAmount = movementAmount;
            CMenu = new ContextMenu();

            foreach (string st in contextOptions)
            {
                MenuItem mnu = new MenuItem();
                mnu.Header = st.Replace("(bool)", "");
                if (st.StartsWith("(bool)"))
                {
                    mnu.IsCheckable = true;
                    mnu.Checked += Mnu_Checked;
                }
                else
                    mnu.Click += Mnu_Click;
                CMenu.Items.Add(mnu);
            }
            MenuItem del = new MenuItem();
            del.Header = "Delete";
            del.Click += Del_Click;
            CMenu.Items.Add(del);
            MenuItem dup = new MenuItem();
            dup.Header = "Duplicate";
            dup.Click += Dup_Click;
            CMenu.Items.Add(dup);
            if (hasCollisionDetection)
            {
                OutlineRect = new Rect
                {
                    Width = width,
                    Height = height,
                    Location = new Point(left, top),
                };
            }
            TemporaryStorage.Items.Add(this);
        }

        internal virtual void Dup_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            TemporaryStorage.Items.Remove(this);
            Parent.Children.Remove(ObjectElement);
        }

        private void Mnu_Checked(object sender, RoutedEventArgs e)
        {
            ChangeProperty(((MenuItem)sender).Header.ToString(), ((MenuItem)sender).IsChecked);
        }

        private void Mnu_Click(object sender, RoutedEventArgs e)
        {
            if (!isChanging)
            {
                CreateTextbox(((MenuItem)sender).Header.ToString());
                isChanging = true;
            }
        }
        internal void CreateRect()
        {
            OutlineRect = new Rect
            {
                Width = width,
                Height = height,
                Location = new Point(ActualLeft, ActualTop),
            };
        }
        internal void UpdateRect()
        {
            if (HasCollisionDetection && OutlineRect != null)
            {
                OutlineRect.Location = new Point(ActualLeft, ActualTop);
                if (this is GameItemEndPoint) TemporaryStorage.EndRect = OutlineRect; 
            }
        }

        private void CreateTextbox(string textboxTag)
        {
            TextBox txt = new TextBox
            {
                Tag = textboxTag,
            };
            Canvas.SetLeft(txt, Canvas.GetLeft(ObjectElement) + (Width/2));
            Canvas.SetTop(txt, Canvas.GetTop(ObjectElement) + (Height/2) );
            Parent.Children.Add(txt);
            txt.Focus();
            txt.KeyDown += Txt_KeyDown;
        }

        private void Txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (int.TryParse(((TextBox)sender).Text, out int value))
                {
                    ChangeProperty((string)((TextBox)sender).Tag, value);
                    Parent.Children.Remove((TextBox)sender);
                    isChanging = false;
                }
                else MessageBox.Show("Something went wrong", "Well, this is awkward");
            }
        }
        private void ChangeProperty(string prop, object value)
        {
            switch(prop.ToLower())
            {
                case "height": Height = (int)value; break;
                case "width": Width = (int)value; break;
                case "collision": HasCollisionDetection = (bool)value; break;
            }
        }

        public GameItem()
        {
            IsEmpty = true;
        }
        public override string ToString()
        {
            return $"{Canvas.GetLeft(ObjectElement)};{Canvas.GetTop(ObjectElement)};{((Shape)ObjectElement).ActualHeight};{((Shape)ObjectElement).ActualWidth};{MovementSpeed};=={(hasCollisionDetection? 1:0)};{MovingAmount}";
        }
        public virtual void ClearOutline()
        {
            ((Shape)ObjectElement).Stroke = null;
        }
        public virtual void SetLocation(Point location)
        {
            Canvas.SetLeft(ObjectElement, location.X - (((Shape)ObjectElement).ActualWidth/2));
            Canvas.SetTop(ObjectElement, location.Y - (((Shape)ObjectElement).ActualHeight/2));
            UpdateRect();
            CreatePath();
        }
        #region movingObject
        //negative for moving to left, positive for moving to right.
        public void MoveBy(int x, int y = 0)
        {
            Canvas.SetLeft(ObjectElement, Canvas.GetLeft(ObjectElement) + x);
            Canvas.SetTop(ObjectElement , Canvas.GetTop(ObjectElement) + y);
            UpdateRect();
        }

        internal int moveDaBody = 0;
        public void MoveItem()
        {
            if (MovingAmount != 0)
            {
                if (moveDaBody >= Math.Abs(MovingAmount))
                {
                    isGoingLeft = !isGoingLeft;
                    moveDaBody = 0;
                    MovementSpeed = -MovementSpeed;
                }
                Canvas.SetLeft(ObjectElement, Canvas.GetLeft(ObjectElement) + MovementSpeed);
                moveDaBody += Math.Abs(MovementSpeed);
                UpdateRect();
            }
        }
        private Line line;
        internal void CreatePath(bool isStatic = false)
        {
            if (LevelController.IsCreateWindow)
                RemovePath();

            if (LevelController.IsCreateWindow || LevelController.ShowDevOutlines)
            {
                line = new Line()
                {
                    X1 = 0,
                    X2 = MovingAmount,
                    Fill = Brushes.Black,
                    Stroke = Brushes.Black,
                };
                Canvas.SetTop(line, ActualTop + (height / 2));
                Canvas.SetLeft(line, ActualLeft + (width / 2));
                Parent?.Children.Add(line);
            }
        }
        internal void RemovePath()
        {
            if (line != null) 
                Parent.Children.Remove(line);
            line = null;
        }

        public virtual void SetLocation(int x, int y)
        {
            throw new NotImplementedException();
        }
        #endregion
        internal void SetOutline(bool blackOutline = true)
        {
            ((Shape)ObjectElement).Stroke = blackOutline ? Brushes.Black : Brushes.Gray;
        }
        #region GameItemAction
        #endregion
    }
}
