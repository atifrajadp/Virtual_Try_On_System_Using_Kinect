using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer;


namespace Virtual_Try_On_System.Database
{
    public class Dataretrieval
    {

        // Retrieves data from the Database
        public Dataretrieval()
        {


            string filename = "tshirt_coral_blue.obj";  //this is the file's name which is stored in database and you want to fetch
            byte[] fileContents;

            string selectStmt = "SELECT Shirt_model FROM CLOTHES WHERE Shirt_name=@Shirt_name ";

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
                using (SqlCommand cmdSelect = new SqlCommand(selectStmt, connection))
                {
                    cmdSelect.Parameters.Add("@Shirt_name", SqlDbType.VarChar).Value = filename;
                    connection.Open();
                    fileContents = (byte[])cmdSelect.ExecuteScalar();

                    
                }
               
                File.WriteAllBytes(@"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\" + filename, fileContents);
               
           
                
            
        }
    }
}
