using GestionalePM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionalePM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //===================================================================================================
        public ActionResult create()
        {
            return View();
        }

        // INSERIRE DATI TRASGRESSORE

        [HttpPost]  
        public ActionResult create(Trasgressore t)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command= new SqlCommand();
            
            command.Parameters.AddWithValue("@Nome",t.Nome);
            command.Parameters.AddWithValue("@Cognome",t.Cognome);
            command.Parameters.AddWithValue("@Indirizzo",t.Indirizzo);
            command.Parameters.AddWithValue("@Citta",t.Citta);
            command.Parameters.AddWithValue("@Cap",t.Cap);
            command.Parameters.AddWithValue("@Cod_Fisc",t.Cod_Fisc);
            command.CommandText = "INSERT INTO ANAGRAFICA VALUES(@Nome,@Cognome,@Indirizzo,@Citta,@Cap,@Cod_Fisc)";
            command.Connection= connection;

            int row=command.ExecuteNonQuery();  
            if(row > 0 ) 
            {
                ViewBag.ConfermaMessaggio = "Trasgressore inserito con successo";
            }
            connection.Close();
            return View();
        }
        //================================================================================================0
        public ActionResult InsViolazione()
        {
            return View();
        }

        //INSERIRE LA VIOLAZIONE
        [HttpPost]
        public ActionResult InsViolazione(Violazione v)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Descrizione", v.Descrizione);
            command.CommandText = "INSERT INTO VIOLAZIONE VALUES(@Descrizione)";
            command.Connection= connection; 

            int row=command.ExecuteNonQuery();
            if(row > 0 )
            {
                ViewBag.ConfermaMessaggio = "Violazione aggiunta con successo";
            }
            connection.Close(); 
            return View();
        }
        //===================================================================================

        public ActionResult InsVerbale()
        {
            ViewBag.ListaViolazione = Violazione.ListViolazione();
            ViewBag.ListTrasgressore = Trasgressore.SelectTrasgressore();
            return View();
        }

        [HttpPost]
         
        //REGISTRARE VERBALE
        public ActionResult InsVerbale(Verbale verb)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB_polizia"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@DataViolazione",verb.DataViolazione);
            command.Parameters.AddWithValue("@Indirizzo",verb.Indirizzo);
            command.Parameters.AddWithValue("@NominativoAgente",verb.NominativoAgente);
            command.Parameters.AddWithValue("@DataTrascrizioneVerbale",verb.DataTrascrizioneVerbale);
            command.Parameters.AddWithValue("@Importo",verb.Importo);
            command.Parameters.AddWithValue("@Punti",verb.Punti);
            command.Parameters.AddWithValue("@IdViolazione", verb.IdViolazione);
            command.Parameters.AddWithValue("@IdTrasgressore", verb.IdTrasgressore);

            command.CommandText = "INSERT INTO Verbale VALUES(@DataViolazione,@Indirizzo,@NominativoAgente,@DataTrascrizioneVerbale,@Importo,@Punti,@IdViolazione,@IdTrasgressore)";
            command.Connection = connection;

            int row = command.ExecuteNonQuery();
            if (row > 0)
            {
                ViewBag.ListaViolazione = Violazione.ListViolazione();
                ViewBag.ListTrasgressore = Trasgressore.SelectTrasgressore();
                ViewBag.ConfermaMessaggio = "Verbale inserito con successo";
            }
            connection.Close();
            return View();
        }
        public ActionResult RegistroVerbale() 
        {
            return View();
        }


        public ActionResult PartialViewImporto()
        {
            List<Verbale> ListaImportoOver400 = new List<Verbale>();
            ListaImportoOver400 = Verbale.GetVerbaleImporto400();
            return PartialView("_PartialViewImporto",ListaImportoOver400);
        }

        public ActionResult PartialViewPunti()
        {
            List<Verbale> ListaTotPunti = new List<Verbale>();
            ListaTotPunti = Verbale.GetVerbalePuntiTot();
            return PartialView("_PartialViewPunti",ListaTotPunti);
        }
        public ActionResult PartialViewNumVerbali()
        {
            List<Verbale> ListaNumVerbali = new List<Verbale>();
            ListaNumVerbali = Verbale.GetNumVerbali();
            return PartialView("_PartialViewNumVerbali",ListaNumVerbali);
        }
        public ActionResult PartialViewOver10Punti()
        {
            List<Verbale> ListaPuntiOver10 = new List<Verbale>();
            ListaPuntiOver10 = Verbale.GetVerbalePuntiOver10();

            return PartialView("_PartialViewOver10Punti", ListaPuntiOver10);
        }

    }
}