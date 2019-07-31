using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad_sharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool file_saved = false;    //da li je izmena sacuvana
        private bool file_exists = false;   //da li je fajl vec na disku?
        public static String file_path;           //putanja tekuceg fajla potrebna za update

        private Find_and_Replace fr = null;

        double[] fontsizes = { 8,9,10,11,12,14,16,18,20,22,24,26,28,36,48,72 };
        public MainWindow()
        {

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            BtnFontColor.FontWeight = FontWeights.Bold;
            BtnItalic.FontStyle = FontStyles.Italic;
            
            CmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f=>f.Source);
            CmbFontFamily.SelectedIndex = 155;
            CmbFontSize.ItemsSource = fontsizes;
            CmbFontSize.SelectedIndex = 4;
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();    //kada se uhvati prozor mozes ga pomerati
        }
        private void Edditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

            //Kada se selektovani tekst promeni moramo postaviti vrednosti za dugmice na ono sto tekst kaze
            object t = Editor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BtnBold.IsChecked = (t != DependencyProperty.UnsetValue) && (t.Equals(FontWeights.Bold));


            t = Editor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            BtnItalic.IsChecked = (t != DependencyProperty.UnsetValue) && (t.Equals(FontStyles.Italic));

            t = Editor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            BtnUnderline.IsChecked = (t != DependencyProperty.UnsetValue) && (t.Equals(TextDecorations.Underline));


            t = Editor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            if (t != DependencyProperty.UnsetValue)
            {
                CmbFontFamily.SelectedItem = t;
            }
            else
            {
                CmbFontFamily.Text = "";
            }

            t = Editor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if(t != DependencyProperty.UnsetValue)
            {
                CmbFontSize.SelectedItem = t;
            }
            else
            {
                CmbFontSize.Text = "";
            }

            t = Editor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            if(t != DependencyProperty.UnsetValue)
            {
                BtnFontColor.Foreground = (Brush)t;
            }
            

        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFontFamily.SelectedItem != null)
            {
                Editor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, CmbFontFamily.SelectedItem);
                Editor.Focus();
            }
        }
        private void CmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFontSize.SelectedItem != null)
            {
                Editor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, CmbFontSize.SelectedItem);
                Editor.Focus();
            }
        }       

        //promena boje
        private void BtnFontColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.ShowDialog();
            var brush = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
            Editor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, brush);
            BtnFontColor.Foreground = brush;
            Editor.Focus();
        }

        //broj reci:
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (L_count!=null)
            {
                //  \S -> gets whitespaces newlines and tabs
                L_count.Content = "Broj reci: " + Regex.Matches(StringFromRTB(), @"[\S]+").Count.ToString();

                /*
                string range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd).Text;

                MatchCollection wordColl = Regex.Matches(range, @"[\W]+");

                if (Editor.Document.ContentStart.GetOffsetToPosition(Editor.Document.ContentEnd) < 5)
                {
                    L_count.Content = "Broj reci: " + 0;
                }
                else
                {
                    L_count.Content = "Broj reci: " + wordColl.Count.ToString();
                }
                */    


            }

            //save ikonica
            


            file_saved = false;

        }
       
        //INSERT DATE + shortcut
        private void InsertDate_Click(object sender, RoutedEventArgs e)
        {
            Editor.CaretPosition.InsertTextInRun(" "+DateTime.Now.ToShortDateString()+" ");
        }
        private void Editor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                InsertDate_Click(sender, e);
            }
        }

        //FIND REPLACE
        private void FindReplace_Checked(object sender, RoutedEventArgs e)
        {
            fr = new Find_and_Replace();
            fr.Show();
        }

        // NEW, OPEN, SAVE AS / SAVE(update)
        private void BtnNew_Checked(object sender, RoutedEventArgs e)
        {

            if (!file_saved)
            {
                Prompt p = new Prompt();
                p.ShowDialog();
            }
            //ako otvorimo fajl idemo iznova
            StringInRTB("");
            file_exists = false;
            file_saved = false;
            file_path = "";

        }
        private void BtnOpen_Checked(object sender, RoutedEventArgs e)
        {
            if (!file_saved)
            {
                Prompt p = new Prompt();
                p.ShowDialog();
                
            }
            try
            {
                using (var sfd = new OpenFileDialog())
                {
                    sfd.Filter = "*.rtf(RTF file)|*.rtf";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        
                            FileStream fs = new FileStream(sfd.FileName, FileMode.Open);
                            TextRange r = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                            r.Load(fs, System.Windows.DataFormats.Rtf);

                            file_saved = true;      //otvaram sacuvani fajl
                            file_exists = true;     //ako otvorim fajl postoji na sistemu
                            file_path = sfd.FileName;
                        fs.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
            

        }
        public void BtnSave_Checked(object sender, RoutedEventArgs e)
        {
            //UPDATE
            if (file_exists)
            {
                FileStream fs = new FileStream(file_path, FileMode.Create);
                TextRange r = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                r.Save(fs, System.Windows.DataFormats.Rtf);

                file_saved = true;
                file_exists = true;     //posto je donji deo koda cuvanje prvi put kazemo da fajl sada postoji
                System.Windows.MessageBox.Show("Izmene sacuvane!");
                fs.Close();
                return;     //ovo je update iskacemo iz metode
            }
            //PRVO CUVANJE
            using (var sfd = new SaveFileDialog())
            {

                sfd.Filter = "*.rtf(RTF file)|*.rtf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    TextRange r = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                    r.Save(fs, System.Windows.DataFormats.Rtf);
                    fs.Close();
                }

                file_path = sfd.FileName;   //kada prvi put cuvamo pamtim putanju za update

            }
            file_exists = true;
            file_saved = true;

        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //ovde cemo provere za save / update...
        private void Close_app_Click(object sender, RoutedEventArgs e)
        {

            if (!file_saved)    //bilo da je cuvan ili ne nudimo snimanje, ako je sniman ranije radi se UPDATE
            {
                Prompt p = new Prompt();
                p.ShowDialog();
            }

            //zatvaram find replace ako je otvoren
            if(fr != null)
            {
                fr.Close();
            }

            this.Close();
        }

        //POMOCNE F-je

        //text iz Rich Text Box-a:
        public String StringFromRTB()
        {
            TextRange temp = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
            return temp.Text;
        }
        //setuj text u Rich Text Box:
        public void StringInRTB(String s)
        {
            TextRange temp = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd)
            {
                Text = s
            };

        }

    }
}