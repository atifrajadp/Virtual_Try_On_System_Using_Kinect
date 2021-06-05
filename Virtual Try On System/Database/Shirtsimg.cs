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
    public abstract class Shirtimg
    {

        public void fetch()
        {
         string ConStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Muhammadatif\Documents\im.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("SELECT picture FROM img where id=2"  , con));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "img");
            if (dataSet.Tables["img"].Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream((byte[])dataSet.Tables["img"].Rows[0]["picture"]);
                ms.Position = 0;
                       
            }
          
            con.Close();
}
    }
}
