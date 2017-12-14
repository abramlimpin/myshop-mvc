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
    }
}