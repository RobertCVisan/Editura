using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Editura.Pages.InterogariSimple
{
    public class InterogariModel : PageModel
    {
        public List<Tuple<string, string>> QueryResults1 { get; set; }
        public List<Tuple<string, string>> QueryResults2 { get; set; }
        public List<Tuple<string, string>> QueryResults3 { get; set; }
        public List<Tuple<string, string>> QueryResults4 { get; set; }
        public List<Tuple<string, string>> QueryResults5 { get; set; }
        public List<Tuple<string, string>> QueryResults6 { get; set; }
        public void OnGet(string autorNume)
        {
            ExecuteQuery1("SELECT A.* " +
                          "FROM Autori A " +
                          "JOIN leg_carti_autori LCA ON A.ID_Autor = LCA.ID_Autor " +
                          "JOIN Carti C ON C.ID_Carte = LCA.ID_Carte " +
                          "WHERE C.An_Publicare > 1900;");

            ExecuteQuery2("SELECT C.Titlu, COUNT(LCA.ID_Autor) AS NumarAutori " +
                          "FROM Carti C " +
                          "JOIN leg_carti_autori LCA ON C.ID_Carte = LCA.ID_Carte " +
                          "GROUP BY C.Titlu;");

            ExecuteQuery3("SELECT C.Titlu, COUNT(LCG.ID_Gen) AS NumarGEN_LITERAR " +
                          "FROM Carti C " +
                          "JOIN LEG_Carti_Gen LCG ON C.ID_Carte = LCG.ID_Carte " +
                          "WHERE C.An_Publicare < 1900 " +
                          "GROUP BY C.Titlu");
            ExecuteQuery4("SELECT TOP 1 E.Nume AS NumeEditura, AVG(C.Pret) AS PretMediu " +
                  "FROM Edituri E " +
                  "JOIN Carti C ON E.ID_Editura = C.ID_Editura " +
                  "GROUP BY E.ID_Editura, E.Nume " +
                  "ORDER BY PretMediu ASC;");
            if (!string.IsNullOrEmpty(autorNume))
            {
                ExecuteQuery5($"SELECT C.Titlu, C.An_Publicare, C.Pret " +
                       "FROM Carti C " +
                       "JOIN leg_carti_autori LCA ON C.ID_Carte = LCA.ID_Carte " +
                       "JOIN Autori A ON A.ID_Autor = LCA.ID_Autor " +
                       $"WHERE A.Nume = '{autorNume}';");
            }
            ExecuteQuery6("SELECT E.Nume AS NumeEditura, C.Titlu AS TitluCarte " +
               "FROM Edituri E " +
               "JOIN Carti C ON E.ID_Editura = C.ID_Editura;");

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
        private void ExecuteQuery5(string sql)
        {
            QueryResults5 = new List<Tuple<string, string>>();
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
                                QueryResults5.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults5.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
        private void ExecuteQuery6(string sql)
        {
            QueryResults6 = new List<Tuple<string, string>>();
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
                                QueryResults6.Add(new Tuple<string, string>(headers, values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryResults6.Add(new Tuple<string, string>("Error", ex.Message));
            }
        }
    }
}