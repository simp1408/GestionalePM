using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionalePM.Models
{
    public class Trasgressore
    {
        public int IDtrasgressore { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public string Indirizzo { get; set; }
        public string Citta { get; set; }

        public int Cap { get; set; }

        public string Cod_Fisc { get; set; }

        public static List<Trasgressore> GetTrasgressore()
        {
            List<Trasgressore> trasgressore = new List<Trasgressore>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Anagrafica";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read()) 
                {
                    Trasgressore t = new Trasgressore();
                     t.IDtrasgressore = Convert.ToInt32(reader["IDtrasgressore"]);
                    t.Nome = reader["Nome"].ToString();
                    t.Cognome = reader["Cognome"].ToString();
                    t.Indirizzo = reader["Indirizzo"].ToString();
                    t.Citta = reader["Citta"].ToString();
                    t.Cap = Convert.ToInt32(reader["Cap"]);
                    t.Cod_Fisc = reader["Cod_Fisc"].ToString();
                    trasgressore.Add(t);
              
                }
            }
            connection.Close();
            return trasgressore;
            
        }

        public static List<SelectListItem> SelectTrasgressore() 
        {
            List<SelectListItem>selectItems = new List<SelectListItem>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Anagrafica";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SelectListItem selectitems = new SelectListItem
                    {
                        Value = reader["IDtrasgressore"].ToString(),
                        Text = reader["Cognome"].ToString() + " " + reader["Nome"].ToString(),
                    };

                    selectItems.Add(selectitems);

                }
            }
            connection.Close();
            return selectItems;

        }
    }
}