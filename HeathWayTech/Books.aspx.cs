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
    public partial class Books : System.Web.UI.Page
    {
        int numberOfBooksLoaded = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUnratedBooks();
        }

        public void LoadUnratedBooks()
        {
            // connection String
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            // communicate with database
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string userName = Session["Data"].ToString();

                // current user's id
                string querryString = "select id from Users where name = \'" + userName + "\'";
                MySqlDataReader myReader = QuerryDataBase(con, querryString);
                myReader.Read();
                int userID = myReader.GetInt32(0);

                // select details of books user has not rated
                querryString = "select title, year, author from Books where id not in (" +
                        "select bookid from Ratings where userid = " + userID + ")";
                myReader = QuerryDataBase(con, querryString);

                string literalString;
                string literalID;
                int i = 0;

                // User is yet to rate some books in database
                if (myReader.HasRows)
                {
                    
                    while (myReader.Read())
                    {
                        List<int> list = new List<int>();
                        list.Add(1);
                        list.Add(2);
                        list.Add(3);
                        list.Add(4);
                        list.Add(5);

                        // Book Name
                        literalString = "<h2>";
                        literalID = "litOpenTag" + i;
                        AddLiteralToPanel(literalString, literalID);
                        literalID = "litName" + i;
                        literalString = myReader.GetString(0);
                        AddLiteralToPanel(literalString, literalID);
                        literalString = "</h2>";
                        literalID = "litCloseTag" + i;
                        AddLiteralToPanel(literalString, literalID);

                        literalString = "<br/>";

                        // Book Author
                        if (!myReader.IsDBNull(2))
                        {
                            literalString = myReader.GetString(2) + "&nbsp";
                            literalID = "litAuthor" + i;
                            AddLiteralToPanel(literalString, literalID);

                        }
                        
                        // Book Year
                        if (!myReader.IsDBNull(1))
                        {
                            literalString = myReader.GetString(1);
                            literalID = "litYear" + i;
                            AddLiteralToPanel(literalString, literalID);
                            literalString = "<br/>";
                            literalID = "litLineBreak" + i;
                            AddLiteralToPanel(literalString, literalID);
                        }

                        RadioButtonList rbl = new RadioButtonList();
                        rbl.RepeatDirection = RepeatDirection.Horizontal;
                        rbl.CssClass = "rbl";
                        rbl.ID = "rblist" + i;
                        rbl.CellPadding = 10;
                        rbl.CellSpacing = 50;
                        rbl.TextAlign = TextAlign.Left;
                        rbl.DataSource = list;
                        rbl.DataBind();
                        Panel1.Controls.Add(rbl);

                        literalString = "<br/>";
                        literalID = "litLineBreak" + i + 1;
                        AddLiteralToPanel(literalString, literalID);

                        numberOfBooksLoaded++;
                        i++;
                    }

                    RateButton.Visible = true;
                }
                // user has rated all books in database
                else
                {
                    literalString = "No more book to rate, please checkback later.";
                    literalID = "litEmpty" + i;
                    AddLiteralToPanel(literalString, literalID);
                    RateButton.Visible = false;
                }
                
                con.Close();
            }
        }

        private void AddLiteralToPanel(string literalString, string literalID)
        {
            Literal lt1 = new Literal();
            lt1.Text = literalString;
            lt1.ID = literalID;
            Panel1.Controls.Add(lt1);
        }

        private MySqlDataReader QuerryDataBase(MySqlConnection con, string querryString)
        {
            con.Close();
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = querryString;
            return cmd.ExecuteReader();
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Ratings.aspx");
        }

        protected void RateButton_Click(object sender, EventArgs e)
        {
            string userName = Session["Data"].ToString();

            // get name and rating of each book
            for(int i = 0; i < numberOfBooksLoaded; i++)
            {
                var radioButtonListId = Panel1.FindControl("rblist" + i) as RadioButtonList;
                // no rating specified
                if(radioButtonListId.SelectedIndex == -1)
                {
                    continue;
                }

                string userRating = radioButtonListId.SelectedValue;

                var bookNameID = Panel1.FindControl("litName" + i) as Literal;
                string bookName = bookNameID.Text;

                AddRatingToDataBase(bookName, userRating, userName);
            }

            Response.Redirect("~/Ratings.aspx");
        }

        private void AddRatingToDataBase(string bookName, string userRating, string userName)
        {
            // connection String
            // update with your mySQL user id, password and database name
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            // communicate with database
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                // user's id
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select id from Users where name = @userName";
                cmd.Parameters.Add(new MySqlParameter("@userName", userName));
                MySqlDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                string userID = myReader.GetString(0);
                con.Close();

                // book's id
                con.Open();
                cmd.CommandText = "select id from books where title = @bookName";
                cmd.Parameters.Add(new MySqlParameter("@bookName", bookName));
                myReader = cmd.ExecuteReader();
                myReader.Read();
                string bookID = myReader.GetString(0);
                con.Close();

                // update ratings table with new entry
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "insert into ratings (userid, bookid, rating, ratingdate) values " + 
                   "(@userID, @bookID, @userRating, current_date())";
                cmd.Parameters.Add(new MySqlParameter("@userID", userID));
                cmd.Parameters.Add(new MySqlParameter("@bookID", bookID));
                cmd.Parameters.Add(new MySqlParameter("@userRating", userRating));
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}