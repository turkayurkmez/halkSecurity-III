using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=Northwind;Integrated Security=True");

        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers WHERE CustomerID=@custid AND City=@pass", sqlConnection);

        sqlCommand.Parameters.AddWithValue("@custid", TextBoxUserName.Text);
        sqlCommand.Parameters.AddWithValue("@pass", TextBoxPassword.Text);


        sqlConnection.Open();
        var reader = sqlCommand.ExecuteReader();
        if (reader.Read())
        {
            Label1.Text = "Giriş başarılı! ";
        }
        else
        {
            Label1.Text = "kullanıcı adı veya şifre yanlış";
        }
    }
}