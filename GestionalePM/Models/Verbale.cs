using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace GestionalePM.Models
{
    public class Verbale
    {
        public int IDverbale { get; set; }
        [Display(Name="Data Violazione")]
        public DateTime DataViolazione { get; set; }

        public string Indirizzo { get; set; }

        [Display(Name ="Nominativo Agente")]
        public string NominativoAgente { get; set; }
        [Display(Name = "Data Verbale")]
        public DateTime DataTrascrizioneVerbale { get; set; }

        public decimal Importo { get; set; }

        public int Punti { get; set; }
        [Display(Name = "Tipo di violazione")]
        public int IdViolazione { get; set; }
        [Display(Name = "Trasgressore")]
        public int IdTrasgressore { get; set; }

        public int NUM_VERBALI { get; set; }

        public Trasgressore Trasgressore { get; set; }

        public static List<Verbale> GetVerbale() 
        {
            List<Verbale>ListaVerbale = new List<Verbale>();


            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Verbale inner join Anagrafica on Verbale.IdTrasgressore= Anagrafica.IDtrasgressore inner join Violazione ON Verbale.IdViolazione= Violazione.IDviolazione";
         
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) 
            {
            while(reader.Read()) 
                { 
                   
                    Verbale verb= new Verbale();
                    verb.IDverbale = Convert.ToInt32(reader["IDverbale"]);
                    verb.DataViolazione= Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                    verb.Indirizzo = reader["Indirizzo"].ToString();
                    verb.NominativoAgente = reader["NominativoAgente"].ToString();
                    verb.DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                    verb.Importo = Convert.ToDecimal(reader["Importo"]);
                    verb.Punti= Convert.ToInt32(reader["Punti"]);
                    verb.IdTrasgressore= Convert.ToInt32(reader["IdTrasgressore"]);
                    verb.IdViolazione= Convert.ToInt32(reader["IdViolazione"]);
                    ListaVerbale.Add(verb);

                }
            
            }
            connection.Close();
            return ListaVerbale;

        }
        public static List<Verbale> GetVerbaleImporto400()
        {

            List<Verbale> ImportoVerbale = new List<Verbale>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select Anagrafica.Nome,Anagrafica.Cognome,Verbale.Importo,Verbale.DataViolazione,Verbale.Punti From Anagrafica INNER JOIN Verbale ON Anagrafica.IDtrasgressore=Verbale.IdTrasgressore WHERE importo>400";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Nome = reader["Nome"].ToString();
                t.Cognome = reader["Cognome"].ToString();
                
                Verbale ImportoVerb = new Verbale();
               // ImportoVerb.IDverbale = Convert.ToInt32(reader["IDverbale"]);  
                ImportoVerb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                ImportoVerb.Importo = Convert.ToDecimal(reader["Importo"]);
                ImportoVerb.Punti = Convert.ToInt32(reader["Punti"]);
                ImportoVerb.Trasgressore = t;
               ImportoVerbale.Add(ImportoVerb);
            };
            
            connection.Close();
            return ImportoVerbale;
        }

        public static List<Verbale> GetVerbalePuntiOver10()
        {

            List<Verbale> ImportoVerbale = new List<Verbale>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select Anagrafica.Nome,Anagrafica.Cognome,Verbale.Importo,Verbale.DataViolazione,Verbale.Punti From Anagrafica INNER JOIN Verbale ON Anagrafica.IDtrasgressore=Verbale.IdTrasgressore WHERE Punti>=10";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Nome = reader["Nome"].ToString();
                t.Cognome = reader["Cognome"].ToString();

                Verbale ImportoVerb = new Verbale();
                // ImportoVerb.IDverbale = Convert.ToInt32(reader["IDverbale"]);  
                ImportoVerb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                ImportoVerb.Importo = Convert.ToDecimal(reader["Importo"]);
                ImportoVerb.Punti = Convert.ToInt32(reader["Punti"]);
                ImportoVerb.Trasgressore = t;
                ImportoVerbale.Add(ImportoVerb);
            };

            connection.Close();
            return ImportoVerbale;
        }

        public static List<Verbale> GetVerbalePuntiTot()
        {

            List<Verbale> TotPunti = new List<Verbale>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT SUM(PUNTI) AS TOTPUNTI, ANAGRAFICA.NOME,ANAGRAFICA.COGNOME FROM Anagrafica INNER JOIN VERBALE ON ANAGRAFICA.IDtrasgressore=VERBALE.IdTrasgressore  GROUP BY COGNOME,NOME ORDER BY COGNOME";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Nome = reader["Nome"].ToString();
                t.Cognome = reader["Cognome"].ToString();

                Verbale ImportoVerb = new Verbale();
                // ImportoVerb.IDverbale = Convert.ToInt32(reader["IDverbale"]);  
                ImportoVerb.Punti = Convert.ToInt32(reader["TOTPUNTI"]);
                ImportoVerb.Trasgressore = t;
                TotPunti.Add(ImportoVerb);
            };

            connection.Close();
            return TotPunti;
        }

        public static List<Verbale> GetNumVerbali()
        {

            List<Verbale> NumVerbali = new List<Verbale>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select COUNT(*)as NUM_VERBALI, Anagrafica.Nome,Anagrafica.Cognome FROM Anagrafica INNER JOIN verbale ON Anagrafica.IDtrasgressore=VERBALE.IdTrasgressore Group BY Cognome,Nome ORDER BY Cognome";
            command.Connection = connection;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Nome = reader["Nome"].ToString();
                t.Cognome = reader["Cognome"].ToString();

                Verbale ImportoVerb = new Verbale();
                // ImportoVerb.IDverbale = Convert.ToInt32(reader["IDverbale"]);  
                ImportoVerb.NUM_VERBALI = Convert.ToInt32(reader["NUM_VERBALI"]);
                ImportoVerb.Trasgressore = t;
                NumVerbali.Add(ImportoVerb);
            };

            connection.Close();
            return NumVerbali;
        }
    }
}