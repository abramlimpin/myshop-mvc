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
    public class UsersController : Controller
    {
        /// <summary>
        /// Displays list of user types from Types table
        /// </summary>
        /// <returns></returns>
        public List<UserType> GetUserTypes()
        {
            var list = new List<UserType>();
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT TypeID, UserType FROM Types
                    ORDER BY UserType";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new UserType
                            {
                                ID = int.Parse(data["TypeID"].ToString()),
                                Name = data["UserType"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public ActionResult Add()
        {
            User record = new User();
            record.UserTypes = GetUserTypes();
            return View(record);
        }
    }
}