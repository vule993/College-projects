using PZ3_NetworkService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PZ3_NetworkService.ViewModel
{
    public class ReportViewModel : BindableBase
    {
        String dailyreport = "DAILY REPORT:" + System.Environment.NewLine + System.Environment.NewLine;
        public String DailyReport
        {
            get
            {
                return dailyreport;
            }
            set
            {
                if(dailyreport != value)
                {
                    dailyreport = value;
                    OnPropertyChanged("DailyReport");
                }
            }
        }

        public List<String> Report;

        public MyCommand ShowData { get; set; }


        public ReportViewModel()
        {
            
            Report = new List<string>();
            ShowData = new MyCommand(Data);
        }

        private void Data()
        {
            DailyReport = "DAILY REPORT:" + System.Environment.NewLine + System.Environment.NewLine;
            Report.Clear();
            for (int i = 0; i< Element.counter; i++){
                Report.Add("");
            }      

            String line;
            int id = -1;
            DateTime dt;
            try
            {
                using (StreamReader rd = new StreamReader("log.txt"))
                {
                    while ((line = rd.ReadLine()) != null)
                    {
                        //citamo
                        string[] separatingChars = { " ", "\tid:", " => " };
                        Int32.TryParse(line.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries)[2], out id); //uzimamo id
                        DateTime.TryParse(line.Split('\t')[0], out dt); //uzimamo datetime

                        if(dt.Date == DateTime.Now.Date)
                        {
                            Report[id - 1] = Report[id - 1] + "\t\t" + line + System.Environment.NewLine;  //na poziciju objekta stavljamo odgovarajuci string
                        }

                    }
                }
                
            }
            catch 
            {
                DailyReport = DailyReport + System.Environment.NewLine + "System recieving data at the moment. Try in a few seconds.";
                return;
            }
            int k = 0;
            foreach (String s in Report)
            {
                k++;
                DailyReport = DailyReport + String.Format("\tElement {0} : ", k) + System.Environment.NewLine + s;
            }

        }
    }
}
