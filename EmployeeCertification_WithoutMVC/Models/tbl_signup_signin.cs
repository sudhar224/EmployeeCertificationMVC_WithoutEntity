using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeCertification_WithoutMVC.Models
{
	public class tbl_signup_signin
	{
		public int id { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string mobile { get; set; }
		public string address { get; set; }
	}
}