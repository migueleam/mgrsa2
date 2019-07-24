using System;

namespace mgrsa2.Models
{

    public class Location
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime Stamp { get; set; }
        public DateTime EditStamp { get; set; }

    }

    /*
    public class LocationDist
    {
       



        public Location()
        {
            //[ar
            this.locationID = 0;
            this.Descripcion = "";
            this.Address = "";
            this.City = "";
            this.Zip = "";
            this.tipoUbicacionID = 0;
            this.Qty = 0;
            this.Active = 9;
        }
        
        public int locationID { get; set; } //0            
        public string Descripcion { get; set; } //
        public string Address { get; set; }
        public string City { get; set; }          
        public string Zip { get; set; } 
        public int tipoUbicacionID { get; set; }         //5
        public int Qty { get; set; }   
        public int Orden { get; set; }
        public string EditionDeActive { get; set; } //8 
        public int Active { get; set; }       //9
        public double Latitude { get; set; }  //10
        public double Longitude { get; set; } //11



        public string tipoUbicacionDesc { get; set; }  //12
        public string tipoUbicacionAbrev { get; set; } //13


        public string vsearch { get; set; }



    }    
    public class LocationSRA
    {

        public LocationSRA()
        {
            this.tipoUbicacionID = 0;
            this.tuDescripcion = "";
            this.tuAbrev = "";

            //l
            this.locationID = 0;
            this.lDescripcion = "";
            this.lActive = 0;
            this.EditionDeActive = "";
            this.Latitude = 0.0;
            this.Longitude = 0.0;
            this.Atiende = "";

            //sl 
            this.locationScheduleID = 0;
            this.Draw = 0;

            //sched     
            this.scheduleID = 0;
            this.sDescripcion = "";
            this.Dia = "";
            this.sType = "";

            //route
            this.RouteID = 0;
            this.RouteNum = 0;
            this.rDescription = "";

            //area
            this.areaID = "";
            this.aDescripcion = "";
            

        }
        //tu
        public int tipoUbicacionID { get; set; }
        public string tuDescripcion { get; set; }
        public string tuAbrev { get; set; }

        //l
        public int locationID { get; set; }               
        public string lDescripcion { get; set; }      
        public int lActive { get; set; }
        public string EditionDeActive { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Atiende { get; set; }
        
        //sl 
        public int locationScheduleID { get; set; }
        public int Draw { get; set; }

        //sched     
        public int scheduleID { get; set; }
        public string sDescripcion { get; set; }
        public string Dia { get; set; }
        public string sType { get; set; }

        //route
        public int RouteID { get; set; }
        public int RouteNum { get; set; }
        public string rDescription { get; set; }      
        
        //area
        public string areaID { get; set; }
        public string aDescripcion { get; set; }             

        //public string vsearch { get; set; }
        

    }
    public class LocationSchd
    {

        public LocationSchd()
        {
            //[ar
            this.locationID = 0;
            this.Descripcion = "";
            this.Address = "";
            this.City = "";
            this.Zip = "";
            this.tipoUbicacionID = 0;
            this.Qty = 0;
            this.Active = 9;

            this.locationScheduleID = 0;
            this.ScheduleID = 0;
            this.newOrden = 0;
            this.draw = 0;

        }

        public int locationID { get; set; } //0            
        public string Descripcion { get; set; } //
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public int tipoUbicacionID { get; set; }         //5
        public int Qty { get; set; }
        public int Orden { get; set; }
        public string EditionDeActive { get; set; } //8 
        public int Active { get; set; }       //9
        public double Latitude { get; set; }  //10
        public double Longitude { get; set; } //11

        public string tipoUbicacionDesc { get; set; }  //12
        public string tipoUbicacionAbrev { get; set; } //13
        public string vsearch { get; set; }


        //FOR ORDEN... BY SCHEDULE

        public int locationScheduleID { get; set; } //15
        public int ScheduleID { get; set; }
        public int newOrden { get; set; }
        public int draw { get; set; }



    }
    
    public class LocationRet
    {

        public LocationRet()
        {         
           
            this.locationID = 0;           
            this.Descripcion = "";
            this.Address = "";
            this.City = "";
            this.Zip = "";
            this.tipoUbicacionID = 0;
            this.Qty = 0;
            this.Active = 9;

            this.locationScheduleID = 0;
            this.ScheduleID = 0;
            this.newOrden = 0;
            this.draw = 0;

            this.eventoID = 0;
            this.edicion = "";
            this.loginID = 0;
        }
               
        public int locationID { get; set; } //0   
        public string Descripcion { get; set; } //
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public int tipoUbicacionID { get; set; }         //5
        public int Qty { get; set; }
        public int Orden { get; set; }
        public string EditionDeActive { get; set; } //8 
        public int Active { get; set; }       //9
        public double Latitude { get; set; }  //10
        public double Longitude { get; set; } //11

        public string tipoUbicacionDesc { get; set; }  //12
        public string tipoUbicacionAbrev { get; set; } //13
        public string vsearch { get; set; }


        //FOR ORDEN... BY SCHEDULE

        public int locationScheduleID { get; set; } //15
        public int ScheduleID { get; set; }
        public int newOrden { get; set; }
        public int draw { get; set; }         //18 

        public int eventoID { get; set; }     //19             
        public string edicion { get; set; }   //20     
        public int loginID { get; set; }      //21

        
    }
    */

}
