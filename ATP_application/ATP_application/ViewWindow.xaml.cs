using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ATP_application
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ViewWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            Teniser t = MainWindow.Atp_list.ElementAt(MainWindow.id);

            lb_name.Content = t.Name;
            lb_surname.Content = t.Surname + "( " + t.Country.ToUpper() + " )";
            lb_birth.Content = t.Display_date;
            lb_points.Content = t.Points.ToString();
            lb_type.Content = (t.Playstyle == "Leva") ?"Levoruk":"Desnoruk";
            img.Source = Add_tennis_player.Load_img(t.Imgpath_bind);
            tbl_about.Text = t.About;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Add_tennis_player change = new Add_tennis_player();
            change.ShowDialog();
            this.Close();
        }

        private void ViewClose_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.id = -1;       //ovo i moze ovde, ali kad sam vec izmenu gasio na mainu tamo ce
            this.Close();
        }
    }
}
