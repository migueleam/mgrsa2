using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Encodings.Web;


namespace mgrsa2.Services
{
    public class AuxServices : IAuxServices
    {

        private AppIdentityDbContext _db;
        private UserManager<AppUser> _usermanager;

        public AuxServices(AppIdentityDbContext db,
            UserManager<AppUser> usermanager
        )
        {
            _db = db;
            _usermanager = usermanager;
        }

        public List<NewAlert> GetNewsAlerts(int max, string type, string publish)
        {

            List<NewAlert> newalerts = new List<NewAlert>();
            NewAlert item;

            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnShared
                    , "[dbo].sp_intra_GetNewAlert"
                    , max       // 0 all
                    , type      // N or A
                    , publish   // "9","0","1" all
                );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = new NewAlert();

                        item.fromDate = reader["fromDate"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["fromDate"]) : "-";
                        item.toDate = reader["toDate"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["toDate"]) : "-";
                        item.Id = (int)reader["id"];
                        item.memo = reader["memo"].ToString(); // System.Net.WebUtility.HtmlDecode(reader["memoenc"].ToString());
                        item.orden = (double)reader["orden"];
                        item.publish = (bool)reader["publish"];
                        item.title = reader["title"].ToString();
                        item.type = reader["type"].ToString();
                        item.index = 0;
                        item.stamp = reader["stamp"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["stamp"]) : "-";
                        newalerts.Add(item);
                    }

                }
                reader.Close();
                newalerts = newalerts.OrderByDescending(al => al.publish).ThenBy(al => al.orden).ToList();


            }
            catch (Exception ex)
            {
                //no data
                var msg = ex.Message;
            }


            return newalerts;

        }
        public NewAlert GetNewAlert(string id)
        {

            NewAlert item = new NewAlert();

            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnShared
                    , "[dbo].sp_intra_GetNewAlertById"
                    , id       // 0 all                 
                );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item.fromDate = reader["fromDate"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["fromDate"]) : "";
                        item.toDate = reader["toDate"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["toDate"]) : "";
                        item.Id = (int)reader["id"];
                        item.memo = System.Net.WebUtility.HtmlDecode(reader["memo"].ToString());
                        item.orden = (double)reader["orden"];
                        item.publish = (bool)reader["publish"];
                        item.title = reader["title"].ToString();
                        item.type = reader["type"].ToString();
                        item.index = 1;
                        item.stamp = reader["stamp"] != DBNull.Value ? string.Format("{0:MM/dd/yyyy}", (DateTime)reader["stamp"]) : "";
                    }

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                //no data
                var msg = ex.Message;
            }

            return item;

        }

        public int CountNewAlerts(string type, string publish)
        {

            try
            {
                var i = SqlHelper.ExecuteScalar(MySettings.ConnShared
                    , "[dbo].sp_intra_CountNewAlert"
                    , type      // N or A
                    , publish   // "9","0","1" all
                );

                if (i != null)
                    return (int)i;
            }
            catch  (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }

            return 0;

        }

        public ResponseModel UpdateNewAlert(
                string id,
                string titulo,
                string fromdate,
                string todate,
                string orden,
                string publish,
                string memo,
                string tipo,
                string userid
            )
        {

            ResponseModel resp = new ResponseModel();
            if (string.IsNullOrEmpty(fromdate))
                fromdate = DateTime.Now.ToShortDateString();                            
            
            if (string.IsNullOrEmpty(todate))
                todate = DateTime.Now.ToShortDateString();                            
            
            try
            {
                string xmemo = memo;
                //string xmemo =  System.Net.WebUtility.HtmlEncode(memo);
                int iresult = 0;
                if (id != "0")
                {
                    iresult = SqlHelper.ExecuteNonQuery(
                        MySettings.ConnShared,
                            "dbo.sp_intra_UpdateNewAlert",
                            id,
                            titulo,
                            fromdate,
                            todate,
                            orden,
                            publish,
                            xmemo,
                            tipo,
                            userid
                    );
                }
                else
                {
                    iresult = SqlHelper.ExecuteNonQuery(
                        MySettings.ConnShared,
                            "dbo.sp_intra_InsertNewAlert",
                            titulo,
                            fromdate,
                            todate,
                            orden,
                            publish,
                            xmemo,
                            tipo,
                            userid
                    );
                }
                
                if (iresult > 0)
                {
                    resp.message = "ok| Datos Actualizados| Noticas/Alerta";
                }
                else
                {
                    resp.message = "er| NO ACTUALIZADO | Noticas/Alertas";
                }
            }
            catch (Exception ex)
            {
                resp.message = "er|" + ex.Message + "| Noticas/Alertas";
            }
            return resp;


        }

        //public ResponseModel UpdateMemos(string sTipo)
        //{
        //    ResponseModel resp = new ResponseModel();
        //    List<NewAlert> nas = new List<NewAlert>();
        //    resp.message = "UPDATE DONE";
        //    nas = GetNewsAlerts(0, sTipo, "9");
        //    string sql = "";
        //    int ireturn = 0;
        //    foreach (NewAlert na in nas)
        //    {
        //        try
        //        {
        //            sql = string.Format("Update dbo.int_notices SET memoenc = '{0}' where id = {1} ", System.Net.WebUtility.HtmlEncode(na.memo), na.Id);
        //            ireturn = SqlHelper.ExecuteNonQuery(MySettings.ConnShared, System.Data.CommandType.Text, sql);
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.message = ex.Message;
        //            return resp;
        //        }

        //    }

        //    return resp;

        //}

    

        #region EVENTOS

        public List<SelectListItem> GetUsuarios(string nivelarea, string oficina, string depts, string userId)
        {

            List<SelectListItem> usuarios = new List<SelectListItem>();
            SelectListItem item; 

            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                    , "[dbo].sp_admin_GetUsuarios",
                   nivelarea,
                   oficina, 
                   depts, 
                   userId
                );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = new SelectListItem();
                        item.Text = reader["Nombre"].ToString();
                        item.Value = reader["Codigo"].ToString();
                  
                        usuarios.Add(item);
                    }

                }
                reader.Close();
                usuarios = usuarios.OrderBy(us => us.Text).ToList();
            
            }
            catch (Exception ex)
            {
                //no data
                var msg = ex.Message;
            }


            return usuarios;       

        }

        public List<SelectListItem> GetUsuariosvB(
                string grupo,
                string inicial, 
                string areas, 
                string codigo, 
                int loginId, 
                string activo, 
                string nombre)
        {

            List<SelectListItem> usuarios = new List<SelectListItem>();
            SelectListItem item;

            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(MySettings.ConnEAMAdmin
                    , "[dbo].sp_admin_GetUsuariosvB",
                    grupo,
                    inicial,
                    areas,
                    codigo,
                    loginId,
                    activo,
                    nombre
                );

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = new SelectListItem();
                        item.Text = reader["Nombre"].ToString();
                        item.Value = reader["Codigo"].ToString();

                        usuarios.Add(item);
                    }

                }
                reader.Close();
                usuarios = usuarios.OrderBy(us => us.Text).ToList();

            }
            catch (Exception ex)
            {
                //no data
                var msg = ex.Message;
            }


            return usuarios;

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


                        item.Fecha = (datos["edFecha"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["edFecha"]));
                        item.FechaAviso = (datos["FechaAviso"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaAviso"]));
                        item.HoraAviso = (datos["HoraAviso"] == DBNull.Value ? "5:00 PM" : ((DateTime)datos["HoraAviso"]).ToShortTimeString());
                        item.FechaCierre = (datos["FechaCierre"] == DBNull.Value ? string.Format("{0:MM/dd/yyyy}", ((DateTime)datos["edFecha"]).AddDays(-9)) : string.Format("{0:MM/dd/yyyy}", (DateTime)datos["FechaCierre"]));
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

        #endregion

        #region AREAS
        public List<Area> GetAreas()
        {

            ResponseModel resp = new ResponseModel();
            List<Area> coll = new List<Area>();
            Area item;

            try
            {

                SqlDataReader dat = SqlHelper.ExecuteReader(MySettings.ConnEdt
                                   , "[dbo].sp_GetAreas"
                                   , 1
                                   );

                if (dat.HasRows)
                {
                    while (dat.Read())
                    {

                        item = new Area();
                        item.areaID = dat["areaId"].ToString();
                        item.Descripcion = dat["descripcion"].ToString();
                        item.Abrev = dat["abrev"].ToString();
                        item.Activa = (bool)dat["activa"];                  

                        coll.Add(item);
                        
                    }

                }

                dat.Close();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }



            return coll;

        }

        #endregion

        


    }



}