using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTraining.Models
{
    public class Student
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        [ForeignKey(typeof(Class))]
        public int IdClass { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
