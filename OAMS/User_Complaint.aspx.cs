﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Button = System.Web.UI.WebControls.Button;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace OAMS
{
    public partial class User_Complaint : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Add("Database", "OAMS");
            connBuilder.Add("Data Source", "contra.cjrbdmxkv84s.ap-south-1.rds.amazonaws.com");
            connBuilder.Add("User Id", "admin");
            connBuilder.Add("Password", "MiloniMadhav");

            MySqlConnection connection = new MySqlConnection(connBuilder.ConnectionString);

            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "select Email_ID,Name,time,consultant_master.uid,DATE(appointment_master.date) as 'AppDate' from OAMS.appointment_master join OAMS.consultant_master on appointment_master.cid=consultant_master.cid join OAMS.user_master on consultant_master.uid=user_master.uid where appointment_master.uid=" + Session["uid"] + ";";
            connection.Open();
            complaint.DataSource = cmd.ExecuteReader();

            complaint.DataBind();
            connection.Close();
        }
        protected void Complaint_Insert(object sender, EventArgs e)
        {
            try
            {
                MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();

                connBuilder.Add("Database", "OAMS");
                connBuilder.Add("Data Source", "contra.cjrbdmxkv84s.ap-south-1.rds.amazonaws.com");
                connBuilder.Add("User Id", "admin");
                connBuilder.Add("Password", "MiloniMadhav");

                MySqlConnection connection = new MySqlConnection(connBuilder.ConnectionString);
                MySqlCommand cmd = connection.CreateCommand();

                RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

                DropDownList val = item.FindControl("type") as DropDownList;
                TextBox val2 = item.FindControl("desc") as TextBox;
                TextBox val3 = item.FindControl("uuid") as TextBox;
                String a = val.SelectedItem.Value;
                String b = val2.Text;
                String c = val3.Text;

                cmd.CommandText = "insert into OAMS.complaint_master(uid,type,description,from_uid) values("+ c +",'"+ a +"','"+ b +"','"+Session["uid"]+"');";
                connection.Open();
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Complaint submitted successfully");
                }
                else
                {
                    MessageBox.Show("error");
                }
                connection.Close();


            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

    }
}