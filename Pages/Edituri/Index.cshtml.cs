using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Editura.Pages.Edituri
{
    public class IndexModel : PageModel
    {
        public List<EdituraInfo> listEdituri = new List<EdituraInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM Edituri";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    { 
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        { 
                            while (reader.Read())
                            {
                                EdituraInfo edituraInfo = new EdituraInfo();
                                edituraInfo.ID_Editura = "" + reader.GetInt32(0);
                                edituraInfo.name =reader.GetString(1);
                                edituraInfo.adress=reader.GetString(2);
                                edituraInfo.contact=reader.GetString(3);

                                listEdituri.Add(edituraInfo);
                            }
                        
                        }
                    
                    }
                }
                    }
            catch (Exception ex) 
            { 
                Console.WriteLine("Exception: "+ ex.ToString());            
            }
        }
    }
    public class EdituraInfo
    {
        public String ID_Editura { get; set; }
        public String name { get; set; }
        public String adress { get; set; }
        public String contact { get; set; }

        public EdituraInfo()
        {
            ID_Editura = string.Empty;
            name = string.Empty;
            adress = string.Empty;
            contact = string.Empty;
        }
    }

}
