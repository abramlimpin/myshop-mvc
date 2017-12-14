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
        /// <summary>
        /// Checks existing email record from Users table
        /// </summary>
        /// <param name="email">User input</param>
        /// <returns>Email record is existing.</returns>
        bool IsExisting(string email)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT UserID FROM Users WHERE Email=@Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    return cmd.ExecuteScalar() == null ? false : true;
                }
            }
        }

        public ActionResult Add()
        {
            User record = new User();
            record.UserTypes = GetUserTypes();
            return View(record);
        }

        [HttpPost]
        public ActionResult Add(User record)
        {
            if (IsExisting(record.Email))
            {
                ViewBag.Message = "<div class='alert alert-danger'>Email address already existing.</div>";
                record.UserTypes = GetUserTypes();
                return View(record);
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
                {
                    con.Open();
                    string query = @"INSERT INTO Users VALUES
                    (@TypeID, @Email, @Password, @FirstName, @LastName,
                    @Street, @Municipality, @City,
                    @Phone, @Mobile, @Status,
                    @DateAdded, @DateModified)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeID", record.TypeID);
                        cmd.Parameters.AddWithValue("@Email", record.Email);
                        cmd.Parameters.AddWithValue("@Password", Helper.Hash(record.Password));
                        cmd.Parameters.AddWithValue("@FirstName", record.FN);
                        cmd.Parameters.AddWithValue("@LastName", record.LN);
                        cmd.Parameters.AddWithValue("@Street", record.Street);
                        cmd.Parameters.AddWithValue("@Municipality", record.Municipality);
                        cmd.Parameters.AddWithValue("@City", record.City);
                        cmd.Parameters.AddWithValue("@Phone", record.Phone);
                        cmd.Parameters.AddWithValue("@Mobile", record.Mobile);
                        cmd.Parameters.AddWithValue("@Status", "Active");
                        cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                        cmd.ExecuteNonQuery();

                        return RedirectToAction("Index");
                    }
                }
            }
        }

        public ActionResult Index()
        {
            var list = new List<User>();
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT u.UserID, t.UserType, u.FirstName, u.LastName,
                    u.Email, u.Street, u.Municipality, u.City,
                    u.Phone, u.Mobile, u.Status,
                    u.DateAdded, u.DateModified
                    FROM Users u
                    INNER JOIN Types t ON u.TypeID = t.TypeID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new User
                            {
                                ID = int.Parse(data["UserID"].ToString()),
                                UserType = data["UserType"].ToString(),
                                FN = data["FirstName"].ToString(),
                                LN = data["LastName"].ToString(),
                                Email = data["Email"].ToString(),
                                Street = data["Street"].ToString(),
                                Municipality = data["Municipality"].ToString(),
                                City = data["City"].ToString(),
                                Phone = data["Phone"].ToString(),
                                Mobile = data["Mobile"].ToString(),
                                Status = data["Status"].ToString(),
                                DateAdded = DateTime.Parse(data["DateAdded"].ToString()),
                                DateModified = data["DateModified"].ToString() == "" ? (DateTime?)null :
                                    DateTime.Parse(data["DateModified"].ToString())
                            });
                        }
                    }
                }
            }
            return View(list);
        }
    }
}