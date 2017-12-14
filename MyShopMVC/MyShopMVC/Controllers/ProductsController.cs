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
    public class ProductsController : Controller
    {

        public List<Category> GetCategories()
        {
            var list = new List<Category>();
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT CatID, Category FROM Categories
                    ORDER BY Category";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new Category
                            {
                                ID = int.Parse(data["CatID"].ToString()),
                                Name = data["Category"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public ActionResult Add()
        {
            Product record = new Product();
            record.Categories = GetCategories();
            return View(record);
        }

        [HttpPost]
        public ActionResult Add(Product record, HttpPostedFileBase image)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO Products VALUES
                    (@Name, @CatID, @Code, @Description, @Image,
                    @Price, @IsFeatured, @Available, @CriticalLevel,
                    @Maximum, @Status, @DateAdded, @DateModified)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@CatID", record.CatID);
                    cmd.Parameters.AddWithValue("@Code", record.Code);
                    cmd.Parameters.AddWithValue("@Description", record.Description);
                    cmd.Parameters.AddWithValue("@Image", DateTime.Now.ToString("yyyyMMddhhmmss-") +
                        image.FileName);
                    image.SaveAs(Server.MapPath("~/Images/Products/" + DateTime.Now.ToString("yyyyMMddhhmmss-") +
                        image.FileName));
                    cmd.Parameters.AddWithValue("@Price", record.Price);
                    cmd.Parameters.AddWithValue("@IsFeatured", record.IsFeatured ? "Yes" : "No");
                    cmd.Parameters.AddWithValue("@Available", 0);
                    cmd.Parameters.AddWithValue("@CriticalLevel", record.Critical);
                    cmd.Parameters.AddWithValue("@Maximum", record.Max);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}