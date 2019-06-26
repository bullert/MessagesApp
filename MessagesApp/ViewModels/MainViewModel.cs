using MessagesApp.Commands;
using MessagesApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MessagesApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private object selectedViewModel;
        
        private string text;

        public MainViewModel()
        {
            Navigation = new NavigationViewModel(this);

            var br0 = Application.Current.FindResource("MaterialDesignBody") as SolidColorBrush;
            var br1 = Application.Current.FindResource("MaterialDesignCardBackground") as SolidColorBrush;
            var br2 = Application.Current.FindResource("MaterialDesignPaper") as SolidColorBrush;

            Text = br0.ToString() + br1.ToString() + br2.ToString();
        }
        
        public NavigationViewModel Navigation { get; set; }

        private ViewModelBase contentView;
        //public ViewModelBase ContentView
        //{
        //    get => contentView;
        //    set => SetProperty(ref contentView, value);
        //}
        public ViewModelBase ContentView
        {
            get
            {
                return contentView;
            }
            set
            {
                contentView = value;
                OnPropertyChanged("ContentView");
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
