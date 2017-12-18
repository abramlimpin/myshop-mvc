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
    public class SuppliersController : Controller
    {

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Supplier record)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO Suppliers VALUES
                    (@CompanyName, @ContactPerson, @Address,
                    @Phone, @Mobile, @Status,
                    @DateAdded, @DateModified)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", record.Company);
                    cmd.Parameters.AddWithValue("@ContactPerson", record.Contact);
                    cmd.Parameters.AddWithValue("@Address", record.Address);
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

        public ActionResult Index()
        {
            var list = new List<Supplier>();
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT SupplierID, CompanyName, ContactPerson,
                    Address, Phone, Mobile, Status,
                    DateAdded, DateModified
                    FROM Suppliers 
                    WHERE Status!=@Status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new Supplier
                            {
                                ID = int.Parse(data["SupplierID"].ToString()),
                                Company = data["CompanyName"].ToString(),
                                Contact = data["ContactPerson"].ToString(),
                                Address = data["Address"].ToString(),
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

        public List<SelectListItem> GetStatus()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Active", Value = "Active" });
            list.Add(new SelectListItem() { Text = "Inactive", Value = "Inactive" });
            list.Add(new SelectListItem() { Text = "Archived", Value = "Archived" });
            return list;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var record = new Supplier();
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"SELECT SupplierID, CompanyName, ContactPerson,
                    Address, Phone, Mobile, Status,
                    DateAdded, DateModified
                    FROM Suppliers 
                    WHERE SupplierID=@SupplierID AND Status!=@Status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", id);
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                record.ID = int.Parse(data["SupplierID"].ToString());
                                record.Company = data["CompanyName"].ToString();
                                record.Contact = data["ContactPerson"].ToString();
                                record.Address = data["Address"].ToString();
                                record.Phone = data["Phone"].ToString();
                                record.Mobile = data["Mobile"].ToString();
                                record.Status = data["Status"].ToString();
                            }
                            record.AllStatus = GetStatus();
                            return View(record);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Details(Supplier record)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE Suppliers SET CompanyName=@CompanyName, 
                        ContactPerson=@ContactPerson, Address=@Address,
                        Phone=@Phone, Mobile=@Mobile, Status=@Status,
                        DateModified=@DateModified
                        WHERE SupplierID=@SupplierID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", record.Company);
                    cmd.Parameters.AddWithValue("@ContactPerson", record.Contact);
                    cmd.Parameters.AddWithValue("@Address", record.Address);
                    cmd.Parameters.AddWithValue("@Phone", record.Phone);
                    cmd.Parameters.AddWithValue("@Mobile", record.Mobile);
                    cmd.Parameters.AddWithValue("@Status", record.Status);
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SupplierID", record.ID);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (SqlConnection con = new SqlConnection(Helper.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE Suppliers SET Status=@Status, DateModified=@DateModified
                    WHERE SupplierID=@SupplierID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SupplierID", id);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}