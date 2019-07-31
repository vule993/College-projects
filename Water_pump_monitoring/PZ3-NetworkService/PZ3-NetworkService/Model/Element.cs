using PZ3_NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PZ3_NetworkService.Model
{
    public class Element:BindableBase
    {
        public static int counter = 0;
        private int id;
        private String name;
        private String type;        //appPath/img/typename.jpg
        private String typename;    //typename
        private double val;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Id { get { return id; } }
        public String Type
        {
            get
            {
                return NetworkDataViewModel.appPath + @"img\" + type + ".jpg";
            }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public String TypeName
        {
            get
            {
                return typename;
            }
            set
            {
                if (typename != value)
                {
                    Type = value;
                    typename = value;
                    OnPropertyChanged("TypeName");
                }
                
            }
        }
        public double Val
        {
            get
            {
                return val;
            }
            set
            {
                if (val != value)
                {
                    val = value;
                    OnPropertyChanged("Val");
                }
                
            }
        }

        public Element(String n = "", String t="", String tn="", double v = 0)
        {
            id = ++counter;
            Name = n;
            Type = t;
            TypeName = tn;
            //MessageBox.Show(Path.Combine(NetworkDataViewModel.appPath, Type));
            Val = v;
        }

        //override toString()
        public override string ToString()
        {
            return String.Format("\tid:{0} => {1}", Id, Val);
        }
    }
}
