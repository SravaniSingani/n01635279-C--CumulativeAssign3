using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolDbAssignmnet.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SchoolDbAssignmnet.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// List of Tecahers in the Database
        /// </summary>
        /// <example>
        /// GET api/TeacherData/ListTeachers -->
        /// {{"TeacherId":"1","TecaherFname":  

        /// </example>
        /// <returns>
        /// Returns a list of Tecahers
        /// </returns>

        [HttpGet]
        [Route("api/teacherdata/listteachers/{SearchKey?}")]
     
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create a connection with database
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish a command for db
            MySqlCommand cmd = Conn.CreateCommand();

            //Query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like  lower(@key) or lower(teacherlname) like lower(@key) or concat(teacherfname,' ',teacherlname) like lower(@key)";
            cmd.Parameters.AddWithValue("key", "%"+SearchKey+"%");
            cmd.Prepare();

            //Store the result in a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create a list to store Tecaher names
            List<Teacher> Teachers = new List<Teacher>{};

            //Loop till the end of the list
            while (ResultSet.Read())
            {
                //Access columns 
                string TeacherName = ResultSet["teacherfname"]+" " + ResultSet["teacherlname"];
               decimal Salary = Convert.ToDecimal(ResultSet["salary"]);
                int TecaherId = Convert.ToInt32(ResultSet["teacherid"]);
               // string HireDate = ResultSet["hiredate"].ToString();
              //  string ClassName = ResultSet["classname"].ToString();

                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TecaherId;
                NewTeacher.TeacherName = TeacherName;
               NewTeacher.Salary = Salary;
              //  NewTeacher.HireDate = HireDate;
              //  NewTeacher.ClassName = ClassName;


                //Add the teacher name to the list
                Teachers.Add(NewTeacher);
            }

            //Close the connection between Database and server
            Conn.Close();

            //Return the list of teacher names
            return Teachers;
        }
     

  
        ///<summary>
        ///Finidng a Teacher through an ID
        ///</summary>
        ///<param name="id"> The teacher id </param>
        ///<return>
        /// Returns the Tecaher object
        /// </return>>

        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]
        public Teacher FindTeacher(int TeacherId)
        {
            Teacher SelectedTeacher = new Teacher();

            //Creating connection instance
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between dtatabase and the server
            Conn.Open();

            //Establish a command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Query
            cmd.CommandText = "SELECT t.teacherid, CONCAT(t.teacherfname,\" \" ,t.teacherlname) AS teachername , t.salary AS salary, t.hiredate AS hiredate, c.classname AS classname FROM teachers t JOIN classes c ON c.teacherid = t.teacherid WHERE t.teacherid =" + TeacherId + " GROUP BY t.teacherid; " ;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while(ResultSet.Read()) 
            {
                //Access columns 
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherName = ResultSet["teachername"].ToString();
                SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
                SelectedTeacher.HireDate = ResultSet["hiredate"].ToString();
                //initiative results course name specified for the teacher
                SelectedTeacher.ClassName = ResultSet["classname"].ToString();


            }
            // Closing server and database connection
            Conn.Close();
            return SelectedTeacher;

        }


    }
}