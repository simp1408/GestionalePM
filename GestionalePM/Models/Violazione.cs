using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GestionalePM.Models
{
    public class Violazione
    {
        public int IDviolazione { get; set; }
        public string Descrizione { get; set; }

        public static List<Violazione> GetViolazione() 
        {
            List<Violazione>ListViolazione= new List<Violazione>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Violazione";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) 
            {
                while(reader.Read()) 
                {
                    Violazione v= new Violazione();
                    v.IDviolazione = Convert.ToInt32(reader["IDviolazione"]);
                    v.Descrizione = reader["Descrizione"].ToString() ;  
                    ListViolazione.Add(v);
                
                }    
            }
            connection.Close();
            return ListViolazione;
        }
        public static List<SelectListItem> ListViolazione() 
        {
            List<SelectListItem> selectItem = new List<SelectListItem> ();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Violazione";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) 
            {
                while(reader.Read()) 
                {
                    SelectListItem selectItems = new SelectListItem
                    {
                        Value = reader["IDviolazione"].ToString(),
                        Text = reader["Descrizione"].ToString()
                    };
                    selectItem.Add(selectItems);
                }
            
            }
            connection.Close();
            return selectItem;
        }
    }
}