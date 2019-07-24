using System;
using System.Collections.Generic;
using mgrsa2.Common;
using mgrsa2.ViewModels;
using System.Data.SqlClient;

namespace mgrsa2.Services
{
    public class EventServices : IEventServices
    {
        private ResponseModel rm;

        public EventServices()
        {
            
        }

        public ResponseModel Delete(Evento evento)
        {
            int iResult = 0;
            rm = new ResponseModel();

            try
            {

                iResult = SqlHelper.ExecuteNonQuery(MySettings.ConnIntra,                                  
                                   "[dbo].sp_intra_evento_Delete",
                                    evento.ID,
                                    evento.useredit
                );

                rm.response = true;

            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }

            return rm;
        }

        public Evento GetEvento(int id)
        {
            Evento evento =  new Evento();

            SqlDataReader reader = SqlHelper.ExecuteReader(
                 MySettings.ConnIntra,
                 "[dbo].sp_intra_evento_GetEventos",
                 id,
                 "0",
                 "0",
                 "0",
                 ""
             );

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    evento.ID = (int)reader["id"];
                    evento.active = (bool)reader["active"];
                    evento.className = reader["classname"].ToString();
                    evento.depts = reader["depts"].ToString();
                    evento.description = reader["description"].ToString();
                    //evento.edit = (DateTime)reader["edit"].ToString();
                    evento.end = reader["end"].ToString();
                    evento.endh = reader["endh"].ToString();
                    evento.endmin = reader["endmin"].ToString();

                    evento.icon = reader["icon"].ToString();
                    evento.nivelarea =  reader["nivelarea"].ToString();
                    evento.oficinas =  reader["oficinas"].ToString();

                    evento.userstamp = reader["userstamp"].ToString();

                    evento.start = reader["start"].ToString();
                    evento.starth = reader["starth"].ToString();
                    evento.startmin = reader["startmin"].ToString();

                    evento.title = reader["title"].ToString();
                    evento.useredit = reader["useredit"].ToString();
                    evento.usuarios = reader["usuarios"].ToString();
                    evento.allday = (bool)reader["allday"];

                }

            }
            reader.Close();

            return evento;
        }

        public ResponseModel Insert(Evento evento)
        {
            int iResult = 0;
            rm = new ResponseModel();

            try
            {
                               
                var vResult = SqlHelper.ExecuteScalar(MySettings.ConnIntra   
                                    , "sp_intra_evento_Insert",

                                   evento.className,
                                   evento.depts,
                                   evento.description,

                                   evento.end,
                                   evento.endh,
                                   evento.endmin,

                                   evento.icon,
                                   evento.nivelarea,
                                   evento.oficinas,

                                   evento.userstamp,

                                   evento.start,
                                   evento.starth,
                                   evento.startmin,

                                   evento.title,
                                   evento.useredit,
                                   evento.usuarios,
                                   (evento.allday) ? 1 : 0
                );

                if (vResult != null)
                    iResult = Convert.ToInt32(vResult);

                rm.identity = iResult;
                rm.response = true;

            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }
            return rm;
        }

        public List<Evento> ListEventos(int id, string nivelarea, string oficinas, string depts, string usuarios, string nivelapp)
        {
            List<Evento> eventos = new List<Evento>();
            Evento evento = null;

            try
            {
                string sqlproc = "[dbo].sp_intra_evento_GetEventos";

                if (nivelapp == "I")
                    sqlproc = "[dbo].sp_intra_evento_GetEventosI";

                SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnIntra
                    , sqlproc
                    , id
                    , nivelarea   
                    , oficinas
                    , depts
                    , usuarios
                 );

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        evento = new Evento();

                        evento.ID = (int)reader["id"];
                        evento.active = (bool)reader["active"];
                        evento.className = reader["classname"].ToString();
                        evento.depts = reader["depts"].ToString();
                        evento.description = reader["description"].ToString();
                        //evento.edit = (DateTime)reader["edit"].ToString();
                        evento.end = reader["end"].ToString();
                        evento.endh = reader["endh"].ToString();
                        evento.endmin = reader["endmin"].ToString();

                        evento.icon = reader["icon"].ToString();
                        evento.nivelarea = reader["nivelarea"].ToString();
                        evento.oficinas = reader["oficinas"].ToString();

                        evento.userstamp = reader["userstamp"].ToString();

                        evento.start = reader["start"].ToString();
                        evento.starth = reader["starth"].ToString();
                        evento.startmin = reader["startmin"].ToString();

                        evento.title = reader["title"].ToString();
                        evento.useredit = reader["useredit"].ToString();
                        evento.usuarios = reader["usuarios"].ToString();
                        evento.allday = (bool)reader["allday"];

                        eventos.Add(evento);

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                rm.response = false;
                rm.message = ex.Message;
            }
            
            return eventos;
        }

        public ResponseModel Update(Evento evento)
        {

           
            rm = new ResponseModel();

            try
            {

                var vResult = SqlHelper.ExecuteScalar(MySettings.ConnIntra
                                   , "sp_intra_evento_Update",
                                   evento.ID,
                                   evento.className,
                                   evento.depts,
                                   evento.description,

                                   evento.end,
                                   evento.endh,
                                   evento.endmin,

                                   evento.icon,
                                   evento.nivelarea,
                                   evento.oficinas,

                                   evento.start,
                                   evento.starth,
                                   evento.startmin,

                                   evento.title,
                                   evento.useredit,
                                   evento.usuarios,
                                   (evento.allday) ? 1 : 0

                );

                rm.response = true;
            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }

            return rm;
        }

        public ResponseModel UpdateNewDate(Evento evento) {

            rm = new ResponseModel();

            try
            {

                var vResult = SqlHelper.ExecuteScalar(MySettings.ConnIntra
                                   , "sp_intra_evento_UpdateNewDate",
                                   evento.ID,
                                 
                                   evento.end,
                                   evento.endh,
                                   evento.endmin,
                                                                
                                   evento.start,
                                   evento.starth,
                                   evento.startmin,
                                                                   
                                   evento.useredit
                                  
                );

                rm.response = true;
            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }

            return rm;


        }
        public ResponseModel SendNotificacion(Evento evento, string email, string subject )
        {
            rm = new ResponseModel();                               
            
            try
            {

               string sbg = "#404040"; //inverse
                if (evento.className.IndexOf("red") >= 0)
                    sbg = "#db4a67";
                else if (evento.className.IndexOf("blue") >= 0)
                    sbg = "#4387bf";
                else if (evento.className.IndexOf("orange") >= 0)
                    sbg = "#d6a848";
                else if (evento.className.IndexOf("pink") >= 0)
                    sbg = "#ac5287";
                else if (evento.className.IndexOf("gray") >= 0)
                    sbg = "#92a2a8";
                else if (evento.className.IndexOf("green") >= 0)
                    sbg = "#92a2a8";



                string sbody = "<h2>"+subject+"</h2>";

                sbody += "<table style='width:80%; background-color:"+ sbg + ";color:#fff; margin-top:20px; padding-left:10px'>" +                    
                    "<tr>" +
                    "    <td colspan='2'><h2>" + evento.title + "</h2></td>" +
                    "</tr>" + 
                    "<tr>" +
                    "    <td colspan='2'><h4>" + evento.description + "</h4></td>" +
                    "</tr>" +
                    "<tr style='height:10px'><td colspan='2'></td></tr>" +
                    "<tr>" +
                    "    <td><h4>Inicio</h4></td><td><h4>Fin</h4></td>" +
                    "</tr>" +
                    "<tr style='height:10px'><td colspan='2'></td></tr>" +
                    "<tr>" +
                    "    <td><h5>" + Common.Global.FormatMomentDate(evento.start, evento.starth,evento.startmin) + " hrs </h5></td>" +
                    "    <td><h5>" + Common.Global.FormatMomentDate(evento.end, evento.endh, evento.endmin) + " hrs</h5></td>" +
                    "</tr>" +                
                    "<tr style='height:10px'><td colspan='2'></td></tr>" +    
                    "</table>";

                sbody += "<h5 style='color:#db4a67;'>Notas: información de El Aviso Magazine @2017</h5>";

                Common.Global.SendMailMessage(
                    "admin@elaviso.com",
                    email,
                    "",
                    "miguel@elaviso.com",
                    "Evento Registrado: " + evento.title,
                    sbody);

                rm.response = true;
                
            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }
            return rm;
        }
    }
}
