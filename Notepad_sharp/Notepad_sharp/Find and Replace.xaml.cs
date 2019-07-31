using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Notepad_sharp
{
    /// <summary>
    /// Interaction logic for Find_and_Replace.xaml
    /// </summary>
    public partial class Find_and_Replace : Window
    {
        public Find_and_Replace()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;

            
            var interopHelper = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            Screen activeScreen = Screen.FromHandle(interopHelper.Handle);

            int w = activeScreen.WorkingArea.Width;
            int h = activeScreen.WorkingArea.Height;
            
            this.Top = h - 0.6*h;
            this.Left = w - 0.27*w;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Tb_Find.Text != "")
            {
                MainWindow m = (MainWindow)System.Windows.Application.Current.MainWindow;

                //m.StringInRTB(m.StringFromRTB().Replace(Tb_Find.Text, Tb_Replace_With.Text));

                //zamena search -> replace

                TextRange tr = new TextRange(m.Editor.Document.ContentStart, m.Editor.Document.ContentEnd);
                string rtf;
                MemoryStream ms = new MemoryStream();

                tr.Save(ms, System.Windows.DataFormats.Rtf);

                rtf = ASCIIEncoding.Default.GetString(ms.ToArray());

                rtf = rtf.Replace(Tb_Find.Text, Tb_Replace_With.Text);

                MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(rtf));

                m.Editor.SelectAll();
                m.Editor.Selection.Load(stream, System.Windows.DataFormats.Rtf);
            }
            else
            {
                System.Windows.MessageBox.Show("Find polje ne sme biti prazno!");
            }
            

            
        }
    }
}
