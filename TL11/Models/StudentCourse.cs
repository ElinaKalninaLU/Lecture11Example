using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TL11.Models
{
    [Table("Student_course")]
    public class StudentCourse
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }

        public int Student_ID { get; set; }

        public int Course_ID {  get; set; }
    }
}
