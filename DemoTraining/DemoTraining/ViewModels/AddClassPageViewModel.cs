using DemoTraining.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DemoTraining.ViewModels
{
    public class AddClassPageViewModel : ViewModelBase
    {
        #region Property
        INavigationService _navigationService;
        Database db;
        private int _id;

        public int Id { get => _id; set { SetProperty(ref _id, value); } }
        private string _name;

        public string Name { get => _name; set { SetProperty(ref _name, value); } }

        private string _displayName;
        public string DisplayName { get => _displayName; set { SetProperty(ref _displayName, value); } }

        private string _homeroomTeacher;

        private ObservableCollection<Class> _listClass;
        public string HomeroomTeacher { get => _homeroomTeacher; set { SetProperty(ref _homeroomTeacher, value); } }
        public ObservableCollection<Class> ListClass { get => _listClass; set { SetProperty(ref _listClass, value); } }

        private Class _selectedClass;
        public Class SelectedClass
        {
            get => _selectedClass; set
            {
                SetProperty(ref _selectedClass, value); if (SelectedClass != null)
                {
                    DisplayName = SelectedClass.DisplayName;
                    Name = SelectedClass.Name;
                    HomeroomTeacher = SelectedClass.HomeroomTeacher;
                }
            }
        }
        public DelegateCommand AddClassCommand { get; private set; }
        public DelegateCommand<Class> EditClassCommand { get; private set; }
        #endregion


        public AddClassPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            db = new Database();
            db.createDatabase();
           
            showListClass();
            AddClassCommand = new DelegateCommand(AddClass);
            EditClassCommand = new DelegateCommand<Class>(EditClass);
        }

        private void showListClass()
        {
            ListClass = new ObservableCollection<Class>();
            List<Class> cl = db.selectClass();
            foreach (var item in cl)
            {
                ListClass.Add(item);
            }
            
        }

        private async void EditClass(Class cl)
        {
            if (SelectedClass != null)
            {
                cl = db.SearchClass(SelectedClass.Id);
                if (cl != null)
                {
                    cl.Name = Name;
                    cl.DisplayName = DisplayName;
                    cl.HomeroomTeacher = HomeroomTeacher;
                    if (db.UpdateClass(cl))
                    {
                        await App.Current.MainPage.DisplayAlert("Notify", "Update class " + cl.Id + " success", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notify", "Update class " + cl.Id + " success", "Ok");
                    }
                }
                ListClass.Clear();
                showListClass();
            }
        }

        private async void AddClass()
        {
            Class cl = new Class()
            {
                DisplayName = DisplayName,
                Name = Name,
                HomeroomTeacher = HomeroomTeacher
            };
            if (db.InsertClass(cl))
            {
                await App.Current.MainPage.DisplayAlert("Notify", "Insert class " + cl.DisplayName + " success", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Notify", "Insert class " + cl.DisplayName + " fail", "Ok");
            }
            ListClass.Add(cl);

        }

        #region Method

        #endregion
    }
}
