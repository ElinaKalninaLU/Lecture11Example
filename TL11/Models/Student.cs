using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TL11.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        public override string? ToString() => Name + " " + Surname;
    }
}
