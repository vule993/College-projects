using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATP_application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataIO serializer = new DataIO();
        public static BindingList<Teniser> Atp_list { get; set; }
        public static int id = -1;
        public static String app_path = System.Windows.Forms.Application.ExecutablePath.Substring(0, System.Windows.Forms.Application.ExecutablePath.Length - 29);



        public static string path = "";

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            String line;
            if (File.Exists("dump.txt"))
            {
                using (StreamReader sr = new StreamReader("dump.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            File.Delete(app_path + line);
                        }
                        catch (Exception) { }
                    }

                }
                File.Delete("dump.txt");
            }

            Atp_list = serializer.DeSerializeObject<BindingList<Teniser>>("atp_lista.xml");
            if(Atp_list == null){
                Atp_list = new BindingList<Teniser>();
            }
            
            InitializeComponent();

            DataContext = this;
        }

        private void Add_player_in_list(object sender, RoutedEventArgs e)
        {
            Add_tennis_player new_window = new Add_tennis_player();
            new_window.ShowDialog();
        }


        private void CloseApp_Click(object sender, CancelEventArgs e)
        {
            serializer.SerializeObject<BindingList<Teniser>>(Atp_list, "atp_lista.xml");
        }

        private void CloseApp_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = File.AppendText("dump.txt"))
            {
                sw.WriteLine(Atp_list.ElementAt(dg_tabela.SelectedIndex).Imgpath);   //posto je file to delete samo img/naziv_slike.jpg pa ce cela putanja biti upisana u dump fajl
            }
            Atp_list.RemoveAt(dg_tabela.SelectedIndex);
        }
        
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            id = dg_tabela.SelectedIndex;
            path = Atp_list.ElementAt(id).Imgpath.ToString();
            Add_tennis_player new_window = new Add_tennis_player();
            new_window.ShowDialog();
            MainWindow.id = -1;
            Atp_list.ResetBindings();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            id = dg_tabela.SelectedIndex;
            ViewWindow v = new ViewWindow();
            v.ShowDialog();
            MainWindow.id = -1;
            Atp_list.ResetBindings();
        }
    }
}
