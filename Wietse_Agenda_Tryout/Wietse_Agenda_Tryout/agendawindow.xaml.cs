using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wietse_Agenda_Tryout
{
    /// <summary>
    /// Interaction logic for AgendaWindow.xaml
    /// </summary>
    public partial class AgendaWindow : Window
    {
        public AgendaWindow()
        {
            InitializeComponent();
            
        }
        private void AgendaCanvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                Canvas.SetLeft(element, e.GetPosition(AgendaCanvas).X);
                Canvas.SetTop(element, e.GetPosition(AgendaCanvas).Y - 15);
                if(!AgendaCanvas.Children.Contains(element))
                    AgendaCanvas.Children.Add(element);
            }
        }

        private void AgendaCanvas_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                AgendaCanvas.Children.Remove(element);
            }
        }

        private void Stack_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                if (!AgendaCanvas.Children.Contains(element))
                    AgendaCanvas.Children.Add(element);
                AgendaCanvas.Children.Remove(element);
                if(!((StackPanel)sender).Children.Contains(element))
                    ((StackPanel)sender).Children.Add(element);
            }
        }

        private void StackPanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                Canvas.SetLeft(element, e.GetPosition(AgendaCanvas).X);
                Canvas.SetTop(element, e.GetPosition(AgendaCanvas).Y - 15);
                if (!((StackPanel)sender).Children.Contains(element))
                    ((StackPanel)sender).Children.Add(element);
            }
        }

        private void StackPanel_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element && e.KeyStates == DragDropKeyStates.LeftMouseButton)
            {
                ((StackPanel)sender).Children.Remove(element);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach(StackPanel stack in WrapAgenda.Children)
            {
                for (int i =0; i <stack.Children.Count; i++)
                {
                    if (stack.Children[i] is TextBlock block)
                    {
                        //make string like StackMaandag; Hier de Taak
                        //dan kun je een substring gebruiken, en dan if stack.naam = substring[0]
                    }
                }
            }
        }
    }
}
