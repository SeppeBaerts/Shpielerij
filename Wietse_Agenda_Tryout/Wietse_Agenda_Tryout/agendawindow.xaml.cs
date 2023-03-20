using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Xps.Packaging;

namespace Wietse_Agenda_Tryout
{

    ///To do list:
    ///--Fix the resize things, ze resizen niet wanneer je iets verplaatst/wanneer je iets specefieks doet
    ///werk met de SettingStatic, de settings worden op dit moment niet gebruikt.
    ///Meer in dragbox zetten 
    ///In Dragbox--> minheight zetten, dan voor elke dragbox afgaan
    ///



    /// <summary>
    /// Interaction logic for AgendaWindow.xaml
    /// </summary>
    public partial class AgendaWindow : Window
    {
        StackPanel stakkie;
        bool isShowingContent;
        bool isSetup = true;
        
        public AgendaWindow()
        {
            InitializeComponent();
            SettingStatic.TxtNaam = TxtName;
            SettingStatic.TxtExtraContent = TxtContent;
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


        //Wordt niet gewbruikt
        private void Stack_Drop(object sender, DragEventArgs e)
            //wordt niet gebruikt? 

        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                if (!AgendaCanvas.Children.Contains(element))
                    AgendaCanvas.Children.Add(element);
                AgendaCanvas.Children.Remove(element);
                if (!((StackPanel)sender).Children.Contains(element))
                {
                    ((StackPanel)sender).Children.Add(element);
                }
            }
        }

        private void StackPanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element)
            {
                Canvas.SetLeft(element, e.GetPosition(AgendaCanvas).X);
                Canvas.SetTop(element, e.GetPosition(AgendaCanvas).Y - 15);
                if (!((StackPanel)sender).Children.Contains(element))
                {
                    if (element is Label lbl)
                    {
                        SettingStatic.DragBoxesDictionary[lbl.Uid].ParentStackPanel = (StackPanel)sender;
                        ((StackPanel)sender).Children.Add(element);
                        SetHeight(((StackPanel)sender).Children.IndexOf(lbl));
                    }
                }
            }
        }

        private void SetHeight(int position)
        {
            if (!isSetup)
            {
                bool hasItems = false;
                double maxheight = 0;
                foreach (StackPanel stack in WrapAgenda.Children)
                {
                    if (stack.Children.Count > position)
                    {
                        hasItems = true;
                        ((Label)stack.Children[position]).Height = 150;
                        ((Label)stack.Children[position]).MinHeight = 0;
                        ((Label)stack.Children[position]).Height = double.NaN;

                        if (((Label)stack.Children[position]).ActualHeight > maxheight)
                            maxheight = ((Label)stack.Children[position]).ActualHeight;
                    }
                }
                foreach (StackPanel stack in WrapAgenda.Children)
                {
                    if (stack.Children.Count > position)
                    {
                        ((Label)stack.Children[position]).MinHeight = maxheight;
                    }
                }
                if (hasItems)
                    ((Label)StackUren.Children[position]).MinHeight = maxheight;
                else
                    ((Label)StackUren.Children[position]).MinHeight = 50;

            }
        }

        private void StackPanel_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.Serializable) is UIElement element && e.KeyStates == DragDropKeyStates.LeftMouseButton)
            {
                ((StackPanel)sender).Children.Remove(element);
                SetHeight(((StackPanel)sender).Children.Count);
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("../../Agenda.txt"))
            {
                if (isShowingContent)
                {
                    ResetContent();
                }
                foreach (StackPanel stack in WrapAgenda.Children)
                {
                    sw.Write($"{stack.Name};");
                    for (int i = 0; i < stack.Children.Count; i++)
                    {
                        if (stack.Children[i] is Label lbl && ((lbl.Content is string st && !string.IsNullOrEmpty(st)) || (lbl.Content is TextBlock txb)))
                        {
                            sw.Write($"{(lbl.Content is string s? s : ((TextBlock)lbl.Content).Text )};{lbl.Tag};");
                        }
                    }
                    sw.WriteLine();

                }
                if (isShowingContent)
                    AddContent();
            }
            MessageBox.Show("Content saved");
        }

        private void GetStackPanel(string s)
        {
            for (int i = 0; i < WrapAgenda.Children.Count; i++)
                if (WrapAgenda.Children[i] is StackPanel stack && stack.Name == s)
                    stakkie = stack;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Agenda();
            isSetup = false;
            if((bool)SettingStatic.GetSetting("::Show content on startup"))
                ToggleShow();
        }

        private void Load_Agenda()
        {
            using (StreamReader sr = new StreamReader("../../Agenda.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string zin = sr.ReadLine();
                    string[] dingen = zin.Split(';');
                    string content = dingen.Length > 4? dingen[4] : null;
                    foreach (string s in dingen)
                    {
                        if (s.Contains("Stack"))
                        {
                            GetStackPanel(s);
                        }
                        else if ((!(s == "Maandag" || s == "Dinsdag" || s == "Woensdag" || s == "Donderdag" || s == "Vrijdag" || s == content)) && !string.IsNullOrWhiteSpace(s))
                        {
                            DragBox box = new DragBox(s, stakkie, content);
                            box.DraggingBox.MouseDoubleClick += DraggingBox_MouseDoubleClick;
                            box.Blokkie.SizeChanged += Blokkie_SizeChanged;
                            SettingStatic.Dragboxes.Add(box);
                        }
                    }
                }
            }
        }

        private void Blokkie_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Label lbl = (Label)((TextBlock)sender).Parent;
            SetHeight(((StackPanel)lbl.Parent).Children.IndexOf(lbl));
        }

        private void DraggingBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label lbl = ((Label)sender);
            if (SettingStatic.CurrentLable != null)
            {
                SaveLabel();
            }
            if (SettingStatic.TxtNaam != null)
            {
                SettingStatic.TxtNaam.Text = SettingStatic.DragBoxesDictionary[lbl.Uid].CardText;
                SettingStatic.TxtExtraContent.Text = SettingStatic.DragBoxesDictionary[lbl.Uid].CardContent;
                SettingStatic.CurrentLable = lbl;
            }
        }

        private void SaveLabel()
        {
            Label lbt = SettingStatic.CurrentLable;
            DragBox drag = SettingStatic.DragBoxesDictionary[lbt.Uid];
            lbt.Tag = TxtContent.Text;
            ((TextBlock)lbt.Content).Text = SettingStatic.TxtNaam.Text;
            drag.CardContent = TxtContent.Text;
            drag.CardText = TxtName.Text;
            if (isShowingContent)
                drag.Blokkie.Text += $"\n{drag.CardContent}";
            SetHeight(drag.ParentStackPanel.Children.IndexOf(lbt));
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            isSetup = true;
            Load_Agenda();
            isSetup= false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < WrapAgenda.Children.Count; i++)
            {
                if (WrapAgenda.Children[i] is StackPanel stack)
                {
                    for (int j = 0; j < stack.Children.Count; j++)
                    {
                        if (stack.Children[j] is Label lbl && lbl.Content is TextBlock txb)
                            stack.Children.Remove(lbl);
                    }
                }
            }
            ResetHeight();
        }

        private void ResetHeight()
        {
            for (int i = 0; i < 7; i++)
            {
                SetHeight(i);
            }
        }

        private void ToggleShow()
        {
            if (!isShowingContent)
            {
                AddContent(); //Moet functie nog even bekijken.
            }
            else
            {
                ResetContent();
                ResetHeight();
            }
            isShowingContent = !isShowingContent;
        }

        private void ResetContent()
        {
            foreach(DragBox drag in SettingStatic.Dragboxes)
            {
                drag.Blokkie.Text = drag.CardText;
            }
        }

        private void AddContent()
        {
            foreach (DragBox drag in SettingStatic.Dragboxes)
            {
                drag.Blokkie.Text += $"\n{drag.CardContent}";
            }
        }

        private void MnuSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }

        private void PrintCanvas(Canvas canvas)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                // Create a new PrintVisual with the canvas as its visual.
                // This will allow us to print the canvas as a visual element.
                //PrintVisual visual = new PrintVisual(canvas);
                //printDlg.PrintVisual(AgendaCanvas, "mslqkdjfqms");

                //// Get the print capabilities of the printer.
                //PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                //// Calculate the scale factor to fit the visual on the page.
                //double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / visual.VisualSize.Width,
                //                        capabilities.PageImageableArea.ExtentHeight / visual.VisualSize.Height);

                //// Apply the scale factor to the visual.
                //visual.Transform = new ScaleTransform(scale, scale);

                // Send the visual to the printer.
                printDlg.PrintVisual(AgendaCanvas, "Print Canvas");
            }
        }

        private void MnuPrint_Click(object sender, RoutedEventArgs e)
        {
            // Create the print dialog object and set options
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;
            if (pDialog.ShowDialog() == true)
            {
                pDialog.PrintVisual(AgendaCanvas, "Print canvas");
            }

            //// Display the dialog. This returns true if the user presses the Print button.
            //Nullable<Boolean> print = pDialog.ShowDialog();
            //if (print == true)
            //{
                
            //    XpsDocument xpsDocument = new XpsDocument("C:\\Users\\12202625\\offline documenten\\Shpielerij\\CSharpShpielerij\\Wietse_Agenda_Tryout\\Wietse_Agenda_Tryout\\bin\\Debug\\doc.xps", FileAccess.ReadWrite);
            //    pDialog.PrintDocument(xpsDocument.GetFixedDocumentSequence().DocumentPaginator, "Test print job");
            //}

        }

        private void MnuScan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveLabel();
            }
        }

        private void MnuShow_Click(object sender, RoutedEventArgs e)
        {
            ToggleShow();
        }


    }
}
