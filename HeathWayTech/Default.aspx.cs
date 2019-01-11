using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace HeathWayTech
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetStartedButton_Click(object sender, EventArgs e)
        {
            string userName = this.nameTextBox.Text;

            // log current user
            Session["Data"] = userName;

            VerifyUser(userName);

            Response.Redirect("~/Books.aspx");
        }

        /**
         * Checks if a user is registered in the database
         * 
         * calls the register function for new users
         */
        public void VerifyUser(string userName)
        {
            // connection String
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            // communicate with database
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select id from Users where name = @userName";
                cmd.Parameters.Add(new MySqlParameter("@userName", userName));
                MySqlDataReader myReader = cmd.ExecuteReader();

                // new user
                if (!myReader.HasRows)
                {
                    RegisterUser(con, userName);
                }

                con.Close();
            }
        }

        public void RegisterUser(MySqlConnection con, string userName)
        {
            con.Close();
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into Users (ID, name) values (null, @userName)";
            cmd.Parameters.Add(new MySqlParameter("@userName", userName));
            cmd.ExecuteNonQuery();
        }
    }
}