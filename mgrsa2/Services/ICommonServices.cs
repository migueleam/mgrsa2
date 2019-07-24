using mgrsa2.Common;
using mgrsa2.Components;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace mgrsa2.Services
{
    public interface ICommonServices
    {

#region USERS...
        Profile GetCurrentUser();
        Profile GetCurrentUser(string userId);

#endregion
        
        List<EdicionForm> GetEdiciones();
        EdicionForm GetEdicion(string Id);
        EdicionForm GetEdicionActual();

        List<SelectListItem> GetPhoneProvidersSel();
        List<PhoneProvider> GetPhoneProviders();
        PhoneProviderForm GetPhoneProvider(int Id);


        #region Roles
        List<Profile> GetCodigos(string texto);
        List<Profile> GetUserNames(string texto);
        List<SelectListItem> GetRoleGroups();

        List<SelectListItem> GetRolesSelect();
        List<SelectListItemPlain> ListPlain(List<SelectListItem> list);


#endregion


        #region Directorio
        List<DirectoryItem> GetDirectory();
        List<DirectoryItem> GetDirectory(string search);
        List<Profile> GetDirectoryProfile(string search);

        #endregion

        #region SelectItems

        List<SelectListItem> GetSupervisores();
        List<SelectListItem> GetContactosPermitidos();
        List<SelectListItem> GetAreas();
        List<SelectListItem> GetAreasAv();

        #endregion

        List<SelectListItem> GetUsersByRole(string role);
        List<SelectListItem> GetUsersByRoleSel(string role);
        List<SelectListItem> GetUsersByRoleSelCod(string role);
        List<SelectListItem> GetRoutesSelect(string areaid);
        List<SelectListItem> GetSchedulesSelect(int routeId);
        List<SelectListItem> GetTipoUbic();

        List<SelectListItem> GetEdiciones(int iTotal, int iFuture, DateTime fecha);

        //DateSpanish GetDateSpanish(string dDate);

        //file Uploaded type
        string GetContentType(string path);
        string GetExtensionCT(string contentType);
        string GetExtension(string fileName);

        #region ANEXOS

        List<Anexo> GetAnexos(int articuloId);
        Anexo AddAnexo(Anexo anexo);

        #endregion 


        #region ATTACHMENT LOGIC

        List<Attachment> Listar(int taskID);
        List<AttachmentVM> Listar(string taskID, string attID);
        List<Attachment> ListarI(int ideaID);
        List<AttachmentVM> ListarI(string ideaID, string attID);

        ResponseModel Guardar(Attachment attachment);
        ResponseModel Insert(Attachment attachment);
        ResponseModel Update(Attachment attachment);
        Attachment Obtener(int id);

        #endregion


        List<SelectVM> ListarDepartment();
        List<SelectVM> ListarPriority();


        List<SelectVM> ListarSelect(string table);


        List<EdicionVM> Ediciones(int iTotal, int iFuture, DateTime fecha);
        EdicionDVM EdicionActual();
        List<EdicionDVM> GetEdicionInfo(string id);

        //general
        List<SelectListItem> GetAuxiliars(string spProc);


    }
}
