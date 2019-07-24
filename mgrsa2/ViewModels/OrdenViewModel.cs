using mgrsa2.Common;
using mgrsa2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mgrsa2.ViewModels
{
    public class VideoVM
    {

        public VideoVM()
        {
            this.leiadvertencia = false;
        }

        public int videoId { get; set; }       //0
        public bool promotion { get; set; }      //1 
        public bool leiadvertencia { get; set; }
        public bool client { get; set; }   //
        public string clientID { get; set; }     //
        public string name { get; set; }   //5
        public string contact { get; set; }
        public string frecuencypay { get; set; }
        public string justify { get; set; }
        public int authId { get; set; }
        public string authdt { get; set; }
        public int authgerId { get; set; }
        public string authgerdt { get; set; }
        public string filmaciondt { get; set; }
        public string website { get; set; }
        public string socialmedia { get; set; }
        public string goalVideo { get; set; }
        public string goalClient { get; set; }
        public string agenteId { get; set; }
        public string agente { get; set; }


        public string versionId { get; set; }
        public string estilo { get; set; }
        public string guia { get; set; }
        public string semanas { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }
        public string area { get; set; }
        public string det { get; set; }
        public string costototal { get; set; }
        public string costoun { get; set; }


    }
    public class VideoForm
    {

        public VideoForm()
        {
            leiadvertencia = false;
            this.resp = new ResponseModel();
        }
        [Display(Name = "Id")]
        public int videoId { get; set; }         //0

        [Display(Name = "Video Promoción")]
        public bool promotion { get; set; }      //1 

        [Display(Name = "Lei Advertencia")]
        public bool leiadvertencia { get; set; }

        [Display(Name = "Cliente haciendo Live")]
        public bool client { get; set; }   //2

        [Display(Name = "Cliente")]
        public string clientID { get; set; }     //3               

        [Display(Name = "Nombre de Cliente")]
        public string name { get; set; }   //4

        [Display(Name = "Persona a Contactar")]
        public string contact { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Como esta pagando el cliente (Frecuencia)")]
        public string frecuencypay { get; set; }

        [Required]
        [Display(Name = "Motivo por el que pide hacer video")]
        public string justify { get; set; }

        [Display(Name = "(2) Authorización de Gerencia")]
        public int authId { get; set; }

        [Display(Name = "(2) Fecha de Authorización")]
        public string authdt { get; set; }

        [Display(Name = "(1) Authorización de Manager")]
        public int authgerId { get; set; }

        [Display(Name = "(1) Fecha de Authorización")]
        public string authgerdt { get; set; }

        [Display(Name = "Fecha y hora disponible para ir a filmar")]
        public string filmaciondt { get; set; }

        [Display(Name = "Website de Negocio")]
        [Url]
        public string website { get; set; }

        [Display(Name = "Lista Social Media(s) del Negocio")]
        public string socialmedia { get; set; }

        [Display(Name = "Fin (objetivo) del Video")]
        public string goalVideo { get; set; }

        [Display(Name = "Objetivo del cliente")]
        public string goalClient { get; set; }

        [Display(Name = "Agente")]
        public string agenteId { get; set; }

        [Display(Name = "Agente")]
        public string agente { get; set; }

        public int userId { get; set; }


        [Display(Name = "Codigo")]
        public string versionId { get; set; }

        [Display(Name = "Estilo")]
        public string estilo { get; set; }

        [Display(Name = "Guia")]
        public string guia { get; set; }

        [Display(Name = "Semanas Contratadas")]
        public string semanas { get; set; }

        [Display(Name = "Inicio")]
        public string desde { get; set; }

        [Display(Name = "Terminación")]
        public string hasta { get; set; }
        [Display(Name = "Area")]
        public string area { get; set; }
        [Display(Name = "Detención")]
        public string det { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Costo Total")]
        public string costototal { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Costo Un")]
        public string costoun { get; set; }

        [Display(Name = "Activo")]
        public bool active { get; set; }      //1 


        [Display(Name = "Notas de Gerencia")]
        public string notasgerencia { get; set; }

        [Display(Name = "Notas Supervisor")]
        public string notassupervisor { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public AgentesList agentes { get; set; }
        public ClientesList clientes { get; set; }
        public DesplegadosList desps { get; set; }
        public List<SelectListItem> socialmedias { get; set; }
        public ResponseModel resp { get; set; }

    }

    public class ClienteVM
    {
        public string clientID { get; set; }
        public string empresa { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string tipoProd { get; set; }
        public string phone { get; set; }
        public string agenteId { get; set; }

        public string agente;
        public string Rep { get; set; }
        public string email { get; set; }
        public string ultFechaContrato { get; set; }
        public string vigente { get; set; }  //1 or 0


    }

    public class DesplegadoVM
    {

        public DesplegadoVM()
        {
            this.versionId = "0";
            this.odId = "0";
            this.odAnuncio = "0";
            this.precioUnit = 0.0;
            this.detVisible = true;
        }

        public string versionId { get; set; }       //0 
        public string estilo { get; set; }   //1
        public string color { get; set; }   //2
        public string ordeninsercion { get; set; }
        public string guia { get; set; }      //4 0
        public int semanas { get; set; }        //5 0

        public string fcaptura { get; set; }
        public string agenteId { get; set; }
        public string agente { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }  //10

        public string area { get; set; }
        public string detener { get; set; }
        public string costototal { get; set; }
        public string costoun { get; set; }
        public string vencida { get; set; }    //15 

        public string clientId { get; set; }

        //zvOrdenes
        public string odId { get; set; }
        public string odEdicion { get; set; }
        public string odFecha { get; set; }
        public string odAnuncio { get; set; }  //20
        public string odPublicar { get; set; }
        public string odDetiene { get; set; }
        public double precioUnit { get; set; }
        public bool detVisible { get; set; }


    }

    public class ClientesList
    {
        public ClientesList()
        {
            this.resp = new ResponseModel();
            this.list = new List<SelectListItem>();
        }

        public List<SelectListItem> list;
        public ResponseModel resp { get; set; }

    }

    public class VideoList
    {
        public VideoList()
        {
            this.resp = new ResponseModel();
            this.list = new List<SelectListItem>();
        }

        public List<SelectListItem> list;
        public ResponseModel resp { get; set; }

    }
    public class AgentesList
    {
        public AgentesList()
        {
            this.resp = new ResponseModel();
            this.list = new List<SelectListItem>();
        }

        public List<SelectListItem> list;
        public ResponseModel resp { get; set; }

    }
    public class DesplegadosList
    {
        public DesplegadosList()
        {
            this.resp = new ResponseModel();
            this.list = new List<SelectListItem>();
        }
        public List<SelectListItem> list;
        public ResponseModel resp { get; set; }


    }

    public class Clientes
    {
        public Clientes()
        {
            this.resp = new ResponseModel();
            this.clientes = new List<ClienteVM>();
        }

        public List<ClienteVM> clientes;
        public ResponseModel resp { get; set; }
        public AgentesList agentes { get; set; }

        public Profile agente;

        public Profile user;
    }
    public class Videos
    {
        public Videos()
        {
            this.resp = new ResponseModel();
            this.videos = new List<VideoVM>();
        }

        public List<VideoVM> videos;
        public ResponseModel resp { get; set; }

    }

    public class Desplegados
    {
        public Desplegados()
        {
            this.resp = new ResponseModel();
            this.desplegados = new List<DesplegadoVM>();
        }
        public List<DesplegadoVM> desplegados;
        public ResponseModel resp { get; set; }
        public AgentesList agentes { get; set; }

        public Profile agente;

        public Profile user;

    }


    //////////////////
    ///////
    ///////////////////

    public class EdicionVM
    {
        public string ID { get; set; }
        public string Display { get; set; }
        public string Descripcion { get; set; }
        public string FEdicion { get; set; }

    }


    public class EdicionDVM
    {
        public string edEdicion { get; set; }
        public System.DateTime edFecha { get; set; }
        public string edCerrada { get; set; }
        public string edEdiciones { get; set; }
        public int edicionId { get; set; }
        public string fechaAviso { get; set; }
        public string horaAviso { get; set; }
        public string fechaCierre { get; set; }
        public string horaCierre { get; set; }
        public string Descripcion { get; set; }

    }
    public class AreaVM
    {
        public string areaID { get; set; }
        public string Descripcion { get; set; }
        public bool Activa { get; set; }
        public string Abrev { get; set; }
        public int Orden { get; set; }

    }

    public class EventoVM
    {
        public int eventoID { get; set; }   //0    
        public string title { get; set; }
        public string description { get; set; }
        public string startatdate { get; set; }
        public string startattime { get; set; }
        public string endatdate { get; set; }     //5
        public string endattime { get; set; }
        public bool isfullday { get; set; }
        public string priority { get; set; }
        public bool active { get; set; }

        public string department { get; set; }
        public string nivelArea { get; set; }
        public string area { get; set; }
        public string group { get; set; }


    }
    public class EventList
    {
        public EventList()
        {
            this.resp = new ResponseModel();
            this.eventosl = new List<EventoVM>();
        }

        public List<EventoVM> eventosl;
        public List<SelectListItem> usuarios { get; set; }
        public ResponseModel resp { get; set; }

        public Profile user;

    }
    public class OrdInsVM
    {
        public string odID { get; set; }
        public string edicion { get; set; }   //1
        public bool publicar { get; set; } //2
        public bool publicarY { get; set; }
        public bool publicarN { get; set; }
        public bool definitiva { get; set; }  //5
        public bool temporal { get; set; }
        public string odPublicar { get; set; }
        public string odDetiene { get; set; }
        public string notas { get; set; }
        public string guia { get; set; }  //10
        public string versionID { get; set; }
        public int ultEdicion { get; set; }
        public int ultPage { get; set; }
        public string edicionFin { get; set; }
        public string estilo { get; set; }  //15
        public string area { get; set; }    //16
        public bool enableNuevos { get; set; }  //17      
        public string contestada { get; set; } //18
    }


    public class OrdenInsList
    {
        public OrdenInsList()
        {
            this.resp = new ResponseModel();
            this.ordinsl = new List<OrdInsVM>();
        }

        public List<OrdInsVM> ordinsl;
        public AgentesList agentes { get; set; }
        public ResponseModel resp { get; set; }

        public Profile agente;

        public Profile user;

        public string edicion;


    }


    public class OrdInsH
    {
        public string odId { get; set; }
        public string odEdicion { get; set; }   //1
        public string edFecha { get; set; }
        public string odAnuncio { get; set; }
        public string odPublicar { get; set; }
        public string odDetiene { get; set; }
        public bool detVisible { get; set; }
        public string odAgente { get; set; }
        public string odArea { get; set; }
        public string odEdiAnt { get; set; }
        public string odPagAnt { get; set; }


    }


    public class LastUpdateVM
    {
        public int updated { get; set; }
        public int total { get; set; }
        public string lastupdate { get; set; }

    }

    public class SummOrdIns
    {
        public string edicion { get; set; }
        public string agente { get; set; }   //1
        public string completa { get; set; }
        public string fechaEd { get; set; }
        public int publicados { get; set; }
        public int suspendidos { get; set; }
        public int noContestados { get; set; }

    }

    public class SummOrdInsList
    {
        public SummOrdInsList()
        {
            this.resp = new ResponseModel();
            this.ordinsl = new List<SummOrdIns>();
        }

        public List<SummOrdIns> ordinsl;
        public AgentesList agentes { get; set; }
        public ResponseModel resp { get; set; }

        public Profile agente;

        public Profile user;

    }

    #region  DESPLEGADOS ONLINE

    public class DesplegadosONLI
    {
        public DesplegadosONLI()
        {
            this.resp = new ResponseModel();
            this.despsonli = new List<DesplegadoONLI>();
        }

        public List<DesplegadoONLI> despsonli;
        public AgentesList agentes { get; set; }
        public ResponseModel resp { get; set; }

        public Profile agente;

        public Profile user;

    }

    public class DesplegadoONLI
    {
        public DesplegadoONLI()
        {
            this.versionId = "0";
            this.processed = false;
        }

        public string Id { get; set; }
        public string versionId { get; set; }
        public string clientId { get; set; }
        public string lugarId { get; set; }
        public string guia { get; set; }
        public string ordinsId { get; set; }
        public int numMeses { get; set; }
        public string hasta { get; set; }
        public string desde { get; set; }
        public double costo { get; set; }
        public string agente { get; set; }
        public string empresa { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string maskPhone { get; set; }
        public string email { get; set; }

        // dbo.ap_desponline
        public bool processed { get; set; }
        public bool suspended { get; set; }
        public string notes { get; set; }
        public string website { get; set; }
        public string dateSuspended { get; set; }
        public string usersuspended { get; set; }
        public string dateProcessed { get; set; }


    }


    #endregion


    #region ORDENES DE DISENO


    public class EdicionesList
    {
        public EdicionesList()
        {
            this.resp = new ResponseModel();
            this.list = new List<SelectListItem>();
        }

        public List<SelectListItem> list;
        public ResponseModel resp { get; set; }

    }


    public class OrdenesDiseno
    {
        public OrdenesDiseno()
        {
            this.resp = new ResponseModel();
            this.ordendis = new List<OrdenDiseno>();
            this.agentes = new AgentesList();
            this.ediciones = new EdicionesList();
            this.agente = new Profile();
            this.user = new Profile();
            this.edActual = "";
        }
        public ResponseModel resp { get; set; }

        public List<OrdenDiseno> ordendis;      
        public AgentesList agentes { get; set; }
        public EdicionesList ediciones { get; set; }

        public Profile agente;

        public Profile user;

        public string edActual = "";

    }
    public class OrdenDiseno
    {
        public OrdenDiseno()
        {
            this.Id = 0;            
        }
        public int Id { get; set;}
        public int editionId { get; set; }
        public string edition { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string agentC { get; set; }
        public string title { get; set; }

        public int status { get; set; }

        public string codeAd { get; set; }
        public string type { get; set; }
        public string area { get; set; }
        public string estilo { get; set; }
        public string agent { get; set; }

        public string entPorAgentCod { get; set; }
        public string entToAgentCod { get; set; }

        public string movDate { get; set; }
        public string movTime { get; set; }
        public string movStatus { get; set; }
        
        public string disenador { get; set; }
        public int adCodeA1 { get; set; }
        public int adCodeA2 { get; set; }
        public int adCodeA3 { get; set; }

        public int adCodeA4 { get; set; }
        public int adCodeA5 { get; set; }
        public int adCodeA6 { get; set; }
        public int adCodeA7 { get; set; }
        public int adCodeA8 { get; set; }

        public int adCodeA9 { get; set; }
        public int adCodeA6E { get; set; }
        public int adCodeA6W { get; set; }
        public int adCodeA4V { get; set; }
        public int adCodeA14 { get; set; }

        public int adCodeA1V { get; set; }
        public int adCodeA11 { get; set; }
        public int adCodeA2V { get; set; }
        public int adCodeA12 { get; set; }
        public int adCodeA3V { get; set; }

        public int adCodeA13 { get; set; }
        public bool infoCompleta { get; set; }
        public int notes { get; set; }        
        public string disenadorC { get; set; }
       }


        #endregion



    }


