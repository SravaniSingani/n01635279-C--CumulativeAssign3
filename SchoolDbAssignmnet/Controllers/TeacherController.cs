using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDbAssignmnet.Models;

namespace SchoolDbAssignmnet.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        // Route to Views/Teacher/List.cshtml
        public ActionResult List(string SearchKey=null)
        {
            //method provides list of teachers

            List<Teacher> Teachers = new List<Teacher>();

            // use teacher data controller

            TeacherDataController Controller = new TeacherDataController();

            Teachers = (List<Teacher>)Controller.ListTeachers(SearchKey);


            return View(Teachers);
        }

        // Get: /Teacher/Show/{TeacherId}

        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            // getting teacher info ffrom database
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
    }
}