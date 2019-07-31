using PZ3_NetworkService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PZ3_NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        
        
        SolidColorBrush colorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
        SolidColorBrush colorND = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
        SolidColorBrush colorDC = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
        SolidColorBrush colorRP = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
        public SolidColorBrush ColorNV{ get { return colorNV; }
            set
            {
                if(colorNV != value)
                {
                    colorNV = value;
                    OnPropertyChanged("ColorNv");
                }
                
            }
        }
        public SolidColorBrush ColorND{ get { return colorND; }
            set
            {
                if (colorND != value)
                {
                    colorND = value;
                    OnPropertyChanged("ColorND");
                }
                
            }
        }



        public SolidColorBrush ColorDC{ get { return colorDC; }
            set
            {
                if (colorDC != value)
                {
                    colorDC = value;
                    OnPropertyChanged("ColorDC");
                }
                
            }
        }
        public SolidColorBrush ColorRP{ get { return colorRP; }
            set {
                if (colorRP != value)
                {
                    colorRP = value;
                    OnPropertyChanged("ColorRP");
                }
                
            }
        }


        BindableBase currentViewModel;  //sta se trenutno prikazuje i geter i seter ispod
        double new_value = 0;

        public static Dictionary<Canvas, Element> CanvasGrid = new Dictionary<Canvas, Element>();

        public BindableBase CurrentViewModel    //setter i getter za trenutni prikaz na strani
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }   //end of property

        /*ovde ce da idu instance viewmodela svih stranica pa cemo ih ucitavati u zavisnosti od prosledjenog stringa*/
        NetworkViewViewModel nvvm = new NetworkViewViewModel();
        NetworkDataViewModel ndvm = new NetworkDataViewModel();
        DataChartViewModel dcvm = new DataChartViewModel();
        ReportViewModel rpvm = new ReportViewModel();

        public MyCommand<String> NavCommand { get; private set; }

        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom
            CurrentViewModel = nvvm;
                                                           
            ColorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
            NavCommand = new MyCommand<string>(OnNav);

            if(File.Exists("log.txt"))
            {
                File.Delete("log.txt");
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "networkview":
                    CurrentViewModel = nvvm;
                    ColorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
                    ColorND = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorDC = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorRP = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    break;
                case "networkdata":
                    CurrentViewModel = ndvm;
                    ColorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorND = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
                    ColorDC = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorRP = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    break;
                case "datachart":
                    CurrentViewModel = dcvm;
                    ColorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorND = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorDC = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
                    ColorRP = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    break;
                case "report":
                    CurrentViewModel = rpvm;
                    ColorNV = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorND = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorDC = new SolidColorBrush(Color.FromArgb(0xFF, 0xE9, 0xEB, 0xEE));
                    ColorRP = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0xA7, 0xE3));
                    break;


            }
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();


            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                    //Prijem poruke
                    NetworkStream stream = tcpClient.GetStream();
                    string incomming;
                    byte[] bytes = new byte[1024];
                    int i = stream.Read(bytes, 0, bytes.Length);
                    //Primljena poruka je sacuvana u incomming stringu
                    incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                    if (incomming.Equals("Need object count"))
                    {
                        //Response
                        /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                         * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                         * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                         * */
                        //Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(NetworkDataViewModel.AllElements.Count.ToString());
                        stream.Write(data, 0, data.Length);
                    }
                    else
                    {
                        //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                        Console.WriteLine(incomming); //Na primer: "Objekat_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            //CommonData.Elements[1].Val = 400;
                            try
                            {
                                char[] separators = new char[] { '_', ':' };
                                string[] result;
                                Element e;
                                result = incomming.Split(separators);
                                //MessageBox.Show(result[2]);
                                int id = Int32.Parse(result[1]);
                                new_value = Double.Parse(result[2]);
                                e = NetworkDataViewModel.AllElements[id];
                                e.Val = new_value;

                                using (StreamWriter output = new StreamWriter("log.txt", append: true))
                                {
                                    output.WriteLine(DateTime.Now + " " + e.ToString());
                                }
                                //upisujemo los datum radi provere
                                using (StreamWriter output = new StreamWriter("log.txt", append: true))
                                {
                                    output.WriteLine(DateTime.MaxValue + " " + e.ToString());//OVDEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
                                }

                                //Dictionary<Canvas, Element> cg = NetworkViewViewModel.CanvasGrid;


                                //dodavanje upozoravajuceg znaka
                                String last7;
                                if (ValueValidation())
                                {   //if true regular photo
                                    last7 = e.TypeName.Substring(e.TypeName.Length - 7);

                                    if (last7 == "(error)")
                                    {
                                        e.TypeName = (e.TypeName.Split('(')[0]);
                                        if (CanvasGrid.ContainsValue(e))
                                        {
                                            Application.Current.Dispatcher.Invoke((Action)delegate
                                            {
                                                Canvas ctochange = CanvasGrid.FirstOrDefault(x => x.Value == e).Key;
                                                Uri imgUri = new Uri(e.Type, UriKind.Relative);
                                                BitmapImage imageBitmap = new BitmapImage(imgUri);
                                                ctochange.Background = new ImageBrush(imageBitmap);
                                            });

                                        }
                                        //e.Type = (e.TypeName.Split('(')[0]);
                                    }
                                }
                                else
                                {   //error photo
                                    last7 = e.TypeName.Substring(e.TypeName.Length - 7);
                                    if (last7 != "(error)")
                                    {
                                        e.TypeName = e.TypeName + "(error)";
                                        //e.Type = e.TypeName + "(error)";
                                        if (CanvasGrid.ContainsValue(e))
                                        {
                                            Application.Current.Dispatcher.Invoke((Action)delegate {
                                                Canvas ctochange = CanvasGrid.FirstOrDefault(x => x.Value == e).Key;
                                                Uri imgUri = new Uri(e.Type, UriKind.Relative);
                                                BitmapImage imageBitmap = new BitmapImage(imgUri);
                                                ctochange.Background = new ImageBrush(imageBitmap);
                                            });


                                        }
                                    }

                                }
                            }
                            catch
                            {
                                MessageBox.Show("You must restart simulator.");
                            }
                            


                            


                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        private bool ValueValidation()
        {
            double min_val = 670;
            double max_val = 735;
            //MessageBox.Show(new_value.ToString());
            if (new_value <= 735 && new_value >= 670)
            {
                return true;
            }

            return false;
        }

    }   //end of class
}   //end of namespace
