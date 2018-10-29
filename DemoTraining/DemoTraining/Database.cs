using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DemoTraining.Models;

namespace DemoTraining
{
    public class Database
    {
        //lấy thư mục lưu trữ csdl trên hệ thống
        string folder = System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal);
        public bool createDatabase()
        {
            try
            {
                //tạo csdl
                using (var connection = new
                    SQLiteConnection(System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    //tạo 2 bang
                    connection.CreateTable<Student>();
                    connection.CreateTable<Class>();
                    connection.CreateTable<User>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message + "Bug is here");
                //Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Xử lý bảng loại Student
        public bool InsertClass(Class loai)
        {
            try
            {
                using (var connection = new
                    SQLiteConnection(System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    connection.Insert(loai);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                //   Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertUser(User us)
        {
            try
            {
                using (var connection = new
                    SQLiteConnection(System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    connection.Insert(us);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                //   Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<Class> selectClass()
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<Class>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                //Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        //Xử lý Thêm Student
        public bool InsertStudent(Student h)
        {
            try
            {
                using (var connection = new
                    SQLiteConnection(System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    connection.Insert(h);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
        public List<Student> SelectStudent()
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<Student>().ToList<Student>();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public int CountUsers(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<User>().Where(p => p.UserName == username && p.PassWord == password).Count();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return -1;
            }
        }

        public bool DeleteStudent(List<Student> lst)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    foreach (var item in lst)
                    {
                        connection.Delete<Student>(item.Id);
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
        public Class SearchClass(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<Class>().Where((p) => p.Id == id).SingleOrDefault();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public bool UpdateClass(Class cl)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    connection.Update(cl);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public List<Student> SelectStudentByIdClass(int idClass)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<Student>().Where((p) => p.IdClass == idClass).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public Student searchStudent(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    return connection.Table<Student>().Where((p) => p.Id == id).SingleOrDefault();
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public bool updateStudent(Student st)
        {
            try
            {
                using (var connection = new SQLiteConnection
                    (System.IO.Path.Combine(folder, "manageStudent2.db")))
                {
                    connection.Update(st);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}
