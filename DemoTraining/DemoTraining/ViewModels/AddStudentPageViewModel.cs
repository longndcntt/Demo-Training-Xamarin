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
    public class AddStudentPageViewModel : ViewModelBase
    {
        #region Property

        INavigationService _navigationService;
        Database db;
        private int _id;
        public int Id { get => _id; set { SetProperty(ref _id, value); } }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth { get => _dateOfBirth; set { SetProperty(ref _dateOfBirth, value); } }

        private string _displayName;
        public string DisplayName { get => _displayName; set { SetProperty(ref _displayName, value); } }

        private int _classId;
        public int ClassId { get => _classId; set { SetProperty(ref _classId, value); } }

        private ObservableCollection<Class> _listClass;

        private ObservableCollection<Student> _listStudent;
        public ObservableCollection<Class> ListClass { get => _listClass; set { SetProperty(ref _listClass, value); } }

        public ObservableCollection<Student> ListStudent { get => _listStudent; set { SetProperty(ref _listStudent, value); } }

        private Class _selectedClass;
        public Class SelectedClass { get => _selectedClass; set { SetProperty(ref _selectedClass, value); } }

        private Class _selectedClassShow;
        public Class SelectedClassShow
        {
            get => _selectedClassShow; set
            {
                SetProperty(ref _selectedClassShow, value);
            }
        }

        private Class _class;

        private Student _selectedStudent;

        public Class Class { get => _class; set { SetProperty(ref _class, value); } }
        public Student SelectedStudent
        {
            get => _selectedStudent; set
            {
                SetProperty(ref _selectedStudent, value);
                if (SelectedStudent != null)
                {
                    DisplayName = SelectedStudent.DisplayName;
                    DateOfBirth = SelectedStudent.DateOfBirth;
                    ClassId = SelectedStudent.IdClass;
                    SelectedClass = db.SearchClass(ClassId);
                }
            }
        }
        #endregion

        #region DelegateCommand
        public DelegateCommand AddStudentCommand { get; private set; }
        public DelegateCommand EditStudentCommand { get; private set; }
        public DelegateCommand ChangeSelectedClassShowCommand { get; set; }
        public DelegateCommand ChangeSelectedClassCommand { get; set; }
        public DelegateCommand ChangeItemSelectedListViewCommand { get; set; }
        public DelegateCommand SelectedClassIndex { get; set; }
        #endregion


        #region Constructor
        public AddStudentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            db = new Database();
            if (db.createDatabase())
            {
                ListClass = new ObservableCollection<Class>();
                ListStudent = new ObservableCollection<Student>();
                loadListClass();
                ShowListStudent();
                AddStudentCommand = new DelegateCommand(AddStudent);
                EditStudentCommand = new DelegateCommand(EditStudent);
                ChangeSelectedClassShowCommand = new DelegateCommand(ChangeSelectedClassShow);
                ChangeSelectedClassCommand = new DelegateCommand(ChangeSelectedClass);
                ChangeItemSelectedListViewCommand = new DelegateCommand(ChangeItemSelectedListView);
            }
        }


        #endregion

        #region EventHandler


        private void ChangeItemSelectedListView()
        {
            if (SelectedStudent != null)
            {
                ClassId = SelectedStudent.IdClass;
                SelectedClass = db.SearchClass(ClassId);
            }
        }

        private void ChangeSelectedClass()
        {
            if (SelectedClass != null)
            {
                ClassId = SelectedClass.Id;
            }
        }

        private void ChangeSelectedClassShow()
        {
            ListStudent.Clear();
            ShowListStudent();
        }
        #endregion

        #region Load Class List
        private void loadListClass()
        {
            List<Class> list = db.selectClass();
            foreach (var l in list)
            {
                ListClass.Add(l);
            }
        }
        #endregion

        #region Show Student ListView
        public void ShowListStudent()
        {
            if (SelectedClassShow != null)
            {
                List<Student> list = db.SelectStudentByIdClass(SelectedClassShow.Id);
                foreach (var l in list)
                {
                    ListStudent.Add(l);
                }
            }
            else
            {
                List<Student> list = db.SelectStudent();
                foreach (var l in list)
                {
                    ListStudent.Add(l);
                }
            }
        }
        #endregion

        #region AddStudent
        private async void AddStudent()
        {
            if (SelectedClass != null)
            {
                Student st = new Student()
                {
                    DisplayName = DisplayName,
                    DateOfBirth = DateOfBirth,
                    IdClass = SelectedClass.Id,
                };
                if (db.InsertStudent(st))
                {
                    await App.Current.MainPage.DisplayAlert("Notify", "Add student success", "Ok");
                    ListStudent.Clear();
                    ShowListStudent();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notify", "Add student fail", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Notify", "You haven't typed into Class Id Field", "Ok");
            }

        }
        #endregion

        #region Edit Student

        private async void EditStudent()
        {
            if (SelectedStudent != null)
            {
                Student st = db.searchStudent(SelectedStudent.Id);
                if (st != null)
                {
                    SelectedClass = db.SearchClass(SelectedStudent.IdClass);
                    st.IdClass = ClassId;
                    st.DisplayName = DisplayName;
                    st.DateOfBirth = DateOfBirth;
                    if (db.updateStudent(st))
                    {
                        await App.Current.MainPage.DisplayAlert("Notify", "Update student " + st.Id + " success", "Ok");
                        ListStudent.Clear();
                        ShowListStudent();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notify", "Update student " + st.Id + " fail", "Ok");
                    }
                }

            }
        }
        #endregion
    }
}
