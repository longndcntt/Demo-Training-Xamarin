using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTraining.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        public DelegateCommand AddClassPageCommand { get; set; }
        public DelegateCommand AddStudentPageCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
            AddStudentPageCommand = new DelegateCommand(navigativeToAddStudentPage);
            AddClassPageCommand = new DelegateCommand(navigativeToAddClassPage);

        }

        private async void navigativeToAddStudentPage()
        {
            await _navigationService.NavigateAsync("AddStudentPage");
        }

        private async void navigativeToAddClassPage()
        {
            await _navigationService.NavigateAsync("AddClassPage");

        }
    }
}
