using mgrsa2.Common;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace mgrsa2.Services
{
    public interface IOrdenServices
    {
        List<VideoVM> GetVideos(int videoId, string userId, int activo);
        VideoForm GetVideo(int Id);
        List<SelectListItem> GetSocialMedias();


        Clientes GetClientes(string company, string agenteId, string clientId, string vigentes);
        ClientesList GetClientesList(string company, string agenteId, string clientId, string vigentes);

        #region Desplegados
        Desplegados GetDesplegados(int clientId);
        Desplegados GetDesplegado(int despId);
        DesplegadosList GetDesplegadosList(int clientId);
        Desplegados GetDesplegadosByAgente(string agenteId, string sVenc);

        #endregion

        AgentesList GetAgentesList(string role, string agenteId, int loginId, string areas);

        AgentesList GetAgentesListB(string dept, string areas, string agenteId, int loginId, string activo, string nombre);
        ResponseModel AddVideo(VideoForm video);
        ResponseModel AuthGerencia(string videoId, string NotasGerencia, string Active, int loginId);
        ResponseModel AuthSupervisor(string videoId, string NotasSupervisor, int loginId);
        ResponseModel UpdateVideoReq(VideoForm video);


        //Ordenes de Insercion

        List<EdicionVM> Ediciones(int iTotal, int iFuture, DateTime fecha);

        EdicionDVM EdicionActual();
        List<OrdInsVM> GetOrdenesIns(string edicion, string agente);
        List<LastUpdateVM> GetLastUpdate(string edicion, string agente);

        string UpdateOrdIns(string iID
                                    , string edAnt
                                    , string pgAnt
                                    , string bNueva
                                    , string sPublicar
                                    , string sDetiene
                                    , string sNotas
                                    , int iUser);


        string InsertLogOI(string edicion
                                 , string agentID
                                 , string type
                                 , string log
                                 , int iUser);


        string UpdateStatusOI(string edicion, string agentID);
        
        List<SummOrdIns> GetSummOrdIns(string edicion, string agente);

        List<EdicionDVM> GetEdicionInfo(string id);

        Desplegados GetDesplegadosH(string clientId);
        List<OrdInsH> GetOrdInsH(string versionId);


        #region DesplegadosOnli
        DesplegadosONLI GetDesplegadosOnli(string clientId, string despId, string sGuia, string agente, string sFrom, string sTo, string vigencia);
        string UpdateDespOL(
             string id, 
             string versionid,                  
             string processed,
             string suspended,
             string notes,
             string url,
             string dateprocessed,
             string datesuspended,
             string suser
        );

        #endregion


    }

}
