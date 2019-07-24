using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace mgrsa2.Services
{
    public interface IAuxServices
    {
        List<NewAlert> GetNewsAlerts(int max, string type, string publish);
        NewAlert GetNewAlert(string id);
        int CountNewAlerts(string type, string publish);
        ResponseModel UpdateNewAlert(
                string id,
                string titulo,
                string fromdate,
                string todate,
                string orden,
                string publish,
                string memo,
                string tipo,
                string userid
        );
        //ResponseModel UpdateMemos(string sTipo);
        #region EVENTOS
        List<SelectListItem> GetUsuarios(string nivelarea, string oficina, string depts, string userId);
        List<SelectListItem> GetUsuariosvB(string grupo, string inicial, string areas, string codigo, int loginId, string activo, string nombre);
        #endregion
        
       
        List<Edicion> GetEdiciones();            
        List<Area> GetAreas();
            

    }



}
