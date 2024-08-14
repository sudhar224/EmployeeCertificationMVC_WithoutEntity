using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EmployeeCertification_WithoutMVC.Models;

namespace EmployeeCertification_WithoutMVC.Controllers
{
    public class tbl_employeeController : Controller
    {
        string Dbconnection = ("Data Source = DESKTOP-7TUTM2S\\SQLEXPRESS; Initial Catalog = employee; Integrated Security = true;");
        //string Dbconnection = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        //string Dbconnection = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        // GET: tbl_employee
        public ActionResult Index()
        {
            if (Session["uname"] == null)
            {
                return RedirectToAction("Signin","tbl_signup_signin");
            }
            ViewBag.LoggedInUser = Session["uname"];

            List<tbl_employee> tbl_empobj = new List<tbl_employee>();
            using(SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetch_employee", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    tbl_empobj.Add(new tbl_employee 
                    {
                        id = Convert.ToInt32 (sdr["id"]),
                        emp_name = Convert.ToString(sdr["emp_name"]),
                        emp_id = Convert.ToString(sdr[2]),
                        age = Convert.ToInt32(sdr["age"]),
                        designation = Convert.ToString(sdr["designation"]),
                        
                    });
                }
                sdr.Close();
                con.Close();

            }

            return View(tbl_empobj);
        }

        public ActionResult  Logout()
        {
            ViewBag.LoggedInUser = null;
            Session["uname"] = null;
            return RedirectToAction("Signin", "tbl_signup_signin");
        }

        // GET: tbl_employee/Details/5
        public ActionResult Details(int id)
        {
            tbl_employee tbl_empobj = new tbl_employee();
            using(SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetch_employee_id " + id, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    tbl_empobj = new tbl_employee
                    {
						id = Convert.ToInt32(sdr["id"]),
						emp_name = Convert.ToString(sdr["emp_name"]),
						emp_id = Convert.ToString(sdr[2]),
						age = Convert.ToInt32(sdr["age"]),
						designation = Convert.ToString(sdr["designation"])
					};
                        
                    
                }
				sdr.Close();
				con.Close();
			}
            return View(tbl_empobj);
        }

        // GET: tbl_employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_employee/Create
        [HttpPost]
        public ActionResult Create(tbl_employee tbl_empobj)
        {
            try
            {
               using(SqlConnection con = new SqlConnection(Dbconnection))
                {
                    con.Open();
                    string query = "sp_create_employee" + "'" + tbl_empobj.emp_name + "','" + tbl_empobj.emp_id + "'," + tbl_empobj.age + ",'" + tbl_empobj.designation + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_employee/Edit/5
        public ActionResult Edit(int id)
        {
            tbl_employee tbl_empobj = new tbl_employee();
            using(SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetch_employee_id " + id , con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    tbl_empobj = new tbl_employee
                    {
                        id = Convert.ToInt32(sdr[0]),
                        emp_name= Convert.ToString(sdr[1]),
                        emp_id= Convert.ToString(sdr[2]),
                        age = Convert.ToInt32(sdr[3]),
                        designation= Convert.ToString(sdr[4])
                    };
                }
                sdr.Close();
                con.Close();
            }
            return View(tbl_empobj);
        }

        // POST: tbl_employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,tbl_employee tbl_embobj)
        {
            try
            {
               using(SqlConnection con = new SqlConnection(Dbconnection))
                {
                    con.Open();
                    string query = "sp_edit_employee " + id + ",'" + tbl_embobj.emp_name + "','" + tbl_embobj.emp_id + "','" + tbl_embobj.age + "','" + tbl_embobj.designation + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_employee/Delete/5
        public ActionResult Delete(int id)
        {
            tbl_employee tbl_empobj = new tbl_employee();
            using(SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetch_employee_id " + id, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    tbl_empobj = new tbl_employee
                    {
                        id = Convert.ToInt32(sdr[0]),
                        emp_name = Convert.ToString(sdr[1]),
                        emp_id = Convert.ToString(sdr[2]),
                        age = Convert.ToInt32(sdr[3]),
                        designation = Convert.ToString(sdr[4])
                    };
                }
                sdr.Close();
                con.Close();
            }
            return View(tbl_empobj);
        }

        // POST: tbl_employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(Dbconnection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_delete_employee " + id, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


		public ActionResult get_cert(int id)
		{
			List<get_cert> get_certobj = new List<get_cert>();
			using (SqlConnection con = new SqlConnection(Dbconnection))
			{
				con.Open();
				SqlCommand cmd = new SqlCommand("sp_get_cert " + id, con);
				SqlDataReader sdr = cmd.ExecuteReader();
				while (sdr.Read())
				{
					get_certobj.Add(new get_cert
					{
						emp_id = Convert.ToString(sdr[0]),
						emp_name = Convert.ToString(sdr[1]),	
						designation = Convert.ToString(sdr[2]),
						certification = Convert.ToString(sdr[3])

					});
				}
				sdr.Close();
				con.Close();

			}

			return View(get_certobj);
		}
	}
}
