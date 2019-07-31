﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PZ3_NetworkService.VML
{
    //locira ViewModel za odgovarajuci deo aplikacije
    public static class ViewModelLocator
    {
        public static readonly DependencyProperty AutoHookedUpViewModelProperty =
            DependencyProperty.RegisterAttached(
                "AutoHookedUpViewModel",    //?? zasto ovakav naziv, kada dodam 1 na kraj naziva sve radi
                typeof(bool),
                typeof(ViewModelLocator), 
                new PropertyMetadata(false, AutoHookedUpViewModelChanged));

        public static bool GetAutoHookedUpViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoHookedUpViewModelProperty);   //znaci ovo je getter za vrednost 0v1
        }
        public static void SetAutoHookedUpViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoHookedUpViewModelProperty, value);     //ovako se dependency-u menja vrednost
        }
        private static void AutoHookedUpViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }
            var viewType = d.GetType();
            String str = viewType.FullName;
            str = str.Replace(".Views", ".ViewModel");
            var viewTypeName = str;

            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }




    }
}
