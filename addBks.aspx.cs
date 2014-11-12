using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment3
{
    public partial class addBks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr2 = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            SqlConnection con2 = new SqlConnection(connStr2);
            string sql3 = "select CAT_NAME, CAT_ID from Category";
            SqlDataReader reader;
            try
            {
                con2.Open();
                SqlCommand cmd3 = new SqlCommand(sql3, con2);
                reader = cmd3.ExecuteReader();
                while (reader.Read())
                {
                    cat.Items.Add(new ListItem(reader[0].ToString(), reader[1].ToString()));

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {

                con2.Close();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            string value = cat.SelectedValue.ToString();
            int val = int.Parse(value);
            string sql = "insert into Book (BookTitle, BookAuthor, ISBN, CAT_ID) values ('" + Ttl.Text + "', '" + Auth.Text + "', '" + ISBN.Text + "', "+val+")";
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, con);
            string sql2 = "select BookTitle from Book WHERE BookTitle='" + Ttl.Text + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            



            try
            {
                con.Open();
                if (cmd2.ExecuteScalar() == null)
                {
                    cmd.ExecuteNonQuery();

                    System.Threading.Thread.Sleep(1000);
                    Response.Redirect("addSucc.aspx");
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    Response.Redirect("err3.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}