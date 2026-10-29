using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace Editura.Pages.Edituri
{
    public class CreateModel : PageModel
    {
        public EdituraInfo edituraInfo = new EdituraInfo();
        public string errorMessage = "";
        public string succesMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() {
            edituraInfo.name = Request.Form["nume"].FirstOrDefault() ?? string.Empty;
            edituraInfo.adress = Request.Form["adresa"].FirstOrDefault() ?? string.Empty;
            edituraInfo.contact = Request.Form["contact"].FirstOrDefault() ?? string.Empty;
            if(edituraInfo.name.Length==0 || edituraInfo.adress.Length==0 || edituraInfo.contact.Length==0) 
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
                    String sql = "INSERT INTO EDITURI" +
                                 "(nume,adresa,contact) VALUES " +
                                 "(@nume,@adresa,@contact)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume", edituraInfo.name);
                        command.Parameters.AddWithValue("@adresa", edituraInfo.adress);
                        command.Parameters.AddWithValue("@contact", edituraInfo.contact);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }
            
            edituraInfo.name = ""; edituraInfo.adress = "";edituraInfo.contact = "";
            succesMessage = "Noua Editura a fost adaugata corespunzator";
            Response.Redirect("/Edituri/Index");

        }

    }
}
