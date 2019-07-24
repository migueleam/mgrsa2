
using mgrsa2.Models;
using System.Collections.Generic;

namespace mgrsa2.Common
{
    /// <summary>
    /// Esta clase ha sido creada con la finalidad de crear una comunicación con el modelo, ya sea retornando una respuesta o un objeto.
    /// Ejm: Cuando hacemos un INSERT, posiblemente no se haya realizado el INSERT porque hay un paso previo que debemos hacer, con esta clase podemos especificar cual es el paso previo que falta.
    /// </summary>
    public class ResponseModel
    {
        public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }
        public string href { get; set; }
        public string function { get; set; }
        public int identity { get; set; }
        public List<routeVM> route { get; set; }
        public ResponseModel()
        {
            this.response = false;
            this.message = "";
            this.identity = 0;
            this.route = new List<routeVM>();
        }

        public void SetResponse(bool r, string m = "")
        {
            this.response = r;
            this.message = m;

            if (!r && m == "")
                this.message = "";
        }

    }
}
