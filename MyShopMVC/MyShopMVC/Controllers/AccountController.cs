using MyShopMVC.App_Code;
using MyShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShopMVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User record)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT UserID, TypeID FROM Users
                    WHERE Email=@Email AND Password=@Password
                    AND Status=@Status AND TypeID!=@TypeID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", record.Email);
                    cmd.Parameters.AddWithValue("@Password", Helper.Hash(record.Password));
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@TypeID", 5);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Session["userid"] = data["UserID"].ToString();
                                Session["typeid"] = data["TypeID"].ToString();
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "<div class='alert alert-danger'>Invalid email or password.</div>";
                            return View();
                        }
                    }
                }
            }
        }
    }
}