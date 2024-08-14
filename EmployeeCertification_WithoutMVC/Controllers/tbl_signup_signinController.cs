using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Xml.Linq;
using EmployeeCertification_WithoutMVC.Models;

namespace EmployeeCertification_WithoutMVC.Controllers
{
	public class tbl_signup_signinController : Controller
    {
        string Dbconnection = ("Data Source = DESKTOP-7TUTM2S\\SQLEXPRESS; Initial Catalog = employee; Integrated Security = true;");
        //string  Dbconnection = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
		//string Dbconnection = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        


		// GET: tbl_signup_signin
		public ActionResult Index()
        {
            return View();
        }

        // GET: tbl_signup_signin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: tbl_signup_signin/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: tbl_signup_signin/Create
        [HttpPost]
        public ActionResult Signup(tbl_signup_signin tbl_signup_obj)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(Dbconnection))
                {
                    con.Open();
                    string query = "sp_signup '" + tbl_signup_obj.username + "','" + tbl_signup_obj.password + "','" + tbl_signup_obj.mobile + "','" + tbl_signup_obj.address + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        ViewBag.successMessage = "success";
                    }
                    else
                    {
						ViewBag.successMessage = "unsuccess";
					}
                    con.Close();
				}
                return View();
                //return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

		public ActionResult Signin()
		{
			return View();
		}

		// POST: tbl_signup_signin/Create
		[HttpPost]
		public ActionResult Signin(tbl_signup_signin tbl_signin_obj)
		{
            string username = "";
			try
			{
				using (SqlConnection con = new SqlConnection(Dbconnection))
				{
					con.Open();
					string query = "sp_signin " + "'" + tbl_signin_obj.username + "','" + tbl_signin_obj.password + "'";
					SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        username = sdr[0].ToString();
                        Session["uname"] = username;
                    }
					if(username != "")
                    {
                        ViewBag.LoginSuccess = "success";
                        return RedirectToAction("Index", "tbl_employee");
                        
                    }
					else
                    {
                        ViewBag.LoginSuccess = "unsuccess";
						//return RedirectToAction("Signin", "tbl_signup_signin");
                    }
				}
				return View();
				//return RedirectToAction();
			}
			catch(Exception ex)
			{
				return View(ex);
			}
		}

		// GET: tbl_signup_signin/Edit/5
		public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tbl_signup_signin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_signup_signin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tbl_signup_signin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
			}
		}
    }
}
	//< connectionStrings >
	//	< add name = "Dbconnection"
	//		 connectionString = "Data Source=DESKTOP-7TUTM2S\\SQLEXPRESS;Initial Catalog=employee;Integrated Security=True;"
	//		 providerName = "System.Data.SqlClient" />
	//</ connectionStrings >
