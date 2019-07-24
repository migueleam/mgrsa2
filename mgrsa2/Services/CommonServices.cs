
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using mgrsa2.Components;
using mgrsa2.Common;
using System.IO;

namespace mgrsa2.Services
{
    public class CommonServices : ICommonServices
    {
        private AppIdentityDbContext _db;
        private IHttpContextAccessor _http;
        private ResponseModel rm;

        #region USEr...
        public Profile GetCurrentUser()
        {
            Profile profile = new Profile();
            profile.ProfileId = 0;
            try
            {
                //var user = _usermanager.GetUserAsync(hUser).Result;           
                var userId = _http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AppUser user = _db.Users.FirstOrDefault(u => u.Id == userId);
                profile = _db.Profiles.FirstOrDefault(p => p.ProfileId == user.ProfileId);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return profile;

        }

        public Profile GetCurrentUser(string userId)
        {
            Profile profile = new Profile();
            profile.ProfileId = 0;
            try
            {
                //var user = _usermanager.GetUserAsync(hUser).Result;
                //var userId = _http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AppUser user = _db.Users.FirstOrDefault(u => u.Id == userId);
                profile = _db.Profiles.FirstOrDefault(p => p.ProfileId == user.ProfileId);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return profile;

        }

        #endregion


        #region EDICIONES
        public List<EdicionForm> GetEdiciones()
        {

            ResponseModel resp = new ResponseModel();
            List<EdicionForm> coll = new List<EdicionForm>();
            EdicionForm item;

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

                        item = new EdicionForm();
                        item.Edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        item.Fecha = (DateTime)datos["edFecha"];

                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaAviso"]);
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraAviso"]);
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaCierre"]);
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraCierre"]);
                        item.edActual = (bool)datos["edActual"];

                        item.sFecha = (datos["edFecha"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));
                        item.sFechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.sHoraAviso = (datos["HoraAviso"] == DBNull.Value ? "5:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.sFechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.sHoraCierre = (datos["HoraCierre"] == DBNull.Value ? "5:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());

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
        public EdicionForm GetEdicion(string Id)
        {

            ResponseModel resp = new ResponseModel();
            EdicionForm item = new EdicionForm();

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

                        item.Edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        item.Fecha = (DateTime)datos["edFecha"];

                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaAviso"]);
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraAviso"]);
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaCierre"]);
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraCierre"]);


                        item.sFecha = (datos["edFecha"] == DBNull.Value ? "" : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));

                        item.sFechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.sHoraAviso = (datos["HoraAviso"] == DBNull.Value ? "5:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.sFechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.sHoraCierre = (datos["HoraCierre"] == DBNull.Value ? "5:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());

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
        public EdicionForm GetEdicionActual()
        {

            ResponseModel resp = new ResponseModel();
            EdicionForm item = new EdicionForm();

            try
            {

                SqlDataReader datos = SqlHelper.ExecuteReader(MySettings.ConnEAMServer
                              , "dbo.sp_Admin_GetEdicionActual"                              
                              );

                if (datos.HasRows)
                {
                    while (datos.Read())
                    {

                        item.Edicion = datos["edEdicion"].ToString();
                        item.EdicionId = (int)datos["EdicionId"];
                        item.Fecha = (DateTime)datos["edFecha"];

                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaAviso"]);
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraAviso"]);
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["FechaCierre"]);
                        item.HoraCierre = (datos["HoraCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos["HoraCierre"]);


                        item.sFecha = (datos["edFecha"] == DBNull.Value ? "" : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));

                        item.sFechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.sHoraAviso = (datos["HoraAviso"] == DBNull.Value ? "5:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.sFechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
                        item.sHoraCierre = (datos["HoraCierre"] == DBNull.Value ? "5:30 PM" : ((DateTime)datos["HoraCierre"]).ToShortTimeString());

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


        #region PHONE PROVIDERS

        public List<SelectListItem> GetPhoneProvidersSel()
        {

            List<SelectListItem> phprovs = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            try
            {

                SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "dbo.sp_admin_GetPhoneProviders"
                              , 0
                              );

                if (list.HasRows)
                {
                    while (list.Read())
                    {

                        item = new SelectListItem();
                        item.Group = group;
                        item.Value = list["PhoneProviderId"].ToString();
                        item.Text = list["Description"].ToString();

                        phprovs.Add(item);

                    }

                }

                list.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return phprovs;


        }

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

        #endregion


        #region ROLES
        public List<SelectListItem> GetRoleGroups()
        {
            SelectListGroup group = new SelectListGroup { Name = "", Disabled = false };

            List<SelectListItem> groups = new List<SelectListItem>()
            {
                new SelectListItem { Value = "A", Text ="Area Publicación", Group = group},
                new SelectListItem { Value = "D", Text ="Departamento", Group=group},
                new SelectListItem { Value = "N", Text ="Nivel / Función", Group=group},
                new SelectListItem { Value = "P", Text ="Tipo de Anuncio-Producto",Group=group},
                new SelectListItem { Value = "O", Text ="Oficinas",Group=group}
            };
            return groups;
        }

        public List<SelectListItem> GetRolesSelect()
        {
            List<SelectListItem> rolessel = new List<SelectListItem>();
            SelectListItem role;
            SelectListGroup group;


            SqlDataReader data = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetRoles"
                                 , ""
                                 , ""
                                 , 1
                                 );


            if (data.HasRows)
            {
                while (data.Read())
                {
                    role = new SelectListItem();
                    group = new SelectListGroup { Name = data["grupo"].ToString(), Disabled = false };

                    role.Value = data["roleid"].ToString();
                    role.Text = data["rolenombre"].ToString();
                    role.Group = group;

                    rolessel.Add(role);
                }

            }
            data.Close();

            return rolessel;

        }


        public List<Profile> GetCodigos(string texto)
        {
            List<Profile> profiles = new List<Profile>();
            profiles = _db.Profiles
                    .Where(p => p.Codigo.Contains(texto))
                    .OrderBy(p => p.Codigo)
                    .ToList();

            return profiles;

        }
        public List<Profile> GetUserNames(string texto)
        {
            List<Profile> profiles = new List<Profile>();
            profiles = _db.Profiles
                    .Where(p => p.UserName == texto)
                    .OrderBy(p => p.UserName)
                    .ToList();

            return profiles;

        }

        #endregion

        #region Directory
        public List<DirectoryItem> GetDirectory()
        {
            //AQUI....

            //string conn = configuration["Data:ConnStrings:connCom30Share"];
            string conn = MySettings.ConnEAMAdmin;
            List<DirectoryItem> directorio = new List<DirectoryItem>();

            string sqlTxt = string.Format("SELECT * FROM [dbo].Profiles WHERE  Activo = 1   ORDER BY Nombre");
            DataTable dt = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlTxt).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                var dir = new DirectoryItem();


                dir.Areas = dr["areas"].ToString();
                dir.Id = (int)dr["id"];
                dir.Codigo = dr["codigo"].ToString();
                dir.Email = dr["email"].ToString();
                dir.Extension = dr["extension"].ToString();
                dir.LoginDept = dr["LoginDept"].ToString();
                dir.Nombre = dr["Nombre"].ToString();
                dir.Phone = FormatPhone(dr["Phone"].ToString());

                directorio.Add(dir);
            }

            return directorio;
        }
        public List<DirectoryItem> GetDirectory(string search)
        {

            var vdirectorio = _db.Profiles
                         .Where(d => d.Activo == true && d.Nombre.Contains(search))
                         .OrderBy(d => d.Nombre)
                         .ToList();

            DirectoryItem dir;
            List<DirectoryItem> directorio = new List<DirectoryItem>();

            foreach (Profile p in vdirectorio)
            {
                dir = new DirectoryItem();
                dir.Areas = p.Areas;
                dir.Id = p.ProfileId;
                dir.Codigo = p.Codigo;
                dir.Email = p.Email;
                dir.Extension = p.Extension;
                dir.LoginDept = p.LoginDept;
                dir.Nombre = p.Nombre;
                dir.Phone = FormatPhone(p.Phone);

                directorio.Add(dir);

            }

            return directorio;
        }
        public List<Profile> GetDirectoryProfile(string search)
        {

            var vdirectorio = _db.Profiles
                         .Where(d => d.Activo == true && d.Nombre.Contains(search))
                         .ToList();

            foreach (Profile p in vdirectorio)
            {
                p.Phone = FormatPhone(p.Phone);
            }

            return vdirectorio;
        }
        public string FormatPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone) && phone.Length == 10)
            {
                phone = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
            }
            else if (!string.IsNullOrEmpty(phone) && phone.Length < 10)
            {
                phone = "Incompleto";
            }
            else
            {
                phone = "No Registrado";
            }

            return phone;

        }

        #endregion

        #region Supervisores SElectItems
        public List<SelectListItem> GetSupervisores()
        {
            List<SelectListItem> supervisores = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                               , "[dbo].sp_common_GetUsersRoles"
                               , ""
                               , "Supervisor"
                               , ""
                               , 0
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["LoginId"].ToString();
                    item.Text = list["Nombre"].ToString();

                    supervisores.Add(item);

                }

            }
            list.Close();
            supervisores = supervisores.OrderBy(s => s.Text).ToList();
            return supervisores;

        }

        public List<SelectListItem> GetContactosPermitidos()
        {

            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };
            List<SelectListItem> contperm = new List<SelectListItem>();

            for (int i = 1; i <= 10; i++)
            {
                item = new SelectListItem();
                item.Group = group;
                item.Value = (i * 10).ToString();
                item.Text = (i * 10).ToString();

                contperm.Add(item);

            }
            return contperm;

        }


        public List<SelectListItemPlain> ListPlain(List<SelectListItem> list)
        {
            List<SelectListItemPlain> listp = new List<ViewModels.SelectListItemPlain>();
            SelectListItemPlain itemp;
            foreach (SelectListItem item in list)
            {
                itemp = new SelectListItemPlain();
                itemp.disabled = item.Disabled ? 1 : 0;
                itemp.group = item.Group.Name;
                itemp.selected = item.Selected ? 1 : 0;
                itemp.text = item.Text;
                itemp.value = item.Value;
                listp.Add(itemp);
            }
            return listp;

        }

        public List<SelectListItem> GetAreas()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader dat = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                    , "[dbo].sp_GetAreas"
                                    , 1
                                    );

            if (dat.HasRows)
            {
                while (dat.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = dat["areaId"].ToString();
                    item.Text = dat["descripcion"].ToString();

                    list.Add(item);

                }

            }
            dat.Close();
            list = list.OrderBy(s => s.Text).ToList();
            return list;

        }


        public List<SelectListItem> GetAreasAv()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader dat = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                    , "[dbo].sp_GetAreasAv"
                                    , 1
                                    );

            if (dat.HasRows)
            {
                while (dat.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = dat["areaId"].ToString();
                    item.Text = dat["descripcion"].ToString();

                    list.Add(item);

                }

            }
            dat.Close();
            list = list.OrderBy(s => s.Text).ToList();
            return list;

        }

        #endregion


        public List<SelectListItem> GetUsersByRole(string role)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "Todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                               , "[dbo].sp_common_GetUsersRoles"
                               , "T" //group
                               , role //ROLE
                               , "" //codigo
                               , 0  //loginid
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["LoginId"].ToString();
                    item.Text = list["Nombre"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;

        }
        public List<SelectListItem> GetUsersByRoleSel(string role)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "Todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                               , "[dbo].sp_common_GetUsersRolesSel"
                               , "T" //group
                               , role //ROLE
                               , "" //codigo
                               , 0  //loginid
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["LoginId"].ToString();
                    item.Text = list["Nombre"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;

        }

        public List<SelectListItem> GetUsersByRoleSelCod(string role)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "Todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                               , "[dbo].sp_common_GetUsersRolesSel"
                               , "T" //group
                               , role //ROLE
                               , "" //codigo
                               , 0  //loginid
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["Codigo"].ToString();
                    item.Text = list["Nombre"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;

        }

        public List<SelectListItem> GetRoutesSelect(string areaid)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnDist
                                  , "[dbo].sp_Dist_GetRoutes"
                                  , 0
                                  , ""
                                  , areaid //areaId
                                  , 0
                                  , 9
                                  );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["routeId"].ToString();
                    item.Text = list["descripcion"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;

        }

        public List<SelectListItem> GetSchedulesSelect(int routeId)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnDist
                                  , "[dbo].sp_Dist_GetSchedules"
                                  , 0
                                  , 0
                                  , ""
                                  , 0
                                  , "0"
                                  , routeId
                                  , ""
                                  , 9
                                  );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["scheduleId"].ToString();
                    item.Text = list["descripcion"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;
        }



        public List<SelectListItem> GetTipoUbic()
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnDist
                               , "[dbo].sp_Dist_TipoUbicacion"
                               , 0
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["Id"].ToString();
                    item.Text = list["Display"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderBy(s => s.Text).ToList();
            return listi;

        }
        public List<SelectListItem> GetEdiciones(int iTotal, int iFuture, DateTime fecha)
        {
            List<SelectListItem> listi = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader list = SqlHelper.ExecuteReader(MySettings.ConnDist
                                , "sp_GetEdicionesXtotalFuture"
                                , iTotal
                                , iFuture
                                , fecha
            );

            if (list.HasRows)
            {
                while (list.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = list["Id"].ToString();
                    item.Text = list["Display"].ToString();

                    listi.Add(item);

                }

            }
            list.Close();
            listi = listi.OrderByDescending(s => s.Value).ToList();
            return listi;

        }
        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".indd", "application/octet-stream" },
                {".psd", "application/octet-stream" },
                {".cdr", "image/x-coreldraw" },
                {".zip", "application/zip"}

            };
        }
               
        private List<extensionFile> GetExts()
        {
            var types = GetMimeTypes();
            List<extensionFile> exts = new List<extensionFile>();
            extensionFile extn = null;

            foreach (var itm in types)
            {
                extn = new extensionFile();
                extn.ext = itm.Key;
                extn.contentType = itm.Value;

                exts.Add(extn);
            }
            return exts;
        }

        public string GetExtensionCT(string contentType)
        {
            var exts = GetExts();
            var ext = exts.First(e => e.contentType == contentType);
            return ext.ext;
        }
        public string GetExtension(string fileName)
        {
            string ext = "";
            int pos = fileName.LastIndexOf('.');
            if (pos > -1)
                ext = fileName.Substring(pos);

            return ext;

        }


        public List<Anexo> GetAnexos(int articuloId)
        {
            List<Anexo> anexos = new List<Anexo>();
            Anexo itm;

            try
            {
                //Create sp_GetArticulos...including ids and ordens
                SqlDataReader reader = SqlHelper.ExecuteReader(
                    MySettings.ConnEdt
                    , "dbo.sp_GetAnexos",
                    0,
                    articuloId
                );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itm = new Anexo();

                        itm.anexoId = (int)reader["anexoId"];
                        itm.articuloId = (int)reader["articuloid"];
                        itm.extension = reader["extension"].ToString();
                        itm.fileDate = reader["fileDate"].ToString();
                        itm.fileName = reader["fileName"].ToString();
                        itm.notas = reader["notas"].ToString();
                        itm.stamp = string.Format("{0:MM/dd/yyyy}", (DateTime)reader["stamp"]);
                        itm.stampHora = ((DateTime)reader["stamp"]).ToShortTimeString();

                        anexos.Add(itm);
                    }

                }
                reader.Close();
                anexos = anexos.OrderByDescending(ax => ax.anexoId).ToList();

            }
            catch (Exception ex)
            {
                //no data
                var msg = ex.Message;
            }


            return anexos;

        }
        public Anexo AddAnexo(Anexo anexo)
        {

            try
            {
                SqlDataReader oread = SqlHelper.ExecuteReader(MySettings.ConnEdt
                    , "[dbo].sp_InsertAnexo",
                    anexo.articuloId,
                    anexo.fileName,
                    anexo.extension,
                    anexo.fileDate,
                    anexo.notas,
                    anexo.stampUserId
                );

                oread.Read();
                anexo.anexoId = Convert.ToInt32(oread["Id"].ToString());
                oread.Close();

                anexo.resp.response = true;
                anexo.resp.identity = Convert.ToInt32(oread["Id"].ToString());

                return anexo;

            }
            catch (Exception ex)
            {
                //no data
                anexo.resp.message = ex.Message;
                var msg = ex.Message;
            }

            return anexo;

        }




        #region ATTACHMENT LOGIC


        public List<Attachment> Listar(int TaskId)
        {
            List<Attachment> attachs = new List<Attachment>();
            Attachment attach = null;

            try
            {
                    SqlDataReader reader = SqlHelper.ExecuteReader(
                        MySettings.ConnProj,
                        "sp_attachment_GetAttachmentsByTaskId",
                        TaskId
                        );

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            attach = new Attachment();

                            attach.attachmentId = (int)reader["attachmentID"];
                            attach.fileName = reader["fileName"].ToString();
                            attach.notes = reader["notes"].ToString();
                            attach.extension = reader["extension"].ToString();
                            attach.taskId = (int)reader["TaskId"];
                            attach.ideaId = (int)reader["IdeaId"];
                            attach.attachmentDate = reader["AttachmentDate"].ToString();

                            attachs.Add(attach);
                        }
                    }
                    reader.Close();

                    attachs = attachs.OrderByDescending(a => a.attachmentId).ToList();


            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }

        
            return attachs;
        }

        public List<AttachmentVM> Listar(string TaskId, string attID)
        {
            List<AttachmentVM> attachs = new List<AttachmentVM>();
            AttachmentVM attach = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(
                MySettings.ConnProj,
                "sp_task_GetAttachments",
                attID,
                TaskId
                );

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    attach = new AttachmentVM();

                    attach.attachmentId = (int)reader["attachmentID"];
                    attach.fileName = reader["fileName"].ToString();
                    attach.notes = reader["notes"].ToString();
                    attach.extension = reader["extension"].ToString();
                    attach.taskId = (int)reader["TaskId"];
                    attach.attachmentDate = reader["AttachmentDate"].ToString();
                    attach.ideaId = (int)reader["IdeaId"];

                    attachs.Add(attach);
                }
            }
            reader.Close();

            attachs = attachs.OrderByDescending(a => a.attachmentId).ToList();

            return attachs;
        }


        public List<Attachment> ListarI(int IdeaId)
        {
            List<Attachment> attachs = new List<Attachment>();
            Attachment attach = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(
                MySettings.ConnProj,
                "sp_attachment_GetAttachmentsByIdeaId",
                IdeaId
                );

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    attach = new Attachment();

                    attach.attachmentId = (int)reader["attachmentID"];
                    attach.fileName = reader["fileName"].ToString();
                    attach.notes = reader["notes"].ToString();
                    attach.extension = reader["extension"].ToString();
                    attach.taskId = (int)reader["TaskId"];
                    attach.ideaId = (int)reader["IdeaId"];
                    attach.attachmentDate = reader["AttachmentDate"].ToString();

                    attachs.Add(attach);
                }
            }
            reader.Close();

            attachs = attachs.OrderByDescending(a => a.attachmentId).ToList();

            return attachs;
        }

        public List<AttachmentVM> ListarI(string IdeaId, string attID)
        {
            List<AttachmentVM> attachs = new List<AttachmentVM>();
            AttachmentVM attach = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(
                MySettings.ConnProj,
                "sp_idea_GetAttachments",
                attID,
                IdeaId
                );

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    attach = new AttachmentVM();

                    attach.attachmentId = (int)reader["attachmentID"];
                    attach.fileName = reader["fileName"].ToString();
                    attach.notes = reader["notes"].ToString();
                    attach.extension = reader["extension"].ToString();
                    attach.taskId = (int)reader["TaskId"];
                    attach.ideaId = (int)reader["IdeaId"];
                    attach.attachmentDate = reader["AttachmentDate"].ToString();

                    attachs.Add(attach);
                }
            }
            reader.Close();

            attachs = attachs.OrderByDescending(a => a.attachmentId).ToList();

            return attachs;
        }



        public ResponseModel Guardar(Attachment attachment)
        {            

            rm.identity = 0;

            rm.response = false;

            if (attachment.attachmentId == 0)
            {
                rm = Insert(attachment);
            }
            else if (attachment.attachmentId > 0)
            {
                rm = Update(attachment);
            }

            return rm;
        }

        public ResponseModel Insert(Attachment attachment)
        {
            int iResult = 0;
            rm = new ResponseModel();

            try
            {
                var vResult = SqlHelper.ExecuteScalar(
                                    MySettings.ConnProj
                                    , "sp_attachment_Insert"
                                    , attachment.fileName
                                    , attachment.notes
                                    , attachment.extension
                                    , attachment.taskId
                                    , attachment.attachmentDate
                                    , attachment.userId
                                    , attachment.ideaId
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


        //NO CREATED...
        public ResponseModel Update(Attachment attachment)
        {
            int iResult = 0;
            rm = new ResponseModel();

            try
            {
                iResult = SqlHelper.ExecuteNonQuery(
                                    MySettings.ConnProj
                                    , "[dbo].sp_attachment_Update"
                                    , attachment.attachmentId
                                    , attachment.fileName
                                    , attachment.notes
                                    , attachment.extension
                                    , attachment.taskId
                                    , attachment.attachmentDate
                                    , attachment.userId
                                    , attachment.ideaId
                                    );

                rm.identity = iResult;
                rm.response = true;

            }
            catch (Exception ex)
            {
                rm.response = false;
                rm.message = ex.Message;
            }

            return rm;
        }

        //NO CREATED
        public Attachment Obtener(int id)
        {
            Attachment attach = null;
            SqlDataReader reader = SqlHelper.ExecuteReader(
                                   MySettings.ConnProj,
                                   "sp_attachment_GetAttachmentByID",
                                   id
                                   );
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    attach = new Attachment();
                    attach.attachmentId = (int)reader["AttachmentID"];
                    attach.fileName = reader["fileName"].ToString();
                    attach.notes = reader["notes"].ToString();
                    attach.extension = reader["extension"].ToString();
                    attach.taskId = (int)reader["TaskId"];
                    attach.attachmentDate = reader["AttachmentDate"].ToString();
                    attach.ideaId = (int)reader["IdeaId"];
                    break;
                }
            }

            return attach;

        }

        #endregion



        #region DEPARTMENT, PRIORITY

        public List<SelectVM> ListarDepartment()
        {

            List<SelectVM> sels = new List<SelectVM>();
            SelectVM sel = null;
            
            SqlDataReader reader = SqlHelper.ExecuteReader(
                MySettings.ConnProj,
                "sp_task_GetDepartment"
            );

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sel = new SelectVM();

                    sel.id = reader["departmentID"].ToString();
                    sel.display = reader["Description"].ToString();

                    sels.Add(sel);

                }

            }
            reader.Close();
            sels.OrderBy(t => t.display).ToList();

            return sels;

        }

        public List<SelectVM> ListarPriority()
        {

            List<SelectVM> sels = new List<SelectVM>();
            SelectVM sel = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(
                    MySettings.ConnProj,
                    "sp_task_GetPriority");

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sel = new SelectVM();

                    sel.id = reader["priorityID"].ToString();
                    sel.display = reader["Description"].ToString();
                    sels.Add(sel);

                }

            }
            reader.Close();
            sels.OrderBy(t => t.display).ToList();

            return sels;

        }

        #endregion

        public List<SelectVM> ListarSelect(string table)
        {
            List<SelectVM> opciones = new List<SelectVM>();
            SelectVM opcion = null;

            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(
                                     MySettings.ConnProj
                                    , "[dbo].sp_proj_GetSelect"
                                    , table
                                    );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        opcion = new SelectVM();
                        opcion.id = reader["ID"].ToString();
                        opcion.display = reader["Display"].ToString();
                        opciones.Add(opcion);
                    }
                }
                reader.Close();
                opciones.OrderBy(t => t.display).ToList();
            }
            catch
            {

            }

            return opciones;
        }



        public List<EdicionVM> Ediciones(int iTotal, int iFuture, DateTime fecha)
        {
            List<EdicionVM> ediciones = new List<EdicionVM>();
            EdicionVM edicion = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnDist,
                "sp_GetEdicionesXtotalFuture"
                , iTotal
                , iFuture
                , fecha
                );

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    edicion = new EdicionVM();

                    edicion.ID = reader["ID"].ToString();
                    edicion.Display = reader["Display"].ToString();
                    edicion.Descripcion = reader["Descripcion"].ToString();
                    edicion.FEdicion = reader["FEdicion"].ToString();
                    ediciones.Add(edicion);
                }
            }
            reader.Close();

            return ediciones;

        }

        public EdicionDVM EdicionActual()
        {
            EdicionDVM edicion = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnEAMServer,
                "sp_intra_edicionActual"
                );

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    edicion = new EdicionDVM();

                    edicion.edEdicion = reader["edEdicion"].ToString();
                    edicion.edFecha = (DateTime)reader["edFecha"];
                    edicion.edCerrada = reader["edCerrada"].ToString();
                    edicion.edEdiciones = reader["edEdiciones"].ToString();
                    edicion.edicionId = (int)reader["edicionId"];
                    edicion.fechaAviso = reader["fechaAviso"].ToString();
                    edicion.horaAviso = reader["horaAviso"].ToString();
                    edicion.fechaCierre = reader["fechaCierre"].ToString();
                    edicion.horaCierre = reader["horaCierre"].ToString();
                    break;
                }
            }
            reader.Close();

            return edicion;

        }
        public List<EdicionDVM> GetEdicionInfo(string id)
        {
            List<EdicionDVM> ediciones = new List<EdicionDVM>();
            EdicionDVM edicion = null;

            SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnEAMServer,
                "sp_intra_EdicionInfo"
                , id
           );

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    edicion = new EdicionDVM();

                    edicion.edEdicion = reader["edEdicion"].ToString() + string.Format("{0: MM/dd/yyyy}", (DateTime)reader["edFecha"]);
                    edicion.edFecha = (DateTime)reader["edFecha"];
                    edicion.edCerrada = reader["edCerrada"].ToString();
                    edicion.edEdiciones = reader["edEdiciones"].ToString();
                    edicion.edicionId = (int)reader["edicionId"];
                    edicion.fechaAviso = reader["fechaAviso"].ToString();
                    edicion.horaAviso = reader["horaAviso"].ToString();
                    edicion.fechaCierre = reader["fechaCierre"].ToString();
                    edicion.horaCierre = reader["horaCierre"].ToString();
                    edicion.Descripcion = reader["Descripcion"].ToString();

                    ediciones.Add(edicion);
                    break;
                }
            }
            reader.Close();

            return ediciones;

        }

        public List<SelectListItem> GetAuxiliars(string spProc)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item;
            SelectListGroup group = new SelectListGroup { Name = "todos", Disabled = false };

            SqlDataReader dat = SqlHelper.ExecuteReader(MySettings.ConnProj
                                    , spProc
                                    );

            if (dat.HasRows)
            {
                while (dat.Read())
                {
                    item = new SelectListItem();
                    item.Group = group;
                    item.Value = dat["id"].ToString();
                    item.Text = dat["display"].ToString();

                    list.Add(item);

                }

            }
            dat.Close();
            list = list.OrderBy(s => s.Text).ToList();
            return list;

        }


    }
}