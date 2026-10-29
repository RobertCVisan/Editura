using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Editura.Pages.InterogariComplexe
{
    public class InterogariModel : PageModel
    {
        public List<Tuple<string, string>> QueryResults1 { get; set; }
        public List<Tuple<string, string>> QueryResults2 { get; set; }
        public List<Tuple<string, string>> QueryResults3 { get; set; }
        public List<Tuple<string, string>> QueryResults4 { get; set; }
        
        public void OnGet(string edituraNume)
        {
            //1.Selectam numele autorilor si titlurile cartilor pentru carti publicate de o editura din bucuresti si care sunt publicate pe o data mai mica decat maximul dintre toate anurile de publicare
            ExecuteQuery1("SELECT A.Nume AS NumeAutor, C.Titlu AS TitluCarte " +
              "FROM AUTORI A " +
              "JOIN LEG_CARTI_AUTORI LCA ON A.ID_Autor = LCA.ID_Autor " +
              "JOIN CARTI C ON LCA.ID_Carte = C.ID_Carte " +
              "WHERE C.ID_Editura = (" +
                  "SELECT E.ID_Editura " +
                  "FROM EDITURI E " +
                  "WHERE E.Adresa LIKE '%Bucuresti%' " +
              ") " +
              "AND C.An_Publicare < (" +
                  "SELECT MAX(An_Publicare) " +
                  "FROM CARTI " +
              ");");
            //2.Selectam toate genurile literare care au cel pu?in o carte publicat? de editura cu cele mai multe c?r?i publicate
            ExecuteQuery2("SELECT DISTINCT G.Denumire " +
                         "FROM GEN_LITERAR G " +
                         "JOIN Leg_Carti_Gen LCG ON G.ID_Gen = LCG.ID_Gen " +
                         "JOIN Carti C ON LCG.ID_Carte = C.ID_Carte " +
                         "WHERE C.ID_Editura = (" +
                         "    SELECT TOP 1 E.ID_Editura " +
                         "    FROM Edituri E " +
                         "    JOIN Carti C1 ON E.ID_Editura = C1.ID_Editura " +
                         "    GROUP BY E.ID_Editura " +
                         "    ORDER BY COUNT(C1.ID_Carte) DESC" +
                         ");");
            //3. electam toate c?r?ile scrise de autori care au contribuit la c?r?i publicate de o anumit? editur?
            ExecuteQuery3("SELECT C.Titlu, A.Nume " +
                         "FROM Carti C " +
                         "JOIN Leg_Carti_Autori LCA ON C.ID_Carte = LCA.ID_Carte " +
                         "JOIN Autori A ON LCA.ID_Autor = A.ID_Autor " +
                         "WHERE C.ID_Editura = (" +
                         "    SELECT ID_Editura " +
                         "    FROM Edituri " +
                         $"    WHERE Nume = '{edituraNume}'" +
                         ");");
            //4.Select?m cataloagele care au publicat cel pu?in o carte cu pre? mai mare decat media pre?urilor din celelalte cataloage
            ExecuteQuery4("SELECT CAT.Nume AS NumeCatalog " +
                         "FROM Cataloage CAT " +
                         "JOIN LEG_Carti_Cataloage LCC ON CAT.ID_Catalog = LCC.ID_Catalog " +
                         "JOIN Carti C ON LCC.ID_Carte = C.ID_Carte " +
                         "WHERE C.Pret > (" +
                         "    SELECT AVG(C1.Pret) " +
                         "    FROM Cataloage CAT1 " +
                         "    JOIN LEG_Carti_Cataloage LCC1 ON CAT1.ID_Catalog = LCC1.ID_Catalog " +
                         "    JOIN Carti C1 ON LCC1.ID_Carte = C1.ID_Carte " +
                         "    WHERE CAT1.ID_Catalog <> CAT.ID_Catalog" +
                         ") " +
                         "GROUP BY CAT.Nume;");

        }

        private void ExecuteQuery1(string sql)
        {
            QueryResults1 = new List<Tuple<string, string>>();
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int fieldCount = reader.FieldCount;
                            string headers = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader.GetName(i)));
                            while (reader.Read())
                            {
                                string values = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader[i].ToString()));
                                QueryResults1.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults1.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
        private void ExecuteQuery2(string sql)
        {
            QueryResults2 = new List<Tuple<string, string>>();
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int fieldCount = reader.FieldCount;
                            string headers = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader.GetName(i)));
                            while (reader.Read())
                            {
                                string values = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader[i].ToString()));
                                QueryResults2.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults2.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
        private void ExecuteQuery3(string sql)
        {
            QueryResults3 = new List<Tuple<string, string>>();
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int fieldCount = reader.FieldCount;
                            string headers = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader.GetName(i)));
                            while (reader.Read())
                            {
                                string values = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader[i].ToString()));
                                QueryResults3.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults3.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
        private void ExecuteQuery4(string sql)
        {
            QueryResults4 = new List<Tuple<string, string>>();
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int fieldCount = reader.FieldCount;
                            string headers = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader.GetName(i)));
                            while (reader.Read())
                            {
                                string values = string.Join(",", Enumerable.Range(0, fieldCount).Select(i => reader[i].ToString()));
                                QueryResults4.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults4.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
    }
}
