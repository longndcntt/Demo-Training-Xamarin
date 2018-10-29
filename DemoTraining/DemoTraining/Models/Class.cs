using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTraining.Models
{
    public class Class
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string HomeroomTeacher { get; set; }

    }
}
