using MessagesApp.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        MainViewModel mainViewModel;

        public NavigationViewModel(MainViewModel mvm)
        {
            mainViewModel = mvm;
            RelayCommand = new RelayCommand(Navigate);

            //kMainViewModel = new MainViewModel();
            LoginViewModel = new LoginViewModel2(this);
            RegisterViewModel = new RegisterViewModel();
            TransitionerViewModel = new TransitionerViewModel();
            ConversationViewModel = new ConversationViewModel();
            ProfileViewModel = new ProfileViewModel();
        }

        public RelayCommand RelayCommand { get; set; }

        //public MainViewModel MainViewModel { get; set; }
        public LoginViewModel2 LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public TransitionerViewModel TransitionerViewModel { get; set; }
        public ConversationViewModel ConversationViewModel { get; set; }
        public ProfileViewModel ProfileViewModel { get; set; }

        private int index;
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }

        public void Navigate(object param)
        {
            Index = int.Parse(param as string) - 1;
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
                case 4:
                    mainViewModel.ContentView = new TransitionerViewModel();
                    break;
                case 5:
                    mainViewModel.ContentView = new ConversationViewModel();
                    break;
                case 6:
                    mainViewModel.ContentView = new ProfileViewModel();
                    break;
                default:
                    System.Windows.Application.Current.Shutdown();
                    break;
            }
        }
    }
}
