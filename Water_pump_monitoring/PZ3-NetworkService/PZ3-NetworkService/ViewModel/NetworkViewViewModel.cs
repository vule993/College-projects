using PZ3_NetworkService.Model;
using PZ3_NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PZ3_NetworkService.ViewModel
{
    class NetworkViewViewModel : BindableBase
    {
        private static Element draggedItem = null;
        private static Element selectedItem = null;
        private static bool dragging = false;

        public bool visible = false;
        public bool Visible { get { return visible; } set {
                if(visible != value)
                {
                    visible = value;
                    OnPropertyChanged("Visible");
                }
               
            }
        }

        static String emptymessage = "No elements available.";


        public static String EmptyMessage { get { return emptymessage; } set { emptymessage = value; } }
        public static bool Dragging { get { return dragging; } set { dragging = value; } }
        public static Element DraggedItem { get { return draggedItem; } set { draggedItem = value; } }
        public static Element SelectedItem { get { return selectedItem; } set { selectedItem = value; } }

        public ObservableCollection<Element> AllElements { get; set; }
        
        
        
        
        //pamtimo ova sranja
        //public static Dictionary<Canvas, Element> CanvasGrid = new Dictionary<Canvas, Element>();





        public MyCommand<ListView> SelectionChangedCommand { get; set; }
        public MyCommand<ListView> MouseLeftButtonUpCommand { get; set; }
        public MyCommand<Canvas> DropCommand { get; set; }
        public MyCommand<Canvas> RemoveCommand { get; set; }

        public NetworkViewViewModel()
        {
            MainWindowViewModel.CanvasGrid.Clear();
            /**/
            
            AllElements = new ObservableCollection<Element>();
            foreach (Element e in NetworkDataViewModel.AllElements)
            {
                AllElements.Add(e);
            }
            SelectionChangedCommand = new MyCommand<ListView>(SelectionChanged);
            MouseLeftButtonUpCommand = new MyCommand<ListView>(MouseLeftButtonUp);
            DropCommand = new MyCommand<Canvas>(Drop);
            RemoveCommand = new MyCommand<Canvas>(Remove);
            if (AllElements.Count() < 1)
            {
                Visible = true;
            }
            
        }
        private void SelectionChanged(ListView t)
        {
            if (!Dragging)
            {
                Dragging = true;
                //
                DraggedItem = (Element)t.SelectedItem;
                t.SelectedItem = null;
                SelectedItem = null;

                DragDrop.DoDragDrop(t, DraggedItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void MouseLeftButtonUp(ListView t)
        {
            DraggedItem = null;
            t.SelectedItem = null;
            //SelectedItem = null; 
            Dragging = false;
        }

        private void Drop(Canvas sender)
        {
            
            if (DraggedItem != null)
            {
                
                if (((Canvas)sender).Resources["taken"] == null && !MainWindowViewModel.CanvasGrid.ContainsValue(DraggedItem))
                {
                    //MessageBox.Show(sender.Name);

                    MainWindowViewModel.CanvasGrid.Add(sender,DraggedItem);

                    Uri imgUri = new Uri(DraggedItem.Type, UriKind.Relative);
                    BitmapImage imageBitmap = new BitmapImage(imgUri);
                    ((Canvas)sender).Background = new ImageBrush(imageBitmap);
                    ((Canvas)sender).Resources.Add("taken", true);
                    ((TextBlock)(sender).Children[0]).Text = "";




                }
                else if(MainWindowViewModel.CanvasGrid.ContainsValue(DraggedItem))
                {
                    MessageBox.Show("Object can be only in one canvas.");
                }
                NetworkViewViewModel.SelectedItem = null;
                NetworkViewViewModel.Dragging = false;
            }
        }

        private void Remove(Canvas sender)
        {
            if (((Canvas)sender).Resources["taken"] != null)
            {
                MainWindowViewModel.CanvasGrid.Remove(sender);
                ((Canvas)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                ((Canvas)sender).Resources.Remove("taken");
                ((TextBlock)(sender).Children[0]).Text = "DROP HERE";
            }
        }

        

    }
}
