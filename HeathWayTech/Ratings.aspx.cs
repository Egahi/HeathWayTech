using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace HeathWayTech
{
    public partial class Ratings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayBooks();
        }

        public void DisplayBooks()
        {
            // connection String
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            // communicate with database
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // select details of books with ratings higher  than 3
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select title, year, author from Books where id in " +
                        "(select bookid from Ratings where rating > 3) order by title";
                MySqlDataReader myReader = cmd.ExecuteReader();
                
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        // Book Name
                        string litString = "<h2>" + myReader.GetString(0) + "</h2>";
                        AddLiteralToPanel(litString);

                        litString = "<br/>";
                        AddLiteralToPanel(litString);

                        // Book Author
                        if (!myReader.IsDBNull(2))
                        {
                            litString = myReader.GetString(2) + "&nbsp";
                            AddLiteralToPanel(litString);
                        }

                        // Book Year
                        if (!myReader.IsDBNull(1))
                        {
                            litString = myReader.GetString(1);
                            AddLiteralToPanel(litString);
                            litString = "<br/>";
                            AddLiteralToPanel(litString);
                        }
                    }

                    RateButton.Visible = true;
                }
                else
                {
                    string litString = "No book with rating high enough to be displayed";
                    AddLiteralToPanel(litString);
                }
                
                con.Close();
            }
        }

        private void AddLiteralToPanel(string literalString)
        {
            Literal lt1 = new Literal();
            lt1.Text = literalString;
            Panel1.Controls.Add(lt1);
        }

        protected void RateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Books.aspx");
        }
    }
}