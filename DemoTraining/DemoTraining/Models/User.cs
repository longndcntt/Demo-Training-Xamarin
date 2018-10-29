using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTraining.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
