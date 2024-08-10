using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Form2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (!validateForm())
            {
                return;
            }
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection con = new SqlConnection(sqlConnectionString);

            try
            {

                string query = @"
                INSERT INTO Form Data (FirstName, MiddleName, LastName, PhoneNumber, DateOfBirth, EmailID,  Address,  Sex, Stream)
                VALUES (@FirstName, @MiddleName, @LastName, @PhoneNumber, @DateOfBirth , @EmailID, @Address, @Sex, @Stream)";


                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@MiddleName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", TextBox4.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(TextBox5.Text));

                cmd.Parameters.AddWithValue("@EmailID", TextBox6.Text);
                cmd.Parameters.AddWithValue("@Address", TextBox7.Text);
                string sex = "";
                if (RadioButton1.Checked)
                {
                    sex = "Male";
                }
                else if (RadioButton2.Checked)
                {
                    sex = "Female";
                }


                cmd.Parameters.AddWithValue("@Sex", sex);

                cmd.Parameters.AddWithValue("@Stream", DropDownList1.SelectedValue);


                con.Open();


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageLabel.Text = "Successfully inserted!";

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    RadioButton1.Checked = false;

                    RadioButton2.Checked = false;
                    DropDownList1.SelectedIndex = 0;
                }
                else
                {
                    MessageLabel.Text = "No rows were inserted. Please check your data.";
                }
            }
            catch (SqlException ex)
            {
                MessageLabel.Text = "An error occurred: " + ex.Message;
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        Boolean validateForm()
        {
            if(TextBox1.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give first Name";
                return false;
            }else if(TextBox3.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give last Name";
                return false;
            }
            else if (TextBox4.Text.Length != 10)
            {
                MessageLabel.Text = "Phone Number must be of length 10";
                return false;
            }else if(TextBox5.Text.Length == 0)
            {
                MessageLabel.Text = "Please give Date Of Birth";
                return false;
            }
            else if(TextBox6.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give your email";
                return false;
            }
            else if(TextBox7.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give your address";
                return false;
            }
            else if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                MessageLabel.Text = "Please give your gender";
                return false;
            }
            else if(DropDownList1.SelectedIndex == 0)
            {
                MessageLabel.Text = "Please slect a stream";
                return false;
            }
            return true;
        }
    }
}
    
