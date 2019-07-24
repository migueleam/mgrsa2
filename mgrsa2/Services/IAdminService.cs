using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mgrsa2.Services
{
    public interface IAdminServices
    {
        List<UserProfile> GetProfilesRoles(string Id);
        List<RoleExt> GetUserRoles(string roleId, string userId);
        List<RoleExt> GetRolesExt(string grupo, string nombre, bool blikenombre);
        List<RoleForm> GetRolesForm(string roleId, string userId);
        List<SelectListItem> GetRolesSelect();
        RoleExt GetRoleExt(string Id);

        List<RoleExt> GetRoles(string nombrerole);

        ResponseModel AddRole(UserRole userrole);
        ResponseModel RemoveRole(UserRole userrole);
        ResponseModel UpdateRole(RoleExt role);
        List<SelectListItem> GetRoleGroups();

        List<PhoneProvider> GetPhoneProviders();
        PhoneProviderForm GetPhoneProvider(int Id);
        ResponseModel UpdatePhoneProvider(PhoneProvider prov);
        ResponseModel CreatePhoneProvider(PhoneProvider prov);


        List<Location> GetLocations();
        Location GetLocation(int Id);
        ResponseModel UpdateLocation(Location loc);
        ResponseModel CreateLocation(Location loc);



        List<Edicion> GetEdiciones();
        Edicion GetEdicion(string Id);
        ResponseModel UpdateEdicion(Edicion itm);

        
        DoorAccessForm GetDoorAccess();
        ResponseModel UpdateDoorAccess(DoorAccessForm itm);       
     
     
        Edicion GetEdicionActual();


    }
}
