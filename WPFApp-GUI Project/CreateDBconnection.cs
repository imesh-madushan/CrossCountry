using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace WPFApp_GUI_Project
{
    internal class CreateDBconnection
    {
        string connection = @"Data Source=DESKTOP-PJF3BH1\MSSQLSERVER01; Initial Catalog=CrossCountry; User ID=imesh; Password=imesh78";

        public SqlConnection GetDBconnetion()
        {
            SqlConnection conobj = new SqlConnection(connection);
            conobj.Open();
            return conobj;
        }
    }
}
