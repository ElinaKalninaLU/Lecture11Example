using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TL11.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
