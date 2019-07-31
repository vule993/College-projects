using PZ3_NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PZ3_NetworkService.ViewModel
{
    public class NetworkDataViewModel : BindableBase
    {

        public static String appPath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.Length - 10);


        bool filter = false;
        

        bool rbg = true, rbl = false;
        public bool Rbg { get { return rbg; } set { rbg = value; } }
        public bool Rbl { get { return rbl; } set { rbl = value; } }

        String validationmessage = null;
        public String ValidationMessage { get { return validationmessage; } set { validationmessage = value; } }


        int id = 0; //za filter



        String idfilter;
        public String IdFilter
        {
            get
            {
                return idfilter;
            }
            set
            {
                if(idfilter != value)
                {
                    idfilter = value;
                    OnPropertyChanged("IdFilter");
                }
            }
        }

        String filtertype;
        public String FilterType
        {
            get
            {
                return filtertype;
            }
            set
            {
                if (filtertype != value)
                {
                    filtertype = value;
                    OnPropertyChanged("FilterType");
                }
            }
        }



        //tipovi u comboboxu
        public static BindingList<String> Types { get; set; }


        //lista svih elemenata
        static ObservableCollection<Element> allelements = new ObservableCollection<Element>();
        public static ObservableCollection<Element> AllElements { get { return allelements; } set { allelements = value; } }
        
        
        
        //lista elemenata za prikaz
        static ObservableCollection<Element> elements = new ObservableCollection<Element>();
        public static ObservableCollection<Element> Elements  { get{ return elements; } set{ elements = value; } }
        
        //selektovan element za brisanje
        public Element selectedElement;
        public Element SelectedElement { get { return selectedElement; } set { selectedElement = value; DeleteCommand.RaiseCanExecuteChanged(); } }




        //
        private String loadedimg = "default";
        public String LoadedImg
        {
            get
            {
                return appPath + "img/" + loadedimg + ".jpg";
            }
            set
            {
                if (value != loadedimg)
                {
                    loadedimg = value;
                    OnPropertyChanged("LoadedImg");
                }
            }
        }
        


        //koristimo prilikom dodavanja elementa koji se posmatra
        String name, model;
        public String Name { get => name; set => name = value; }
        public String Model { get => model; set { LoadedImg = value; model = value; }  }


        

        public MyCommand AddCommand { get; set; }
        public MyCommand DeleteCommand { get; set; }
        public MyCommand ApplyFilterCommand { get; set; }
        public MyCommand ResetFilterCommand { get; set; }


        public NetworkDataViewModel()
        {
            Types = new BindingList<String>();
            Types.Add("Centrifugal water pump");
            Types.Add("Diesel water pump");
            Types.Add("Sino plant water pump");
            Types.Add("Tradepower jet motor water pump");

            //Interaction 15h kabinet



            AddCommand = new MyCommand(OnAdd);
            DeleteCommand = new MyCommand(OnDelete, CanDelete);
            ApplyFilterCommand = new MyCommand(OnApplyFilter);
            ResetFilterCommand = new MyCommand(OnResetFilter);
            

        }   //end of ctor

        private void OnAdd()
        {
            OnResetFilter();
            //MessageBox.Show(Name);
            if (Name == "" || Model == "" || Name == null || Model == null)
            {
                MessageBox.Show("You must enter all data.");
                return;
            }
            if (Name.Count() < 26)
            {
                AllElements.Add(new Element(Name, Model, Model, 0));
                Elements.Add(AllElements[AllElements.Count - 1]);
            }
            else
            {
                MessageBox.Show("Name max length is 25 chars.");
            }
            
            
        }
        private void OnDelete()
        {
            AllElements.Remove(SelectedElement);
            Elements.Remove(SelectedElement);
        }
        private void OnApplyFilter()
        {
            

            if (!IsIdValid() && IsIdEntered())
            {
                MessageBox.Show("Id must be valid number and greater then zero.");
                return;
            }
            

            if (!IsIdEntered() && !IsTypeSelected()) // oba nisu uneta
            {
                MessageBox.Show("You must fill at least one filter");
                return;
            }

            Elements.Clear();
            filter = true;

            bool tf = rbg;  //ako je selektovano vece

            if (IsIdEntered() && IsIdValid() && IsTypeSelected())  // oba filtera
            {
                foreach (Element e in AllElements)
                {
                    if (tf) //ako je selektovano vece
                    {
                        //ako je pogodjen tip i id objekta VECI od unetog id-a
                        if((e.TypeName.Equals(FilterType) || e.TypeName.Equals(FilterType+"(error)")) && e.Id>id )
                        {
                            Elements.Add(e);
                        }
                    }
                    else    //ako je selektovano manje
                    {
                        //ako je pogodjen tip i id objekta MANJI od unetog id-a
                        if ((e.TypeName.Equals(FilterType) || e.TypeName.Equals(FilterType + "(error)")) && e.Id < id)
                        {
                            Elements.Add(e);
                        }
                    }
                }
            }
            else if(IsTypeSelected())   //samo tip
            {
                foreach (Element e in AllElements)
                {
   
                    //ako je pogodjen tip
                    if (e.TypeName.Equals(FilterType) || e.TypeName.Equals(FilterType + "(error)"))
                    {
                        Elements.Add(e);
                    }

                }
            }
            else if (IsIdEntered() && IsIdValid()) //samo id
            {

                //ako je pogodjen tip
                foreach (Element e in AllElements)
                {
                    if (tf) //ako je selektovano vece
                    {
                        //ako je pogodjen tip i id objekta VECI od unetog id-a
                        if ( e.Id > id)
                        {
                            Elements.Add(e);
                        }
                    }
                    else    //ako je selektovano manje
                    {
                        //ako je pogodjen tip i id objekta MANJI od unetog id-a
                        if (e.Id < id)
                        {
                            Elements.Add(e);
                        }
                    }
                }

            }
            

        }
        private bool IsIdEntered()   //da li je nesto uneto kao id?
        {
            return (IdFilter != null && IdFilter != "");
        }
        private bool IsIdValid()    //da li je id ispravan
        {
            return ((Int32.TryParse(IdFilter, out id) && id > 0));
        }
        private bool IsTypeSelected()   //da li je selektovan tip
        {
            return FilterType != null;
        }

        private void OnResetFilter()
        {

            FilterType = null;
            IdFilter = null;
            filter = false;

            Elements.Clear();
            foreach(Element e in AllElements)
            {
                Elements.Add(e);
            }
            
        }
        private bool CanDelete()
        {
            return SelectedElement != null;     //ako sam bilo sta selektovao moze da se brise
        }


    }
}
