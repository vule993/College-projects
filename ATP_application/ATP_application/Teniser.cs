using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_application
{
    [Serializable]
    public class Teniser
    {
        
        private String name;        //
        private String surname;     //
        private DateTime birthday;  //day of birth
        private String country;     //3 letters

        private String playstyle;   //left/right handed...
        private int points;    //atp points
        private String imgpath;     //how to store img?? idea -> store path of img
        
        private readonly String imgpath_bind;     //how to store img?? idea -> store path of img
        private String display_date;
        private String about;

       


        public String Name { get { return name; } set { name = value; } }
        public String Surname { get { return surname; } set { surname = value; } }
        public DateTime Birthday { get { return birthday; } set { birthday = value; } }
        public String Country { get { return country; } set { country = value.ToUpper(); } }
        public String Playstyle { get { return playstyle; } set { playstyle = value; } }
        public int Points { get { return points; } set { points = value; } }

        public String Imgpath { get { return imgpath; } set { imgpath = value; } }
        public String Imgpath_bind { get { return Path.Combine(MainWindow.app_path, imgpath); } set {  } }
        public String Display_date { get { return display_date; } set { display_date = value; } }
        public String About { get { return about; } set { about = value; } }


        public Teniser(){ }
        public Teniser(String n = "V", String s = "R", String c = "SRB", String ps = "R", int p = 0, String cb_day = "", String cb_month = "", String cb_year = "", String imgp = "", String ab = "")
        {

            name = n;
            String date = cb_day + "/" + cb_month + "/" + cb_year;
            display_date = date;
            surname = s;
            country = c.ToUpper();
            playstyle = ps;
            points = p;
            imgpath = imgp;
            imgpath_bind = MainWindow.app_path + imgp;
            //DateTime.TryParse(display_date, out birthday);
            //display_date = Birthday.Date.ToShortDateString();

            about = ab;

        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} ", Name, Surname, Country, Playstyle, Points, Imgpath);
        }

    }
}
