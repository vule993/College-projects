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
    /// Interaction logic for Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        public static int indicator = -1;
        public Warning()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            System.Media.SystemSounds.Hand.Play();
        }

        private void povratak_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void izadji_Click(object sender, RoutedEventArgs e)
        {
            indicator = 1;
            this.Close();
        }
    }
}
