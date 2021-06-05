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
    public class Datainsertion
    {

     //Insert's data into Database 
        public Datainsertion( string pathformodel,string pathformaterial,string pathforimage,string query,string colonetoadd,string coltwotoadd,string colthreetoadd,string colfourtoadd,string colfivetoadd)
        {
            string cloth_filename = Path.GetFileName(pathformodel);
            string cloth_model = Path.GetFullPath(pathformodel);
            string material_filename = Path.GetFileName(pathformaterial);
            string material_file = Path.GetFullPath(pathformaterial);
            string material_image = Path.GetFullPath(pathforimage);



            byte[] contents_of_model = File.ReadAllBytes(cloth_model);
            byte[] contents_of_material = File.ReadAllBytes(material_file);
            byte[] contents_of_image = File.ReadAllBytes(material_image);
            string insertStmt = query;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
            using (SqlCommand cmdInsert = new SqlCommand(insertStmt, connection))
            {
                cmdInsert.Parameters.Add(colonetoadd, SqlDbType.VarChar, 20).Value = cloth_filename;
                cmdInsert.Parameters.Add(coltwotoadd, SqlDbType.VarBinary, int.MaxValue).Value = contents_of_model;
                cmdInsert.Parameters.Add(colthreetoadd, SqlDbType.VarChar, 20).Value = material_filename;
                cmdInsert.Parameters.Add(colfourtoadd, SqlDbType.VarBinary, int.MaxValue).Value = contents_of_material;
                cmdInsert.Parameters.Add(colfivetoadd, SqlDbType.VarBinary, int.MaxValue).Value = contents_of_image;
                connection.Open();
                cmdInsert.ExecuteNonQuery();
                connection.Close();

            }   
            }
        }
    }

