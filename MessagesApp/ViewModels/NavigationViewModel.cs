using MessagesApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        MainViewModel mainViewModel;

        public NavigationViewModel(MainViewModel mvm)
        {
            mainViewModel = mvm;
            RelayCommand = new RelayCommand(Navigate);
        }

        public RelayCommand RelayCommand { get; set; }

        public void Navigate(object param)
        {
            switch (int.Parse(param as string))
            {
                case 1:
                    mainViewModel.ContentView = new LoginViewModel();
                    break;
                case 2:
                    mainViewModel.ContentView = new MainViewModel();
                    break;
                case 3:
                    mainViewModel.ContentView = new RegisterViewModel();
                    break;
                default:
                    System.Windows.Application.Current.Shutdown();
                    break;
            }
        }
    }
}
