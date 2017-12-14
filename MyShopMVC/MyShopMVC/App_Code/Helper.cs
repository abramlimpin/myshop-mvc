using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyShopMVC.App_Code
{
    public class Helper
    {
        /// <summary>
        /// Returns the connection string value from web.config file
        /// </summary>
        /// <returns>Database connection string</returns>
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
        }
    }
}