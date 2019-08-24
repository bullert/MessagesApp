using MessagesApp.Commands;
using MessagesApp.Views;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MessagesApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            Navigation = new NavigationViewModel(this);

            var br0 = Application.Current.FindResource("MaterialDesignCheckBoxDisabled") as SolidColorBrush;
            var br1 = Application.Current.FindResource("MaterialDesignBodyLight") as SolidColorBrush;
            var br2 = Application.Current.FindResource("MaterialDesignPaper") as SolidColorBrush;

            Text = br0.ToString() + br1.ToString() + br2.ToString();
        }
        
        public NavigationViewModel Navigation { get; set; }
        
        private BindableBase contentView;
        public BindableBase ContentView
        {
            get => contentView;
            set => SetProperty(ref contentView, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
    }
}
