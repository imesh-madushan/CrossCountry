using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApp_GUI_Project
{
    public class IDgenarate
    {
        static CreateDBconnection con = new CreateDBconnection();
        //Genarate Customer ID
        public static string createID(string idType)
        {
            int idcount = -1;
            int idLength = 0;
            string idStart = "null";
            string table = "null";

            if (idType == "Cus_ID")
            {
                idStart = "Cus";
                idLength = 12;
                table = "Customer";
            }

            if (idType == "Address_ID")
            {
                idStart = "Add";
                idLength = 10;
                table = "Customer_Address";
            }

            if (idType == "Order_ID")
            {
                idStart = "Odr";
                idLength = 10;
                table = "Orrder";
            }

            if (idType == "Cart_ID")
            {
                idStart = "Crt";
                idLength = 10;
                table = "Cart";
            }

            if (idType == "Pay_ID")
            {
                idStart = "Pay";
                idLength = 10;
                table = "Payment";
            }

            //If correct idType is passed as parameter an ID will retuen
            if (idType != null && idStart != "null" && idLength != 0 && table != "null")
            {
                Random rnd = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder ID = new StringBuilder(idStart, idLength);  //Creating string using stringbuilder

                while (idcount != 0)
                {
                    while (ID.Length < idLength)
                    {
                        ID.Append(chars[rnd.Next(chars.Length)]); //Add random string to from chars to ID 
                    }

                    try
                    {
                        string sql = $"SELECT COUNT(*) FROM {table} WHERE {idType} = '{ID.ToString()}'"; //Find if the genarated ID is already available in the database
                        SqlCommand cmd = new SqlCommand(sql, con.GetDBconnetion());

                        idcount = (int)cmd.ExecuteScalar(); //Set the result to idcount in int format 

                        if (idcount == 0)
                        {
                            break;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show($"An Error occured while genarating new {idType} ID\n" + ex.Message);
                        break;
                    }
                }

                if (idcount == 0) //When ID genarate success return the ID
                {
                    return ID.ToString();
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }  
        }
    }
}
