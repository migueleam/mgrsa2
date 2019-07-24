using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mgrsa2.Services
{
    public class AdminServices : IAdminServices
    {

        private AppIdentityDbContext _db;
        public List<UserProfile> GetProfilesRoles(string Id)
        {

            List<UserProfile> userprofiles = new List<UserProfile>();

            //var vuserprofiles = _db.Users.Select(u => new { u.Id, u.Email, u.UserName, u.ProfileId, u.LoginId, u.Profile.Nombre, u.Profile.Codigo, u.Profile.Activo })
            //    .OrderBy( u => u.Nombre)
            //    .ToList();

            UserProfile userpf = null;

            SqlDataReader user = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetProfilesRoles", Id);


            if (user.HasRows)
            {
                while (user.Read())
                {
                    userpf = new UserProfile();
                    //userprofiles.Add(MapDbUserProfile(userpf));

                    userpf.Id = user["Id"].ToString();
                    userpf.Email = user["Email"].ToString();
                    userpf.UserName = user["UserName"].ToString();
                    userpf.ProfileId = (int)user["ProfileId"];
                    userpf.LoginId = (int)user["LoginId"];
                    userpf.Nombre = user["Nombre"].ToString();
                    userpf.Codigo = user["Codigo"].ToString();
                    userpf.Activo = (bool)user["Activo"];

                    userprofiles.Add(userpf);
                }

            }

            user.Close();

            return userprofiles;
        }


        public List<RoleExt> GetUserRoles(string roleId, string userId)
        {
            List<RoleExt> rolesext = new List<RoleExt>();

            RoleExt roleext;

            SqlDataReader roles = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetUserRoles"
                                 , roleId
                                 , userId);

            if (roles.HasRows)
            {
                while (roles.Read())
                {
                    roleext = new RoleExt();

                    roleext.Id = roles["roleid"].ToString();
                    roleext.Active = true;
                    roleext.Name = roles["rolenombre"].ToString();

                    roleext.Group = roles["grupo"].ToString();
                    roleext.GroupId = roles["grupoId"].ToString();
                    rolesext.Add(roleext);
                }

            }
            roles.Close();

            return rolesext;
        }
        public List<RoleExt> GetRolesExt(string grupo, string nombre, bool blikenombre)
        {

            List<RoleExt> rolesext = new List<RoleExt>();

            RoleExt roleext;

            SqlDataReader roles = SqlHelper.ExecuteReader(
                                MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetRoles"
                                 , grupo
                                 , nombre
                                 , (blikenombre ? 1 : 0)
                                 );

            if (roles.HasRows)
            {
                while (roles.Read())
                {
                    roleext = new RoleExt();
                    roleext.Id = roles["roleid"].ToString();
                    roleext.Name = roles["rolenombre"].ToString();
                    roleext.Group = roles["grupo"].ToString();
                    roleext.Active = (bool)roles["Active"];
                    roleext.RoleInitial = roles["roleinitial"].ToString();
                    rolesext.Add(roleext);
                }

            }
            roles.Close();

            return rolesext;

        }
        public List<RoleForm> GetRolesForm(string roleId, string userId)
        {
            List<RoleForm> rolesform = new List<RoleForm>();
            RoleForm roleform;

            SqlDataReader roles = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetUserRoles"
                                 , roleId
                                 , userId);

            if (roles.HasRows)
            {
                while (roles.Read())
                {
                    roleform = new RoleForm();
                    roleform.userId = roles["userid"].ToString();
                    roleform.roleId = roles["roleid"].ToString();
                    roleform.Active = true;
                    roleform.Nombre = roles["rolenombre"].ToString();
                    roleform.Group = roles["grupo"].ToString();

                    rolesform.Add(roleform);
                }

            }
            roles.Close();

            return rolesform;

        }
        public List<SelectListItem> GetRolesSelect()
        {
            List<SelectListItem> rolessel = new List<SelectListItem>();
            SelectListItem role;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };


            SqlDataReader data = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetRoles"
                                 , ""
                                 , 0);


            if (data.HasRows)
            {
                while (data.Read())
                {
                    role = new SelectListItem();
                    group.Name = data["grupo"].ToString();
                    role.Group = group;
                    role.Value = data["roleid"].ToString();
                    role.Text = data["rolenombre"].ToString();

                    rolessel.Add(role);
                }

            }
            data.Close();

            return rolessel;

        }

        public ResponseModel AddRole(UserRole userrole)
        {
            ResponseModel resp = new ResponseModel();

            var role = _db.UserRoles.FirstOrDefault(r => r.UserId == userrole.UserId && r.RoleId == userrole.RoleId);

            if (role != null)
            {
                resp.message = "in|Role Existing|Roles";
                resp.response = true;
                return resp;
            }

            try
            {
                int result = SqlHelper.ExecuteNonQuery(
                                        MySettings.ConnEAMAdmin
                                      , "dbo.admin_AddUserRole"
                                      , userrole.UserId
                                      , userrole.RoleId);

                resp.response = true;
                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

        }

        public ResponseModel RemoveRole(UserRole userrole)
        {
            ResponseModel resp = new ResponseModel();

            var role = _db.UserRoles.FirstOrDefault(r => r.UserId == userrole.UserId && r.RoleId == userrole.RoleId);

            if (role == null)
            {
                resp.message = "wn|Role NO Encontrado|ROLES";
                resp.response = true;
                return resp;
            }
            else
            {
                try
                {
                    _db.UserRoles.Remove(role);
                    _db.SaveChanges();

                    resp.response = true;
                    return resp;
                }
                catch (Exception ex)
                {
                    resp.message = ex.Message;
                    return resp;
                }

            }


        }

        public ResponseModel UpdateRole(RoleExt role)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                //UPDATE dbo.AspNetRoles


                int uResult = SqlHelper.ExecuteNonQuery(
                    MySettings.ConnEAMAdmin,
                   "[dbo].admin_UpdateRole",
                   role.Id,
                   role.Name,
                   role.GroupId,
                   role.RoleInitial,
                   (role.Active ? 1 : 0)

                );
                resp.response = true;
                resp.message = "ok| Role Actualizado|Roles Mgr|1000";
            }
            catch (Exception ex)
            {
                resp.message = "er|" + ex.Message  +"|Roles Mgr|3000";                
            }

            return resp;
        }

        public RoleExt GetRoleExt(string Id)
        {

            ResponseModel resp = new ResponseModel();
            RoleExt role = new RoleExt();

            try
            {

                SqlDataReader roles = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.admin_GetRole"
                              , Id
                              );

                if (roles.HasRows)
                {
                    while (roles.Read())
                    {
                        role.Id = roles["roleid"].ToString();

                        role.Active = (bool)roles["active"];
                        role.Name = roles["rolenombre"].ToString();
                        role.RoleInitial = roles["roleinitial"].ToString();
                        role.Group = roles["grupo"].ToString();
                        role.GroupId = roles["grupoid"].ToString();

                    }

                }
                roles.Close();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return role;
        }

        public List<RoleExt> GetRoles(string nombrerole)
        {

            ResponseModel resp = new ResponseModel();
            List<RoleExt> roles = new List<RoleExt>();
            RoleExt role;

            try
            {

                SqlDataReader oroles = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.admin_GetRoles"
                              , ""
                              , 0
                              );

                if (oroles.HasRows)
                {
                    while (oroles.Read())
                    {
                        role = new RoleExt();
                        role.Id = oroles["roleid"].ToString();
                        role.Active = (bool)oroles["Active"];
                        role.Name = oroles["rolenombre"].ToString();
                        role.RoleInitial = oroles["roleinitial"].ToString();
                        role.Group = oroles["grupo"].ToString();
                        role.GroupId = oroles["grupoid"].ToString();

                        roles.Add(role);
                    }

                }
                oroles.Close();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return roles;
        }

        public List<SelectListItem> GetRoleGroups()
        {
            List<SelectListItem> groups = new List<SelectListItem>()
            {
                new SelectListItem { Value = "A", Text ="AREA"},
                new SelectListItem { Value = "D", Text ="DEPARTAMENTO"},
                new SelectListItem { Value = "N", Text ="NIVEL / FUNCION"},
                new SelectListItem { Value = "T", Text ="TIPO DE ANUNCIO"},
                new SelectListItem { Value = "O", Text ="OFICINAS"}
            };
            return groups;
        }

        #region PHONE PROVIDERS


        public List<PhoneProvider> GetPhoneProviders()
        {

            ResponseModel resp = new ResponseModel();
            List<PhoneProvider> provs = new List<PhoneProvider>();
            PhoneProvider prov;

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.sp_admin_GetPhoneProviders"
                              , 0
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        prov = new PhoneProvider();
                        prov.PhoneProviderId = (int)datos["PhoneProviderId"];
                        prov.Address = datos["Address"].ToString();
                        prov.Description = datos["Description"].ToString();
                        prov.FromLength = (int)datos["fromLength"];
                        prov.MaxLength = (int)datos["maxLength"];

                        provs.Add(prov);

                    }

                }

                datos.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return provs;


        }
        public PhoneProviderForm GetPhoneProvider(int Id)
        {

            ResponseModel resp = new ResponseModel();

            PhoneProviderForm prov = new PhoneProviderForm();


            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.sp_admin_GetPhoneProviders"
                              , Id
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        prov.PhoneProviderId = datos["PhoneProviderId"].ToString();
                        prov.Address = datos["Address"].ToString();
                        prov.Description = datos["Description"].ToString();
                        prov.FromLength = (int)datos["fromLength"];
                        prov.MaxLength = (int)datos["maxLength"];
                        prov.response.response = true;
                        prov.response.identity = (int)datos["PhoneProviderId"];
                    }

                    resp.response = true;
                }
                datos.Close();
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
            }

            return prov;


        }
        public ResponseModel UpdatePhoneProvider(PhoneProvider prov)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                //UPDATE dbo.AspNetRoles


                int uResult = SqlHelper.ExecuteNonQuery(
                    MySettings.ConnEAMAdmin,
                   "[dbo].sp_admin_UpdatePhoneProvider",

                   prov.PhoneProviderId,
                   prov.Address,
                   prov.Description,
                   prov.FromLength,
                   prov.MaxLength

                );
                resp.response = true;
                resp.message = "ok|Proveedor ACTUALIZADO|Prov Tel Mgr|u";

            }
            catch (Exception ex)
            {
                resp.message = "er|Proveedor NO ACTUALIZADO (" + ex.Message + ")|Prov Tel Mgr|" + prov.PhoneProviderId.ToString();
            }

            return resp;


        }
        public ResponseModel CreatePhoneProvider(PhoneProvider prov)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                SqlDataReader oread = SqlHelper.ExecuteReader(
                                     MySettings.ConnEAMAdmin,
                                     "[dbo].sp_admin_AddPhoneProvider",
                                     prov.Address,
                                     prov.Description,
                                     prov.FromLength,
                                     prov.MaxLength
                                 );

                oread.Read();
                int iId = Convert.ToInt32(oread["Id"].ToString());
                oread.Close();

                if (iId > 0)
                {
                    resp.response = true;
                    resp.identity = iId;
                    resp.message = "ok|Proveedor ADICIONADO|Prov Tel Mgr|n";
                }
                oread.Close();
            }
            catch (Exception ex)
            {
                resp.message = "er|Proveedor NO ADICIONADO (" + ex.Message + ")|Prov Tel Mgr|" + prov.PhoneProviderId.ToString();
            }

            return resp;


        }

        #endregion


        #region LOCATION (ip)
        public List<Location> GetLocations()
        {

            ResponseModel resp = new ResponseModel();
            List<Location> locs = new List<Location>();
            Location loc;

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.sp_admin_GetLocations"
                              , 0
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        loc = new Location();
                        loc.Id = (int)datos["Id"];
                        loc.IP = datos["ip"].ToString();
                        loc.Description = datos["Description"].ToString();
                        loc.Active = (bool)datos["Active"];
                        loc.EditStamp = (DateTime)datos["editStamp"];

                        locs.Add(loc);

                    }

                }

                datos.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return locs;

        }
        public Location GetLocation(int Id)
        {
            ResponseModel resp = new ResponseModel();

            Location loc = new Location();

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.sp_admin_GetLocations"
                              , Id
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {


                        loc = new Location();
                        loc.Id = (int)datos["Id"];
                        loc.IP = datos["ip"].ToString();
                        loc.Description = datos["Description"].ToString();
                        loc.Active = (bool)datos["Active"];
                        loc.EditStamp = (DateTime)datos["editStamp"];

                        resp.response = true;
                        resp.identity = (int)datos["Id"];
                    }

                }
                datos.Close();

            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
            }

            return loc;

        }
        public ResponseModel UpdateLocation(Location loc)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                int iResult = SqlHelper.ExecuteNonQuery(
                    MySettings.ConnEAMAdmin,
                   "[dbo].sp_admin_UpdateLocation",
                   
                   loc.Id,
                   loc.IP,
                   loc.Description,
                   loc.Active

                );
                resp.response = true;
                resp.message = "ok|Punto de Acceso ACTUALIZADO|Punto de Acceso Mgr|u";

            }
            catch (Exception ex)
            {
                resp.message = "er|Punto de Acceso NO ACTUALIZADO (" + ex.Message + ")|Punto de Acceso Mgr|" + loc.Id.ToString();
            }

            return resp;

        }
        public ResponseModel CreateLocation(Location loc)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                SqlDataReader oread = SqlHelper.ExecuteReader(
                                         MySettings.ConnEAMAdmin,
                                         "[dbo].sp_admin_AddLocation",
                                         loc.IP,
                                         loc.Description,
                                         loc.Active
                                     );

                oread.Read();
                int iId = Convert.ToInt32(oread["Id"].ToString());
                oread.Close();

                if (iId > 0)
                {
                    resp.response = true;
                    resp.identity = iId;

                    resp.message = "ok|Punto de Acceso ADICIONADO|Punto de Acceso Mgr|i";
                }

                oread.Close();

            }
            catch (Exception ex)
            {
                resp.message = "er|Punto de Acceso NO ADICIONADO (" + ex.Message + ")|Punto de Acceso Mgr|" + loc.Id.ToString();
            }
            return resp;

        }

        #endregion

        #region EDICIONES
        public List<Edicion> GetEdiciones()
        {

            ResponseModel resp = new ResponseModel();
            List<Edicion> coll = new List<Edicion>();
            Edicion item;

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMServer
                              , "dbo.sp_EAMAdmin_GetEdiciones"
                              , 52
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        item = new Edicion();
                        item.edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        //item.Fecha = string.Format("{0:MM/dd/yyyy}",datos["edFecha"].ToString());

                        //item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaAviso"]);
                        //item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraAviso"]);
                        //item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaCierre"]);
                        //item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraCierre"]);

                        item.edActual = (bool)datos["edActual"];

                        //item.Fecha = datos["edFecha"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy,h,m}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM,dd,yyyy,h,m}", (DateTime)datos["edFecha"]);
                        //item.FechaAviso = datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]);
                        //item.HoraAviso = datos["HoraAviso"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy,17,0}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM,dd,yyyy,h,m}", ((DateTime)datos["HoraAviso"]).AddDays(-9));                    
                        //item.FechaCierre = datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]);
                        //item.HoraCierre = datos["HoraCierre"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy,17,30}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM,dd,yyyy,h,m}", ((DateTime)datos["HoraCierre"]).AddDays(-9));


                        item.Fecha = (datos["edFecha"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));
                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? "5:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? "5:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());

                        coll.Add(item);


                    }

                }

                datos.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return coll;

        }
        public Edicion GetEdicion(string Id)
        {

            ResponseModel resp = new ResponseModel();
            Edicion item = new Edicion();

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMServer
                              , "dbo.sp_EAMAdmin_GetEdicion"
                              , Id
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        item.edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        //item.Fecha = string.Format("{0:MM/dd/yyyy}",datos["edFecha"].ToString());

                        //item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaAviso"]);
                        //item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraAviso"]);
                        //item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaCierre"]);
                        //item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraCierre"]);

                        //item.edActual = (bool)datos["edActual"];
                        //item.Fecha = datos["edFecha"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]);
                        //item.FechaAviso = datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]);
                        //item.HoraAviso = datos["HoraAviso"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy,17,0}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM,dd,yyyy,HH,mm}", ((DateTime)datos["HoraAviso"]).AddDays(-9));
                        //item.FechaCierre = datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]);
                        //item.HoraCierre = datos["HoraCierre"] == DBNull.Value ? string.Format("{0:MM,dd,yyyy,17,30}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM,dd,yyyy,HH,mm}", ((DateTime)datos["HoraCierre"]).AddDays(-9));


                        item.Fecha = (datos["edFecha"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));
                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? "05:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? "05:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());


                    }

                }
                datos.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return item;

        }
        public ResponseModel UpdateEdicion(Edicion itm)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                int iResult = SqlHelper.ExecuteNonQuery(
                    MySettings.ConnEAMServer,
                   "[dbo].sp_EAMAdmin_UpdateEdicion",

                    itm.EdicionId,
                    itm.FechaAviso + " " + itm.HoraAviso,
                    itm.FechaCierre + " " + itm.HoraCierre

                );
                resp.response = true;
                resp.message = "Edicion ACTUALIZADA (" + itm.edicion + ")";

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                resp.message = "Edicion NO ACTUALIZADA (" + itm.edicion + ")";
            }

            return resp;

        }
        #endregion

        #region DOOR ACCESS
        public DoorAccessForm GetDoorAccess()
        {

            ResponseModel resp = new ResponseModel();
            DoorAccessForm item = new DoorAccessForm();

            try
            {
                var vActualDate = SqlHelper.ExecuteScalar(MySettings.ConnDoorAccess
                    , CommandType.Text
                    , "SELECT f_EName  FROM t_a_SystemParam WHERE f_NO = 12 ");

                if (vActualDate != null)
                {
                    DateTime dActualDate = Convert.ToDateTime(vActualDate.ToString());
                    item.sActualFecha = string.Format("{0:MM/dd/yyyy}", dActualDate);
                    item.sNuevaFecha = string.Format("{0:MM/dd/yyyy}", dActualDate.AddDays(90));

                }

                resp.response = true;

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return item;

        }
        public ResponseModel UpdateDoorAccess(DoorAccessForm itm)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                string sDate = string.Format("{0:yyyy-MM-dd}", itm.sNuevaFecha);

                int iResult = SqlHelper.ExecuteNonQuery(MySettings.ConnDoorAccess
                                    , CommandType.Text
                                    , "UPDATE t_a_SystemParam SET f_EName = '" + sDate + "' WHERE f_NO = 12 ");

                resp.response = true;
                resp.message = "Fecha ACTUALIZADA (" + itm.sNuevaFecha + ")";

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                resp.message = "Fecha NO ACTUALIZADA (" + itm.sNuevaFecha + ")";
            }

            return resp;

        }


        #endregion


        #region CHECKLIST: AREAS, PROGRESS

        public Edicion GetEdicionActual()
        {

            ResponseModel resp = new ResponseModel();
            Edicion item = new Edicion();

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnShared
                              , "dbo.sp_intra_EdActual"                              
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        item.edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        item.Fecha = (datos["edFecha"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));
                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? "05:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-10)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? "05:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());

                    }

                }
                datos.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return item;

        }

        

        #endregion

    }
}
