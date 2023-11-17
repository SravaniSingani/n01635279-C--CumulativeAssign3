using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDbAssignmnet.Models
{
    public class Teacher
    {
        //What defines the teacher
        public int TeacherId {  get; set; }                                 
        public string TeacherName {  get; set; }
        public decimal Salary {  get; set; }
        public string HireDate { get; set; }
        public string ClassName { get; set; }
    }
}