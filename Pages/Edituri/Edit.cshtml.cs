using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Editura.Pages.Edituri
{
    public class EditModel : PageModel
    {
        public EdituraInfo edituraInfo = new EdituraInfo();
        public string errorMessage = "";
        public string succesMessage = "";
        public void OnGet()
        {
            String ID_Editura = Request.Query["id"].FirstOrDefault() ?? "";

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM EDITURI WHERE ID_Editura=@ID_Editura";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Editura", ID_Editura);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                edituraInfo.ID_Editura = "" + reader.GetInt32(0);
                                edituraInfo.name = reader.GetString(1);
                                edituraInfo.adress = reader.GetString(2);
                                edituraInfo.contact = reader.GetString(3);
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

        public void OnPost() {
            edituraInfo.ID_Editura = Request.Form["ID_Editura"];
            edituraInfo.name = Request.Form["Nume"]; 
            edituraInfo.adress = Request.Form["adresa"]; 
            edituraInfo.contact = Request.Form["contact"];
            if (edituraInfo.ID_Editura.Length == 0 || edituraInfo.name.Length == 0 || edituraInfo.adress.Length == 0 || edituraInfo.contact.Length == 0)
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
                    String sql = "UPDATE EDITURI " +   
                                 "SET Nume=@Nume, adresa=@adresa, contact=@contact " +
                                 " WHERE ID_Editura=@ID_Editura";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nume", edituraInfo.name);
                        command.Parameters.AddWithValue("@adresa", edituraInfo.adress);
                        command.Parameters.AddWithValue("@contact", edituraInfo.contact);
                        command.Parameters.AddWithValue("@ID_Editura", edituraInfo.ID_Editura);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=   ex.Message;
                return;


            }
            Response.Redirect("/Edituri/Index");
        }
    }

}
