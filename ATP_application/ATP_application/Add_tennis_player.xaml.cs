using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ATP_application
{
    /// <summary>
    /// Interaction logic for Add_tennis_player.xaml
    /// </summary>
    public partial class Add_tennis_player : Window
    {
        private String[] comboValues = new String[] { "Leva", "Desna" };
        private int[] comboValuesD = new int[31];
        private String[] comboValuesM = new String[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private int[] comboValuesY = new int[50];
        public static Teniser t;
        
        public Add_tennis_player()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            for (int i = 0; i < 50; i++)
            {
                if (i < 31)
                {
                    comboValuesD[i] = i+1;
                }
                comboValuesY[i] = 2005 - i;
            }                      //popunjavanje komboa dani i godine

            InitializeComponent();
            
            pb_img.Source = Load_img(Path.Combine(MainWindow.app_path, "default.png"));
            tb_imgpath.Visibility = Visibility.Collapsed;          
            cb_playstyle.ItemsSource = comboValues;             //inicijalizacija svih komboa
            cb_day.ItemsSource = comboValuesD;
            cb_month.ItemsSource = comboValuesM;
            cb_year.ItemsSource = comboValuesY;

            if (MainWindow.id != -1)
            {
                t = MainWindow.Atp_list[MainWindow.id];

                tb_name.Text = t.Name;
                tb_surname.Text = t.Surname;
                tb_country.Text = t.Country;

                String[] datum = new String[3];
                datum = t.Display_date.Split('-','/');

                cb_day.Text = datum[0];
                cb_month.Text = datum[1];
                cb_year.Text = datum[2];
                cb_playstyle.Text = t.Playstyle;               
                tb_points.Text = t.Points.ToString();

                tb_imgpath.Text = t.Imgpath;    //pamtim samo img/naziv_deo.ext

                if (File.Exists(t.Imgpath_bind))
                {
                    pb_img.Source = Load_img(t.Imgpath_bind);
                }
                else
                {
                    pb_img.Source = Load_img(Path.Combine(MainWindow.app_path, "default.png"));
                }
               
                tbl_about.Text = t.About;
                Bu_add.Content = "Izmeni";
                Browse.Content = "Izmeni sliku";
            }


        }

        private void Add_player_in_list(object sender, RoutedEventArgs e)
        { 
            String file_to_delete;
            if (Validate())
            {
                String img_path = Path.Combine(MainWindow.app_path, @"img\");                 //putanja do slika na lokalu
                DateTime dt = DateTime.Now;
                int hash = dt.GetHashCode();
                string hash_name = hash.ToString() + Path.GetExtension(tb_imgpath.Text);

                if(MainWindow.id == -1)
                {
                    t = new Teniser(tb_name.Text, tb_surname.Text, tb_country.Text, cb_playstyle.Text, Int32.Parse(tb_points.Text), cb_day.Text, cb_month.Text, cb_year.Text, Path.Combine(@"img\",hash_name), tbl_about.Text);
                    MainWindow.Atp_list.Add(t);
                    File.Copy(tb_imgpath.Text, Path.Combine(img_path, hash_name));      //kopiramo sliku na lokal
                }
                else
                {
                    file_to_delete = MainWindow.Atp_list[MainWindow.id].Imgpath;    //uzimam staru internu putanju da obrisem fajl ukoliko se promeni
                    MainWindow.Atp_list[MainWindow.id].Name = tb_name.Text;
                    MainWindow.Atp_list[MainWindow.id].Surname = tb_surname.Text;
                    MainWindow.Atp_list[MainWindow.id].Country = tb_country.Text;
                    MainWindow.Atp_list[MainWindow.id].Display_date = cb_day.Text+ "-" + cb_month.Text + "-" + cb_year.Text;
                    MainWindow.Atp_list[MainWindow.id].Playstyle = cb_playstyle.Text;
                    MainWindow.Atp_list[MainWindow.id].Points = Int32.Parse(tb_points.Text);
                    MainWindow.Atp_list[MainWindow.id].About = tbl_about.Text;
                    MainWindow.Atp_list[MainWindow.id].Imgpath = tb_imgpath.Text;
                    MainWindow.Atp_list[MainWindow.id].Imgpath_bind = MainWindow.app_path + tb_imgpath.Text;
                    if (tb_imgpath.Text != file_to_delete)   //ako se slika ne promeni
                    {

                        using (StreamWriter sw = File.AppendText("dump.txt"))
                        {
                            sw.WriteLine(file_to_delete);   //posto je file to delete samo img/naziv_slike.jpg pa ce cela putanja biti upisana u dump fajl
                        }

                        MainWindow.Atp_list[MainWindow.id].Imgpath = Path.Combine(@"img\", hash_name);
                        
                        File.Copy(tb_imgpath.Text, Path.Combine(img_path, hash_name));      //kopiramo sliku na lokal
                    }

                }

                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Morate popuniti sve podatke.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool Validate()
        {
            bool successeful = true;

            if (tb_name.Text.Equals(String.Empty) || tb_name.Text.Length > 18)
            {
                tb_name.BorderBrush = Brushes.Red;
                tb_name.BorderThickness = new Thickness(2);

                w_name.Content = "Popunite polje.Max 18 slova.";

                successeful = false;
            }
            else
            {
                w_name.Content = "";
                tb_name.BorderBrush = Brushes.Green;
            }
            
            if (tb_surname.Text.Equals(String.Empty) || tb_surname.Text.Length > 18)
            {
                w_surname.Content = "Popunite polje.Max 18 slova";
                tb_surname.BorderBrush = Brushes.Red;
                tb_surname.BorderThickness = new Thickness(2);
                successeful = false;
            }
            else
            {
                w_surname.Content = "";
                tb_surname.BorderBrush = Brushes.Green;
            }

            //drzava 3 slova 
            if (tb_country.Text.Length > 3 || tb_country.Text.Equals(String.Empty))
            {
                w_country.Content = "Popunite polje.Max 3 slova.";
                tb_country.BorderBrush = Brushes.Red;
                tb_country.BorderThickness = new Thickness(2);
                successeful = false;
            }
            else
            {
                w_country.Content = "";
                tb_country.BorderBrush = Brushes.Green;
            }

            
            string trydate = cb_day.Text + "/" + cb_month.Text + "/" + cb_year.Text;
            bool tf = DateTime.TryParse(trydate, out DateTime validationTime);
            if (cb_day.Text.Equals(String.Empty) || cb_month.Text.Equals(String.Empty) || cb_year.Text.Equals(String.Empty) || !tf)
            {
                
                w_date.Content = "Popunite polje.Feb max 29dana.";
                cb_day.BorderBrush = Brushes.Red;
                cb_day.BorderThickness = new Thickness(2);

                cb_month.BorderBrush = Brushes.Red;
                cb_month.BorderThickness = new Thickness(2);

                cb_year.BorderBrush = Brushes.Red;
                cb_year.BorderThickness = new Thickness(2);

                successeful = false;
            }
            else
            {
                
                w_date.Content = "";
                cb_day.BorderBrush = Brushes.Green;
                cb_month.BorderBrush = Brushes.Green;
                cb_year.BorderBrush = Brushes.Green;
            }

            if (cb_playstyle.Text.Equals(String.Empty))
            {
                w_playstyle.Content = "Popunite polje.";
                cb_playstyle.BorderBrush = Brushes.Red;
                cb_playstyle.BorderThickness = new Thickness(2);

                successeful = false;
            }
            else
            {
                w_playstyle.Content = "";
                cb_playstyle.BorderBrush = Brushes.Green;
            }
     
            if (tb_imgpath.Text.Equals(String.Empty))
            {
                w_img.Content = "Dodajte sliku.";
                tb_imgpath.BorderBrush = Brushes.Red;
                tb_imgpath.BorderThickness = new Thickness(2);
                successeful = false;
            }
            else
            {
                w_img.Content = "";
                tb_imgpath.BorderBrush = Brushes.Green;
            }

            //mora broj, max 5 cifara, ne sme prazno
            

            if (tb_points.Text.Equals(String.Empty)|| !Int32.TryParse(tb_points.Text, out int x) || x / 100000 > 0 || x < 0 )
            {
                w_points.Content = "Popunite (+) brojem.Max 5 cifara.";
                tb_points.BorderBrush = Brushes.Red;
                tb_points.BorderThickness = new Thickness(2);
                successeful = false;
            }
            else
            {
                w_points.Content = "";
                tb_points.BorderBrush = Brushes.Green;
            }
            
            //15 * 20 + 18 max - eventualna greska
            if (tbl_about.Text.Length > (15 * 20 + 10) || tbl_about.Text.Equals(String.Empty))
            {
                w_about.Content = String.Format("Popunite polje.Max {0} slova.", (15 * 20 + 10));
                tbl_about.BorderBrush = Brushes.Red;
                tbl_about.BorderThickness = new Thickness(2);
                successeful = false;
            }
            else
            {
                w_about.Content = "";
                tbl_about.BorderBrush = Brushes.Green;
            }

            return successeful;
        }
        public static BitmapImage Load_img(String s)
        {
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(s);
            bimage.EndInit();
            return bimage;
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {      
            try
            {
                OpenFileDialog dialog = new OpenFileDialog { Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
                
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {         
                                        //full path
                    tb_imgpath.Text = dialog.FileName;                  
                    pb_img.Source = Load_img(dialog.FileName);

                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Dogodila se greska prilikom dodavanja slike.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Bu_exit_Click(object sender, RoutedEventArgs e)
        {
            Warning w = new Warning();
            w.ShowDialog();
            if (Warning.indicator > 0)
            {
                Warning.indicator = -1;
                this.Close();
            }

        }


    }
}
