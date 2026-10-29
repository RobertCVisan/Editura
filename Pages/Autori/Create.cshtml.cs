using Editura.Pages.Edituri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Editura.Pages.Autori
{
    public class CreateModel : PageModel
    {
        public AutoriInfo autorInfo = new AutoriInfo();
        public string errorMessage = "";
        public string succesMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            autorInfo.nume = Request.Form["nume"].FirstOrDefault() ?? string.Empty;
            autorInfo.adresa = Request.Form["adresa"].FirstOrDefault() ?? string.Empty;
            autorInfo.contacte = Request.Form["contact"].FirstOrDefault() ?? string.Empty;
            if (autorInfo.nume.Length == 0 || autorInfo.adresa.Length == 0 || autorInfo.contacte.Length == 0)
            {
                errorMessage = "Toate campurile trebuie completate";
                return;
            }

            //Salvam clientul nou in baza de date
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO AUTORI" +
                                 "(nume,adresa,contact) VALUES " +
                                 "(@nume,@adresa,@contact)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume", autorInfo.nume);
                        command.Parameters.AddWithValue("@adresa", autorInfo.adresa);
                        command.Parameters.AddWithValue("@contact", autorInfo.contacte);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            autorInfo.nume = ""; autorInfo.adresa = ""; autorInfo.contacte = "";
            succesMessage = "Noul Autor a fost adaugat corespunzator";
            Response.Redirect("/Autori/Index");

        }

    }
}