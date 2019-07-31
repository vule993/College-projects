using PZ3_NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PZ3_NetworkService.ViewModel
{
    public class DataChartViewModel : BindableBase
    {

        ObservableCollection<int> chooseelement = new ObservableCollection<int>();
        public ObservableCollection<int> ChooseElement
        {
            get
            {
                return chooseelement;
            }
            set
            {
                if(chooseelement != value)
                {
                    chooseelement = value;
                    OnPropertyChanged("ChooseElement");
                }
                
            }
        }



        static int selectedid = 0;
        public static int SelectedId { get { return selectedid; } set { selectedid = value; } }

        static ObservableCollection<RectItem> rectitems = new ObservableCollection<RectItem>();
        public static ObservableCollection<RectItem> RectItems { get { return rectitems; }
            set
            {
                if (rectitems != value)
                {
                    rectitems = value;
                    //OnPropertyChanged("RectItems");
                }
                
            }
        }


        String path;
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                if(path != value){
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        Dictionary<double,DateTime> elementvalues = new Dictionary<double, DateTime>();
        public Dictionary<double,DateTime> ElementValues { get { return elementvalues; } set { elementvalues = value; } }

        public MyCommand ShowGraph { get; set; }


        //CONSTUCTOR
        public DataChartViewModel()
        {
            for(int i = 1; i <= Element.counter; i++)
            {
                ChooseElement.Add(i);
            }
            ShowGraph = new MyCommand(Graph);
        }

     
        private void Graph()
        {

            if(SelectedId != 0)
            {
                RectItems.Clear();
                ElementValues.Clear();

                int linesNo = 0;

                String line;    //za citanje iz fajla
                int id = -1;    //za id procitan iz fajla
                double val = -1;   //za value procitan iz fajla
                DateTime dt;
                RectItem r = new RectItem(0,DateTime.MinValue);



                try
                {
                    using (StreamReader rd = new StreamReader("log.txt"))
                    {
                        while ((line = rd.ReadLine()) != null)
                        {
                            
                            //citamo
                            string[] separatingChars = { " ", "\tid:", " => " };
                            Int32.TryParse(line.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries)[2], out id); //uzimamo id
                            Double.TryParse(line.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries)[4], out val); //uzimamo val
                            DateTime.TryParse(line.Split('\t')[0], out dt); //uzimamo datetime

                            if (SelectedId == id)
                            {
                                try
                                {
                                    elementvalues.Add(val, dt);
                                    
                                }
                                catch
                                {

                                }
                                
                            }




                        }
                    }

                }
                catch
                {
                    MessageBox.Show("System recieving data at the moment. Try in a few seconds.");
                    return;
                }



                try
                {

                    if(ElementValues.Count == 0)
                    {
                        MessageBox.Show("No data for display.");
                        return;
                    }

                    linesNo = elementvalues.Count;
                    int maxBarsToShow = 10;
                    int brojac = 0;
                    int width = 960 / linesNo;  //sirina svakog je ista
                    
                    int x = 0;

                    
                    foreach (KeyValuePair<double, DateTime>row in ElementValues)
                    {

                        if(linesNo < maxBarsToShow)
                        {
                            //MessageBox.Show("Usao");
                            r = new RectItem(row.Key, row.Value);
                            r.Width = width;
                            r.Height = row.Key / 2;
                            r.Y = 490 - r.Height;
                            r.X = x;
                            RectItems.Add(r);

                            x += width;
                            brojac++;
                        }
                        else if (brojac<linesNo-maxBarsToShow)
                        {
                            brojac++;
                            width = 960 / maxBarsToShow;
                            continue;
                        }
                        else
                        {
                            //MessageBox.Show("Usao");
                            r = new RectItem(row.Key, row.Value);
                            r.Width = width;
                            r.Height = row.Key / 2;
                            r.Y = 490 - r.Height;
                            r.X = x;
                            RectItems.Add(r);

                            x += width;
                        }
                        
                        
                        
                    }

                    





                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("You must select element.");
            }


        }
    }

    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        SolidColorBrush col = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
        public SolidColorBrush Col { get { return col; } set { col = value; } }
        double val;
        DateTime date;
        public double Val { get { return val; } set { val = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public String Data { get { return String.Format("\tValue: {0}\n\tDate : {1}",Val,Date); } }
        public RectItem(double v, DateTime d)
        {
            val = v;
            date = d;
            if(val > 735 || val < 670)
            {
                //# FFFF0000
                Col = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));
            }
            //new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
        }
    }



}
