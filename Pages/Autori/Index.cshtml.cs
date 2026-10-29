using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Editura.Pages.Autori
{
    public class IndexModel : PageModel
    {
        public List<AutoriInfo> listAutori = new List<AutoriInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM Autori";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AutoriInfo autorInfo = new AutoriInfo();
                                autorInfo.ID_Autor = "" + reader.GetInt32(0);
                                autorInfo.nume = reader.GetString(1);
                                autorInfo.adresa = reader.GetString(2);
                                autorInfo.contacte = reader.GetString(3);

                                listAutori.Add(autorInfo);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class AutoriInfo
    {
        public String ID_Autor { get; set; }
        public String nume { get; set; }
        public String adresa { get; set; }
        public String contacte { get; set; }

        public AutoriInfo()
        {
            ID_Autor = string.Empty;
            nume = string.Empty;
            adresa = string.Empty;
            contacte = string.Empty;
        }
    }

}