using Editura.Pages.Edituri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Editura.Pages.Autori
{
    public class EditModel : PageModel
    {
        public AutoriInfo autorInfo = new AutoriInfo();
        public string errorMessage = "";
        public string succesMessage = "";
        public void OnGet()
        {
            String ID_Autor = Request.Query["id"].FirstOrDefault() ?? "";

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Autori WHERE ID_Autor=@ID_Autor";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Autor", ID_Autor);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                autorInfo.ID_Autor = "" + reader.GetInt32(0);
                                autorInfo.nume = reader.GetString(1);
                                autorInfo.adresa = reader.GetString(2);
                                autorInfo.contacte = reader.GetString(3);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            autorInfo.ID_Autor = Request.Form["ID_Autor"];
            autorInfo.nume = Request.Form["Nume"];
            autorInfo.adresa = Request.Form["adresa"];
            autorInfo.contacte = Request.Form["contact"];
            if (autorInfo.ID_Autor.Length == 0 || autorInfo.nume.Length == 0 || autorInfo.adresa.Length == 0 || autorInfo.contacte.Length == 0)
            {
                errorMessage = "Toate campurile trebuie completate";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Autori " +
                                 "SET Nume=@Nume, adresa=@adresa, contact=@contact " +
                                 " WHERE ID_Autor=@ID_Autor";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nume", autorInfo.nume);
                        command.Parameters.AddWithValue("@adresa", autorInfo.adresa);
                        command.Parameters.AddWithValue("@contact", autorInfo.contacte);
                        command.Parameters.AddWithValue("@ID_Autor", autorInfo.ID_Autor);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;


            }
            Response.Redirect("/Autori/Index");
        }
    }

}
