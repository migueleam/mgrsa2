
using Microsoft.AspNetCore.Mvc.Rendering;
using mgrsa2.Common;
using mgrsa2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace mgrsa2.ViewModels
{
      
    public class SelectListItemPlain
    {
        public int disabled { get; set; } //0,1
        public string group  { get; set; }   
        public int selected { get; set; } //0,1
        public string text { get; set; }
        public string value { get; set; }

    }

    public class Evento
    {
        public Evento()
        {
            this.ID = 0;
        }

        public int ID { get; set; }   //0    
        public string title { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string className { get; set; }
        public string nivelarea { get; set; }
        public string oficinas { get; set; }
        public string depts { get; set; }
        public string usuarios { get; set; }
        public string starth { get; set; }
        public string startmin { get; set; }
        public string endh { get; set; }
        public string endmin { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool active { get; set; }
        public System.DateTime stamp { get; set; }
        public string userstamp { get; set; }
        public System.DateTime edit { get; set; }
        public string useredit { get; set; }
        public bool allday { get; set; }


    }


    public class EventoList
    {
        public EventoList()
        {
            this.resp = new ResponseModel();
            this.eventosl = new List<Evento>();
        }

        public List<Evento> eventosl;
        public List<SelectListItem> usuarios { get; set; }
        public ResponseModel resp { get; set; }

        public Profile user;

    }
    public class Ediciones
    {
        public Ediciones()
        {
            this.response = new ResponseModel();
            this.ediciones = new List<Edicion>();
        }
        public List<Edicion> ediciones { get; set; }
        public ResponseModel response { get; set; }

    }
        
    public class Areas
    {
        public Areas()
        {
            this.response = new ResponseModel();
            this.areas = new List<Area>();
        }
        public List<Area> areas { get; set; }
        public ResponseModel response { get; set; }

    }

    public class Anexo
    {
        public Anexo()
        {
            anexoId = 0;
            articuloId = 0;
            fileName = "";
            extension = "";
            fileDate = "";
            stamp = "";
            stampHora = "";
            stampUserId = 0;
            notas = "";

            this.resp = new ResponseModel();
        }
        public int anexoId { get; set; }
        public int articuloId { get; set; }
        public string fileName { get; set; }
        public string extension { get; set; }
        public string fileDate { get; set; }
        public string notas { get; set; }
        public string stamp { get; set; }
        public string stampHora { get; set; }
        public int stampUserId { get; set; }
        public ResponseModel resp { get; set; }

    }

    public class fileUpload
    {
        public fileUpload()
        {
            this.articuloId = 0;
            this.fileDate = "";
            this.nota = "";
            this.file = null;
        }

        public int articuloId { get; set; }
        public string fileDate { get; set; }
        public string nota { get; set; }
        public IFormFile file { get; set; }

    }


    #region anexos

    public class extensionFile
    {
        public string ext { get; set; }
        public string contentType { get; set; }
    }
    
    #endregion






}


