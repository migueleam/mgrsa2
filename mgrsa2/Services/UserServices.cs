using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using mgrsa2.Services;

namespace mgrsa2.Services
{
    public class UserServices : IUserServices
    {

        //private MySettings mysettings;
        private PhoneProvider phoneprovider;
        private Profile profile;
        private AppIdentityDbContext _db;
        private IHttpContextAccessor _http;
        private UserManager<AppUser> _usermanager;

        public object User { get; private set; }


        public AppUser FindUserByUserName(string username)
        {
            AppUser user = new AppUser();

            try
            {
                user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return user;
        }

        public UserServices(AppIdentityDbContext db,
            UserManager<AppUser> usermanager
         )
        {
            _db = db;
            _usermanager = usermanager;
        }

        //private IConfiguration configuration;

        public List<Profile> GetProfiles()
        {

            List<Profile> profiles = new List<Profile>();

            return _db.Profiles.ToList();
        }
        public List<Profile> GetLoginsAdo()
        {

            //string conn = configuration["Data:ConnStrings:connCom30Share"];
            string conn = MySettings.ConnShared;
            List<Profile> profiles = new List<Profile>();

            string sqlTxt = string.Format("SELECT * FROM [dbo].tblLogins  ORDER BY LoginID");
            DataTable dt = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlTxt).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                profile = new Profile();

                profile.LoginId = (int)dr["LoginID"];
                profile.Activo = (dr["loginActive"].ToString() == "T" ? true : false);
                profile.Codigo = dr["LoginCagCodigo"].ToString();
                profile.Extension = dr["LoginPhoneExtension"].ToString();
                profile.LoginTmkLeadSources = dr["LoginTmkLeadSources"].ToString();
                profile.Nombre = dr["LoginFullName"].ToString();
                profile.Phone = dr["LoginCellular"].ToString();
                profile.PhoneProviderId = (int)dr["LoginProvider"];


                profile.UserName = dr["LoginUserID"].ToString();
                profile.Password = dr["LoginPassword"].ToString();
                profile.Email = dr["LoginEmail"].ToString();


                profile.LevelId = (int)dr["LoginLevelID"];
                profile.SupervisorId = (int)dr["LoginSupervisor"];
                profile.Supervisor = (bool)dr["LoginSupervisorB"];
                profile.LoginDept = dr["LoginDept"].ToString();
                profile.CoSupervisor = (bool)dr["LoginCoSupervisor"];
                profile.Areas = dr["LoginArea"].ToString();

                profile.LoginTelemarketingArea = dr["LoginTelemarketingArea"].ToString();
                profile.LoginTelemarketing = (bool)dr["LoginTelemarketing"];

                profiles.Add(profile);
            }

            return profiles;
        }
        public List<Profile> GetLogins()
        {
            List<Profile> profiles = new List<Profile>();
            profiles = _db.Profiles.ToList();

            return profiles;

        }
        public List<PhoneProvider> GetPhoneProvidersAdo()
        {
            //string conn = configuration["Data:ConnStrings:connCom30Share"];
            string conn = MySettings.ConnShared;
            List<PhoneProvider> phoneproviders = new List<PhoneProvider>();

            string sqlTxt = string.Format("SELECT * FROM [dbo].mk_CellProvider  ORDER BY id");
            DataTable dt = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlTxt).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                phoneprovider = new PhoneProvider();

                phoneprovider.Description = dr["description"].ToString();
                phoneprovider.Address = dr["address"].ToString();
                phoneprovider.FromLength = (int)dr["fromlength"];
                phoneprovider.MaxLength = (int)dr["maxlength"];

                phoneproviders.Add(phoneprovider);
            }

            return phoneproviders;
        }
        public List<PhoneProvider> GetPhoneProviders()
        {
            List<PhoneProvider> providers = new List<PhoneProvider>();
            providers = _db.PhoneProviders.ToList();
            return providers;

        }

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

                dir.twitter = dr["twitter"].ToString();
                dir.facebook = dr["facebook"].ToString();
                dir.instagram  = dr["instagram"].ToString();
                dir.linkedIn = dr["linkedIn"].ToString();

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

                dir.twitter = p.twitter;
                dir.facebook = p.facebook;
                dir.instagram = p.instagram;
                dir.linkedIn = p.linkedIn;

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


        #region ROLES

        public List<Role> GetRoles()
        {
            return new List<Role>()
            {
                 new Role() { Name = "Admin" },
                 new Role() { Name = "Area 1" },
                 new Role() { Name = "Area 2" },
                 new Role() { Name = "Area 3" },
                 new Role() { Name = "Area 4" },
                 new Role() { Name = "Area 5" },
                 new Role() { Name = "Area 6" },
                 new Role() { Name = "Area 7" },
                 new Role() { Name = "Area 8" },
                 new Role() { Name = "Area 9" },
                 new Role() { Name = "Asistente" },
                 new Role() { Name = "Avisos" },
                 new Role() { Name = "Contabilidad" },
                 new Role() { Name = "Desplegados" },
                 new Role() { Name = "Diseño" },
                 new Role() { Name = "Distribucion" },
                 new Role() { Name = "Editorial" },
                 new Role() { Name = "Editor" },
                 new Role() { Name = "Gerencia" },
                 new Role() { Name = "Glossies" },
                 new Role() { Name = "Invitado" },
                 new Role() { Name = "Marketing" },
                 new Role() { Name = "Mini Avisos" },
                 new Role() { Name = "Of Ontario" },
                 new Role() { Name = "Of OC" },
                 new Role() { Name = "Of Principal" },
                 new Role() { Name = "Of SF" },
                 new Role() { Name = "Personal_HR" },
                 new Role() { Name = "Social Media" },
                 new Role() { Name = "Supervisor" },
                 new Role() { Name = "Tecnico" },
                 new Role() { Name = "Telemarketing" },
                 new Role() { Name = "Usuario" },
                 new Role() { Name = "Ventas" }

            };
        }
        public List<RoleExt> GetRolesExt(string roleId, string userId)
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

        #endregion

        public List<DirectoryItem> GetDirectoryAdo(string search)
        {
            //AQUI....

            //string conn = configuration["Data:ConnStrings:connCom30Share"];
            string conn = MySettings.ConnEAMAdmin;
            List<DirectoryItem> directorio = new List<DirectoryItem>();

            string sqlTxt = string.Format("SELECT * FROM [dbo].Profiles  WHERE Activo = 1 AND Nombre LIKE '%{0}%' ORDER BY Nombre ", search);
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

                dir.twitter = dr["twitter"].ToString();
                dir.facebook = dr["facebook"].ToString();
                dir.instagram = dr["instagram"].ToString();
                dir.linkedIn = dr["linkedIn"].ToString();
                
                directorio.Add(dir);
            }

            return directorio;
        }




        #region  GET AUXILIARES SELECT LIST ITEM

        /////////////
        ///
        ///   Auxiliares
        ///   
        ////////////////

        public List<SelectListItem> GetSupervisores()
        {
            List<Profile> profiles = _db.Profiles
                            .Where(p => p.Activo && p.Supervisor)
                            .OrderBy(p => p.Nombre)
                            .ToList();

            SelectListItem item;

            List<SelectListItem> supervisores = new List<SelectListItem>();


            foreach (Profile p in profiles)
            {
                item = new SelectListItem();
                item.Value = p.LoginId.ToString();
                item.Text = p.Nombre;

                supervisores.Add(item);

            }

            return supervisores;

        }

        public List<SelectListItem> GetCodigos()
        {
            List<Profile> profiles = _db.Profiles
                         .OrderBy(p => p.Activo)
                         .ThenBy(p => p.Codigo)
                         .ToList();

            SelectListItem item;

            List<SelectListItem> codigos = new List<SelectListItem>();
            var Activo = new SelectListGroup { Name = "Activo" };
            var NoActivo = new SelectListGroup { Name = "No Activo" };

            foreach (Profile p in profiles)
            {
                item = new SelectListItem();
                item.Value = p.Codigo;
                item.Text = p.Codigo;

                if (p.Activo)
                    item.Group = Activo;
                else
                    item.Group = NoActivo;

                codigos.Add(item);

            }

            return codigos;


        }
        public List<SelectListItem> GetProveedores()
        {
            List<PhoneProvider> proveedoresOr = _db.PhoneProviders
                         .OrderBy(p => p.Description)
                         .ToList();

            SelectListItem item;

            List<SelectListItem> proveedores = new List<SelectListItem>();

            foreach (PhoneProvider p in proveedoresOr)
            {
                item = new SelectListItem();
                item.Value = p.PhoneProviderId.ToString();
                item.Text = p.Description;

                proveedores.Add(item);

            }
            return proveedores;


        }

        public List<SelectListItem> GetContactosPermitidos()
        {

            SelectListItem item;

            List<SelectListItem> contperm = new List<SelectListItem>();

            for (int i = 1; i <= 10; i++)
            {
                item = new SelectListItem();
                item.Value = (i * 10).ToString();
                item.Text = (i * 10).ToString();

                contperm.Add(item);

            }
            return contperm;


        }
        public List<SelectListItem> GetLevels()
        {
            List<SelectListItem> levels = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text ="Administrador"},
                new SelectListItem { Value = "2", Text ="Editor"},
                new SelectListItem { Value = "3", Text ="Usuario"},
                new SelectListItem { Value = "4", Text ="Invitado"}
            };
            return levels;
        }


        #endregion


        public async Task<List<DirectoryItem>> GetDirectoryAnsync(string search)
        {
            List<DirectoryItem> directorio = new List<DirectoryItem>();

            var vdirectorio = await _db.Profiles
                            .Where(d => d.Activo == true && d.Nombre.Contains(search))
                            .OrderBy(d => d.Nombre)
                            .ToListAsync();

            DirectoryItem dir;


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
                
                dir.twitter = p.twitter;
                dir.facebook = p.facebook;
                dir.instagram = p.instagram;
                dir.linkedIn = p.linkedIn;
                
                directorio.Add(dir);

            }

            return directorio;

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
                //phone = "No Registrado";
                phone = "";
            }

            return phone;

        }


        #region  GET PROFILES
        //view models

        public List<UserProfile> GetUserProfiles()
        {
            string rolegps = "ADNOP";
            int p = 0;
            List<UserProfile> userprofiles = new List<UserProfile>();

            //var vuserprofiles = _db.Users.Select(u => new { u.Id, u.Email, u.UserName, u.ProfileId, u.LoginId, u.Profile.Nombre, u.Profile.Codigo, u.Profile.Activo })
            //    .OrderBy( u => u.Nombre)
            //    .ToList();

            UserProfile userpf = null;

            SqlDataReader user = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetUserProfilesRoles", "", 0, 0);


            if (user.HasRows)
            {
                while (user.Read())
                {
                    if (userpf == null)
                    {
                        userpf = new UserProfile();

                        userpf.Id = user["Id"].ToString();
                        userpf.Email = user["Email"].ToString();
                        userpf.UserName = user["UserName"].ToString();
                        userpf.ProfileId = (int)user["ProfileId"];
                        userpf.LoginId = (int)user["LoginId"];
                        userpf.Nombre = user["Nombre"].ToString();
                        userpf.Codigo = user["Codigo"].ToString();
                        userpf.Activo = (bool)user["Activo"];

                        userpf.Phone = user["Phone"].ToString();
                        userpf.Extension = user["Extension"].ToString();
                        userpf.PhoneProviderId = (int)user["PhoneProviderId"];
                        userpf.SupervisorId = (int)user["SupervisorId"];
                        userpf.PhoneFmt = FormatPhone(user["Phone"].ToString());


                        userpf.twitter = user["twitter"].ToString();
                        userpf.facebook = user["facebook"].ToString();
                        userpf.instagram = user["instagram"].ToString();
                        userpf.linkedIn = user["linkedIn"].ToString();

                        p = rolegps.IndexOf(user["Group"].ToString());
                        userpf.roles[p] = user["roleNombre"].ToString();

                    }
                    else if (user["Id"].ToString() != userpf.Id)
                    {

                        userprofiles.Add(userpf);

                        userpf = new UserProfile();

                        userpf.Id = user["Id"].ToString();
                        userpf.Email = user["Email"].ToString();
                        userpf.UserName = user["UserName"].ToString();
                        userpf.ProfileId = (int)user["ProfileId"];
                        userpf.LoginId = (int)user["LoginId"];
                        userpf.Nombre = user["Nombre"].ToString();
                        userpf.Codigo = user["Codigo"].ToString();
                        userpf.Activo = (bool)user["Activo"];

                        userpf.Phone = user["Phone"].ToString();
                        userpf.Extension = user["Extension"].ToString();
                        userpf.PhoneProviderId = (int)user["PhoneProviderId"];
                        userpf.SupervisorId = (int)user["SupervisorId"];
                        userpf.PhoneFmt = FormatPhone(user["Phone"].ToString());

                        userpf.twitter = user["twitter"].ToString();
                        userpf.facebook = user["facebook"].ToString();
                        userpf.instagram = user["instagram"].ToString();
                        userpf.linkedIn = user["linkedIn"].ToString();

                        p = rolegps.IndexOf(user["Group"].ToString());
                        userpf.roles[p] = user["roleNombre"].ToString();

                    }
                    else
                    {
                        p = rolegps.IndexOf(user["Group"].ToString());
                        userpf.roles[p] = user["roleNombre"].ToString();

                    }


                }
                userprofiles.Add(userpf);

            }
            user.Close();

            return userprofiles;
        }

        public UserProfile GetUserProfiles(string Id, int loginId, int profileId)
        {
            UserProfile userpf = new UserProfile();

            SqlDataReader user = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                                 , "dbo.admin_GetUserProfiles"
                                 , Id
                                 , loginId
                                 , profileId
                                 );

            if (user.HasRows)
            {
                while (user.Read())
                {
                    userpf.Id = user["Id"].ToString();
                    userpf.Email = user["Email"].ToString();
                    userpf.UserName = user["UserName"].ToString();
                    userpf.ProfileId = (int)user["ProfileId"];
                    userpf.LoginId = (int)user["LoginId"];
                    userpf.Nombre = user["Nombre"].ToString();
                    userpf.Codigo = user["Codigo"].ToString();
                    userpf.Activo = (bool)user["Activo"];
                }

            }
            user.Close();

            return userpf;

        }

        private UserProfile MapDbUserProfile(dynamic dbUP)
        {
            return new UserProfile()
            {
                Id = dbUP.Id,
                Email = dbUP.Email,
                UserName = dbUP.UserName,
                ProfileId = dbUP.ProfileId,
                LoginId = dbUP.LoginId,
                Nombre = dbUP.Nombre,
                Codigo = dbUP.Codigo,
                Activo = dbUP.Activo
            };

        }

        #endregion



        #region Users Comercial 30

        public ResponseModel AddLogin(UserFormModel user)
        {

            ResponseModel resp = new ResponseModel();

            var profile = _db.Profiles.FirstOrDefault(p => p.Codigo == user.Codigo);
            //temp to avoid existing Logins...
            if (profile != null)
            {
                resp.identity = profile.LoginId;
                resp.message = "Profile Existente";
                resp.response = true;
                return resp;
            }

            //ver si existe...
            //

            try
            {
                var vLoginId = SqlHelper.ExecuteScalar(MySettings.ConnShared
                    , "[dbo].sp_EAMAdmin_ExisteLogin"
                    , user.UserName
                    , user.Codigo
                );

                if (vLoginId != null)
                {
                    resp.identity = Convert.ToInt32(vLoginId);
                    resp.message = "Usuario Existente";
                    resp.response = true;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

            try
            {
                SqlDataReader oread = SqlHelper.ExecuteReader(MySettings.ConnShared
                    , "[dbo].sp_EAMAdmin_InsertLogin"
                    , user.UserName
                    , user.Password
                    , user.Codigo
                    , user.Nombre
                    , 3
                    , user.SupervisorId
                    , 1
                    , 40
                    , "C"
                );

                oread.Read();
                resp.identity = Convert.ToInt32(oread["Id"].ToString());
                oread.Close();

                user.LoginId = resp.identity;
                resp.response = true;
                return resp;
            }
            catch (Exception ex)
            {
                resp.identity = 0;
                resp.message = ex.Message;
                return resp;
            }



        }
        public ResponseModel AddProfile(UserFormModel user)
        {

            ResponseModel resp = new ResponseModel();

            var prof = _db.Profiles.FirstOrDefault(p => p.Codigo == user.Codigo);

            if (prof != null)
            {
                resp.identity = prof.ProfileId;
                resp.response = true;
                return resp;
            }

            Profile profile = new Profile();
            try
            {
                profile.LoginId = user.LoginId;
                profile.Codigo = user.Codigo;
                profile.Email = user.UserName + "@elaviso.com";
                profile.Nombre = user.Nombre;
                profile.Password = user.Password;
                profile.SupervisorId = user.SupervisorId;
                profile.UserName = user.UserName;
                profile.LoginTelemarketingArea = "";
                profile.LoginTelemarketing = false;

                profile.twitter = user.twitter;
                profile.facebook = user.facebook;
                profile.instagram = user.instagram;
                profile.linkedIn = user.linkedIn;


                _db.Profiles.Add(profile);
                _db.SaveChanges();

                resp.identity = profile.ProfileId; //identity

                resp.response = true;

                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

        }

        #endregion

    

        public Profile GetProfile(int Id)
        {

            Profile profile = new Profile();
            profile = _db.Profiles.FirstOrDefault(p => p.ProfileId == Id);

            return profile;
        }
        public ProfileForm GetProfileForm(int Id)
        {

            AppUser user = _db.Users.FirstOrDefault(u => u.ProfileId == Id);
            Profile profile = new Profile();
            profile = _db.Profiles.FirstOrDefault(p => p.ProfileId == Id);

            ProfileForm prof = new ProfileForm();

            prof.UserId = user.Id;
            prof.Codigo = profile.Codigo;
            prof.Password = profile.Password;
            prof.Email = profile.Email;
            prof.LoginId = profile.LoginId;
            prof.ProfileId = profile.ProfileId;
            prof.UserName = profile.UserName;


            prof.Activo = profile.Activo;
            prof.Nombre = profile.Nombre;
            prof.Phone = profile.Phone;
            prof.Extension = profile.Extension;
            prof.SupervisorID = profile.SupervisorId;
            prof.PhoneProviderID = profile.PhoneProviderId;
            prof.ContactosPermitidos = profile.ContactosPermitidos;

            prof.twitter = profile.twitter;
            prof.facebook = profile.facebook;
            prof.instagram = profile.instagram;
            prof.linkedIn = profile.linkedIn;

            //old system

            //try
            //{
            //    SqlDataReader oread = SqlHelper.ExecuteReader(MySettings.ConnShared
            //        , "[dbo].[sp_EAMAdmin_GetLogin]"
            //        , prof.LoginId
            //    );

            //    oread.Read();

            //    prof.Areas = oread["LoginArea"].ToString();
            //    prof.Supervisor = (bool)oread["LoginSupervisorB"];
            //    prof.CoSupervisor = (bool)oread["LoginCoSupervisor"];
            //    prof.LevelID = (int)oread["LoginLevelId"];
            //    prof.LoginDept = oread["LoginDept"].ToString();
            //    prof.LoginTelemarketingArea = oread["LoginTelemarketingArea"].ToString();
            //    prof.LoginTmkLeadSources = oread["LoginTmkLeadSources"].ToString();
            //    prof.LoginTelemarketing = (bool)oread["LoginTelemarketing"];
            //    prof.LoginTelemarketingSupervisor = (bool)oread["LoginTelemarketingSupervisor"];

            //    prof.LoginOt = (bool)oread["LoginOt"];
            //    prof.LoginDesign = (bool)oread["LoginDesign"];
            //    prof.LoginAdminRpts = (bool)oread["LoginAdminRpts"];

            //    oread.Close();
            //}
            //catch (Exception ex)
            //{
            //    var message = ex.Message;
            //}

            return prof;

        }
        public Profile GetProfileByLoginId(int loginId)
        {
            Profile profile = new Profile();
            profile = _db.Profiles.FirstOrDefault(p => p.LoginId == loginId);

            return profile;
        }


        public List<EntryCard> GetEntryCards(string userId, int loginId, int profileId, string card)
        {
            List<EntryCard> ecs = new List<EntryCard>();

            EntryCard ec;

            try
            {

                SqlDataReader r = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "[dbo].sp_admin_GetEntryCards"
                              , ""
                              , loginId
                              , profileId
                              , card
                              );

                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        ec = new EntryCard();

                        ec.UserId = r["Id"].ToString();
                        ec.EntryCardId = (int)r["entrycardId"];
                        ec.Active = (bool)r["active"];
                        ec.Card = r["card"].ToString();
                        ec.CreateLogiId = (int)r["createlogiId"];
                        ec.LoginId = (int)r["loginId"];
                        ec.ProfileId = (int)r["profileId"];

                        ecs.Add(ec);
                    }
                }
                r.Close();

            }
            catch (Exception ex)
            {
                var msg = "er" + ex.Message + "|Users Mgr";
            }

            return ecs;

        }
        public ResponseModel AddEntryCard(EntryCard card)
        {
            ResponseModel resp = new ResponseModel();

            try
            {
                List<EntryCard> cards = new List<EntryCard>();
                cards = this.GetEntryCards("", 0, 0, card.Card);

                if (cards.Count > 0)
                {
                    resp.identity = cards[0].EntryCardId;
                    resp.message = "wn|Tarjeta Existente, revise información|Adición de Tarjetas";
                    resp.response = false;
                }
                else
                {

                    int iResult = InsertEntryCard(card);
                    card.EntryCardId = iResult;
                    if (iResult == 0)
                    {

                        resp.identity = iResult;
                        resp.message = "Tarjeta (Ec) NO ADICIONADA Adicionada";
                        resp.response = false;

                    }

                    //insert into old system

                    Profile prof = GetProfile(card.ProfileId);
                    Profile profUser = GetProfileByLoginId(card.CreateLogiId);

                    iResult = InsertTarjeta(prof.Codigo, prof.LoginId.ToString(), card.Card, card.Active, profUser.Codigo);

                    resp.identity = card.EntryCardId;
                    resp.message = "Tarjeta Adicionada";
                    resp.response = true;
                }
                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

        }
        public ResponseModel UpdateEntryCard(EntryCard card)
        {

            int iActive = (card.Active) ? 1 : 0;
            int iResult = 0;

            ResponseModel resp = new ResponseModel();

            try
            {
                iResult = SqlHelper.ExecuteNonQuery(MySettings.ConnEAMAdmin
                                , "[dbo].sp_admin_UpdateEntryCard"
                                , card.EntryCardId
                                , iActive
                                //, card.Card
                                //, card.LoginId
                                //, card.ProfileId
                                , card.EditLogiId
                                , card.EditProfileId
                                );



                Profile prof = GetProfile(card.ProfileId);
                Profile profUser = GetProfileByLoginId(card.EditProfileId);

                iResult = UpdateTarjeta(prof.Codigo, prof.LoginId.ToString(), card.Card, card.Active, profUser.Codigo);

                resp.identity = card.EntryCardId;
                resp.message = "Tarjeta Actualizada";
                resp.response = true;

                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

        }
        public static int InsertEntryCard(EntryCard card)
        {
            int iActive = (card.Active) ? 1 : 0;
            int iResult = 0;

            SqlDataReader oread = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                               , "[dbo].sp_admin_AddEntryCard"
                               , iActive
                               , card.Card
                               , card.LoginId
                               , card.ProfileId
                               , card.CreateLogiId
                               , card.CreateProfileId
                               );

            oread.Read();
            iResult = Convert.ToInt32(oread["Id"].ToString());
            oread.Close();

            return iResult;

        }
        public static int InsertTarjeta(string sAgentUserID, string iLoginID, string sTarjetaID, bool bActive, string sUserID)
        {
            string sActive = (bActive) ? "1" : "0";
            int iResult = 0;
            string sqlTxt = string.Format(" INSERT INTO  hr_tarjetas (userID, loginID, tarjetaID, stampUserID, Active )  " +
                            " VALUES ('{0}',{1},'{2}','{3}',{4});  SELECT @@IDENTITY  as ID ",
                            sAgentUserID, iLoginID.ToString(), sTarjetaID, sUserID, sActive);

            SqlDataReader oread = SqlHelper.ExecuteReader(MySettings.ConnShared, CommandType.Text, sqlTxt);

            oread.Read();
            iResult = Convert.ToInt32(oread["ID"].ToString());
            oread.Close();

            return iResult;

        }
        public static int UpdateTarjeta(string sAgentUserID, string iLoginID, string sTarjetaID, bool bActive, string sUserID)
        {
            string sActive = (bActive) ? "1" : "0";
            int iResult = 0;

            string sqlTxt = string.Format(" UPDATE hr_Tarjetas SET " +
                                          " userID = '{0}', " +
                                          " loginID = {1}, " +
                                          " tarjetaID = '{2}', " +
                                          " active = {3}, " +
                                          " editUserID = '{4}', " +
                                          " editStamp = getdate() " +
                                          " WHERE tarjetaId = '{2}' "
                                          , sAgentUserID, iLoginID.ToString(), sTarjetaID, sActive, sUserID);

            iResult = SqlHelper.ExecuteNonQuery(MySettings.ConnShared, CommandType.Text, sqlTxt);

            return iResult;

        }


        public List<RoleForm> GetRolesForm(string userId)
        {

            List<RoleForm> roles = new List<RoleForm>();

            RoleForm role;

            try
            {

                SqlDataReader r = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "[dbo].sp_admin_GetRoles"
                              , userId
                              , 0
                              , 0
                              , ""
                              );

                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        role = new RoleForm();

                        role.userId = r["Id"].ToString();
                        role.ProfileId = (int)r["profileId"];
                        role.LoginId = (int)r["loginId"];
                        role.roleId = r["roleId"].ToString();
                        role.Nombre = r["roleNombre"].ToString();
                        role.grupo = r["grupo"].ToString();
                        role.Group = r["group"].ToString();
                        role.roleInitial = r["roleInitial"].ToString();

                        roles.Add(role);
                    }
                }

                r.Close();

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return roles;


        }
        //ResponseModel AddRole(UserRole userrole);
        //ResponseModel RemoveRole(UserRole userrole);

        public List<string> GetRoles(string userId)
        {

            string rolegps = "ADNOP";

            //Area, Dept, Nivel, Off , Tipo
            //

            List<string> roles = new List<string>(5);

            try
            {

                SqlDataReader r = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "[dbo].sp_admin_GetRoles"
                              , userId
                              , 0
                              , 0
                              , ""
                              );

                if (r.HasRows)
                {
                    while (r.Read())
                    {

                        int p = rolegps.IndexOf(r["Group"].ToString());
                        roles[p] = r["roleNombre"].ToString();

                    }
                }

                r.Close();

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return roles;

        }
        

        #region SETTING PROFILE
        public ResponseModel UpdateProfile(ProfileForm profile)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                Profile prof = new Profile();
                prof = _db.Profiles.FirstOrDefault(p => p.ProfileId == profile.ProfileId);

                prof.Activo = profile.Activo;
                prof.Nombre = profile.Nombre;
                prof.SupervisorId = profile.SupervisorID;
                prof.Phone = profile.Phone;
                prof.Extension = profile.Extension;
                prof.PhoneProviderId = profile.PhoneProviderID;
                prof.ContactosPermitidos = profile.ContactosPermitidos;

                prof.LoginTmkLeadSources += "";
                prof.LevelId = (profile.LoginId == 0 ? 3 : profile.LoginId);

                prof.twitter = profile.twitter;
                prof.facebook = profile.facebook;
                prof.instagram = profile.instagram;
                prof.linkedIn = profile.linkedIn;
                

        //old systems complaint
        //prof.Areas = profile.Areas;
        //prof.Supervisor = profile.Supervisor;
        //prof.CoSupervisor = profile.CoSupervisor;
        //prof.LevelId = profile.LevelID;
        //prof.LoginDept = profile.LoginDept;
        //prof.LoginTmkLeadSources = profile.LoginTmkLeadSources + "";
        //prof.LoginTelemarketingArea = profile.LoginTelemarketingArea + "";
        //prof.LoginTelemarketing = profile.LoginTelemarketing;
        //prof.LoginTelemarketingSupervisor = profile.LoginTelemarketingSupervisor;
        //prof.LoginOt = profile.LoginOt;
        //prof.LoginDesign = profile.LoginDesign;
        //prof.LoginAdminRpts = profile.LoginAdminRpts;

        _db.SaveChanges();

                resp.identity = prof.ProfileId;
                resp.message = "ok| Usuario Actualizado|Usuarios Mgr";
                resp.response = true;

                //UPDATE tblLogins.

                try
                {

                    int uResult = SqlHelper.ExecuteNonQuery(
                        MySettings.ConnShared,
                       "[dbo].sp_EAMAdmin_UpdateLoginPartial",

                       prof.LoginId,
                       prof.Nombre,
                       prof.Password,
                       prof.Phone,
                       prof.Extension,
                       prof.PhoneProviderId,
                       prof.Activo,
                       prof.ContactosPermitidos //,

                    //prof.LevelId,
                    //prof.Areas,
                    //prof.Supervisor,
                    //prof.SupervisorId,
                    //prof.CoSupervisor,
                    //prof.LoginDept,
                    //prof.LoginTelemarketing,
                    //prof.LoginTelemarketingArea,
                    //prof.LoginTmkLeadSources,
                    //prof.LoginTelemarketingSupervisor,

                    //prof.LoginOt,
                    //prof.LoginDesign,
                    //prof.LoginAdminRpts


                    );
                }
                catch (Exception ex)
                {
                    var message = "er|" + ex.Message + "| User Mgr";
                }

                return resp;
            }
            catch (Exception ex)
            {
                resp.message = "er|" + ex.Message + "| User Mgr";
                return resp;
            }

        }
        
        public ResponseModel UpdateMyProfile(myProfile profile)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                Profile prof = new Profile();
                prof = _db.Profiles.FirstOrDefault(p => p.ProfileId == profile.ProfileId);
                           
                prof.Phone = profile.Phone;
                prof.Extension = profile.Extension;
                prof.PhoneProviderId = profile.PhoneProviderId;

                prof.twitter = profile.twitter;
                prof.facebook = profile.facebook;
                prof.instagram = profile.instagram;
                prof.linkedIn = profile.linkedIn;
                
                _db.SaveChanges();

                resp.identity = prof.ProfileId;
                resp.message = "ok| Usuario Actualizado|Usuarios Mgr";
                resp.response = true;

                //UPDATE tblLogins.

                try
                {

                    int uResult = SqlHelper.ExecuteNonQuery(
                        MySettings.ConnShared,
                       "[dbo].sp_EAMAdmin_UpdateLoginPartial",

                       prof.LoginId,
                       prof.Nombre,
                       prof.Password,
                       prof.Phone,
                       prof.Extension,
                       prof.PhoneProviderId,
                       prof.Activo,
                       prof.ContactosPermitidos //,
                   
                    );
                }
                catch (Exception ex)
                {
                    var message = "er|" + ex.Message + "| User Mgr";
                }

                return resp;
            }
            catch (Exception ex)
            {
                resp.message = "er|" + ex.Message + "| User Mgr";
                return resp;
            }

        }

        public ResponseModel SetProfile(ProfileForm profile)
        {
            ResponseModel resp = new ResponseModel();

            try
            {

                Profile prof = new Profile();
                prof = _db.Profiles.FirstOrDefault(p => p.ProfileId == profile.ProfileId);


                prof.Phone = profile.Phone;
                prof.Extension = profile.Extension;
                prof.PhoneProviderId = profile.PhoneProviderID;
                prof.Password = profile.Password;

                _db.SaveChanges();


                resp.identity = prof.ProfileId;
                resp.message = "Profile Actualizado";
                resp.response = true;

                //UPDATE tblLogins.

                try
                {

                    int uResult = SqlHelper.ExecuteNonQuery(
                        MySettings.ConnShared,
                       "[dbo].sp_EAMAdmin_SetLogin",

                       prof.LoginId,
                       prof.Password,
                       prof.Phone,
                       prof.Extension

                    );
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }

                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }

        }
        
        public ProfileForm GetProfileForm(string userId, int loginId, int profileId)
        {

            ProfileForm prof = new ProfileForm();

            try
            {

                SqlDataReader r = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                              , "[dbo].admin_GetUserProfiles"
                              , userId
                              , loginId
                              , profileId
                              );

                if (r.HasRows)
                {
                    while (r.Read())
                    {

                        prof.UserId = r["Id"].ToString();
                        prof.Email = r["email"].ToString();
                        prof.UserName = r["username"].ToString();
                        prof.LoginId = int.Parse(r["loginId"].ToString());
                        prof.ProfileId = int.Parse(r["profileId"].ToString());
                        prof.Nombre = r["nombre"].ToString();
                        prof.Codigo = r["codigo"].ToString();
                        prof.Activo = (bool)r["activo"];
                        prof.ContactosPermitidos = int.Parse(r["ContactosPermitidos"].ToString());

                        prof.Phone = r["phone"].ToString();
                        prof.Extension = r["extension"].ToString();
                        prof.PhoneProviderID = int.Parse(r["phoneproviderId"].ToString());
                        prof.SupervisorID = int.Parse(r["supervisorId"].ToString());

                        prof.twitter = r["twitter"].ToString();
                        prof.facebook = r["facebook"].ToString();
                        prof.instagram = r["instagram"].ToString();
                        prof.linkedIn = r["linkedIn"].ToString();
                      

                    }
                }
                r.Close();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return prof;

        }

        public UserFormModel GetUserForm(string Id)
        {
            UserFormModel userform = new UserFormModel();

            AppUser user = _db.Users.FirstOrDefault(u => u.Id == Id);
            Profile profile = _db.Profiles.FirstOrDefault(p => p.ProfileId == user.ProfileId);

            //map To UserFormModel

            userform.Id = user.Id;
            userform.ProfileId = profile.ProfileId;
            userform.LoginId = profile.LoginId;
            userform.Codigo = profile.Codigo;
            userform.Nombre = profile.Nombre;
            userform.Activo = profile.Activo;
            userform.Phone = profile.Phone;
            userform.Extension = profile.Extension;
            userform.ContactosPermitidos = profile.ContactosPermitidos;
            userform.LoginTmkLeadSources = profile.LoginTmkLeadSources;
            userform.UserName = profile.UserName;
            userform.Password = profile.Password;
            userform.Email = profile.Email;
            userform.SupervisorId = profile.SupervisorId;
            userform.PhoneProviderId = profile.PhoneProviderId;

            userform.twitter = profile.twitter;
            userform.facebook = profile.facebook;
            userform.instagram = profile.instagram;
            userform.linkedIn = profile.linkedIn;

            userform.Supervisores = GetSupervisores();
            userform.Codigos = GetCodigos();
            userform.PhoneProveedores = GetProveedores();
                     

            return userform;

        }

        #endregion


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


        public Profile GetProfile(string userId)
        {

            Profile profile = new Profile();

            try
            {
                profile = _db.Profiles.FirstOrDefault(p => p.Codigo == userId);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return profile;

        }

        public ResponseModel PassLogProf()
        {

            ResponseModel resp = new ResponseModel();

            try
            {
                List<Profile> logins = GetLoginsAdo();

                foreach (Profile login in logins)
                {
                    var prof = _db.Profiles.FirstOrDefault(p => p.LoginId == login.LoginId);
                    if (prof != null)
                    {
                        prof.LoginTelemarketing = login.LoginTelemarketing;
                        prof.LoginTelemarketingArea = login.LoginTelemarketingArea;
                        prof.Supervisor = login.Supervisor;

                        _db.SaveChanges();
                    }

                }

                resp.response = true;
                resp.message = "ok|Passed Complaint Logins to Profile|Complaint Info";
                return resp;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
                return resp;
            }
        }

    }
}