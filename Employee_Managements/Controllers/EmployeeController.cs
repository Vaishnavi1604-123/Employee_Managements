using Employee_Managements.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;

namespace Employee_Managements.Controllers
{
    public class EmployeeController : Controller
    {
        System.Data.SqlClient.SqlConnection _con;
        public ActionResult CreateEmployee()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value="1",Text="HR"},
                new SelectListItem{Value="2",Text="Manager"},
                new SelectListItem{Value="3",Text="Trainee"},
                new SelectListItem{Value="4",Text="Director"},
                new SelectListItem{Value="5",Text="CEO"},
            };
            var employee = new Employee
            {
                DesignationList = new SelectList(items, "Value", "Text")
            };
            return View(employee);
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {

            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            using (SqlCommand command = new SqlCommand("InsertEmployee", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", employee.First_Name);
                command.Parameters.AddWithValue("@LastName", employee.Last_Name);
                command.Parameters.AddWithValue("@DesignationId", employee.DesignationId);
                command.Parameters.AddWithValue("@Experience", employee.Experience);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("EmpDetails");
        }
        public IActionResult EmpDetails()
        {
            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            SqlCommand cmd = new SqlCommand("EmployeeDetails", _con);
            List<EmpDetails> details = new List<EmpDetails>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                details.Add(new EmpDetails
                {
                    Id = (int)reader["Id"],
                    First_Name = (string)reader["First_Name"],
                    Last_Name = (string)reader["Last_Name"],
                    DesignationName = (string)reader["DesignationName"],
                    Experience = (int)reader["Experience"]
                });
            }
            return View(details);
        }
        [HttpGet("{Id}")]
        public ActionResult Delete(int Id)
        {
            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            SqlCommand cmd = new SqlCommand("DelEmp", _con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
            return RedirectToAction("EmpDetails");
            //cmd.Parameters.Add(new SqlParameter("@id", id))

            //[HttpGet("{Id}")]
            //public ActionResult Delete()
            //{
            //    _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            //    _con.Open();
            //    SqlCommand cmd = new SqlCommand("Deleting", _con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.Parameters.AddWithValue("@Id", Id);
            //    cmd.ExecuteNonQuery();
            //    return RedirectToAction("EmpDetails");

            //}
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var items = new List<SelectListItem>
            {
                new SelectListItem{Value="1",Text="HR"},
                new SelectListItem{Value="2",Text="Manager"},
                new SelectListItem{Value="3",Text="Trainee"},
                new SelectListItem{Value="4",Text="Director"},
                new SelectListItem{Value="5",Text="CEO"},
            };
            var employee = new Employee
            {
                DesignationList = new SelectList(items, "Value", "Text")
            };
            ViewData["DesignationList"] = new SelectList(items, "Value", "Text");
            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            SqlCommand cmd = new SqlCommand("Emp", _con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            EmpDetails details = new EmpDetails();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                details.Id = (int)reader["Id"];
                details.First_Name = (string)reader["First_Name"];
                details.Last_Name = (string)reader["Last_Name"];
                details.DesignationName = (string)reader["DesignationName"];
                details.Experience = (int)reader["Experience"];

            }
            return View(details);
        }
        //[HttpPost]
        [HttpPost]
        public ActionResult Edit(int Id, Employee emp)
        {
            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            SqlCommand cmd = new SqlCommand("UpdateEmpDetails", _con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            cmd.Parameters.AddWithValue("@FirstName", emp.First_Name);
            cmd.Parameters.AddWithValue("@LastName", emp.Last_Name);
            cmd.Parameters.AddWithValue("@DesignationName", emp.DesignationName);
            cmd.Parameters.AddWithValue("@Experience", emp.Experience);
            int result=cmd.ExecuteNonQuery();
            _con.Close();
            return RedirectToAction("EmpDetails");

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateAjax()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAjax(string First_Name, string Last_Name, int DesignationId, int Experience)
        {
            _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
            _con.Open();
            using (SqlCommand command = new SqlCommand("InsertEmployee", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", First_Name);
                command.Parameters.AddWithValue("@LastName", Last_Name);
                command.Parameters.AddWithValue("@DesignationId", DesignationId);
                command.Parameters.AddWithValue("@Experience", Experience);
                int result=command.ExecuteNonQuery();
            }
            return View();
        }
       
        //public IActionResult Index()
        //{
        //    _con = new SqlConnection("Data Source=VGATTU-L-5481;Initial Catalog=Employee_Management;User ID=SA;Password=Welcome2evoke@1234");
        //    SqlCommand cmd = new SqlCommand("OderByEmp", _con);
        //    List<EmpDetails> details = new List<EmpDetails>();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        details.Add(new EmpDetails
        //        {
        //            Id = (int)reader["Id"],
        //            First_Name = (string)reader["First_Name"],
        //            Last_Name = (string)reader["Last_Name"],
        //            DesignationName = (string)reader["DesignationName"],
        //            Experience = (int)reader["Experience"]
        //        });
        //    }
        //    return View(details);


        }
        //}
    }


