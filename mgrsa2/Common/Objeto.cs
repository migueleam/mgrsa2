using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace mgrsa2.Common
{
    public class Objeto
    {
        public static string SerializarLista<T>(List<T> lista, char sepCol, char sepRow, bool incluirHeader)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            if (incluirHeader)
            {
                foreach (PropertyInfo propiedad in propiedades)
                {
                    sb.Append(propiedad.Name);
                    sb.Append(sepCol);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(sepRow);
            }

            object valor;
            string tipo;
            foreach (T obj in lista)
            {
                foreach (PropertyInfo propiedad in propiedades)
                {
                    valor = propiedad.GetValue(obj, null);
                    tipo = propiedad.PropertyType.ToString().ToLower();
                    if (valor != null)
                    {
                        if (tipo.Contains("datetime"))
                        {
                            sb.Append(String.Format("{0:d}", valor));
                        }
                        else
                        {
                            sb.Append(valor.ToString());
                        }
                    }
                    else
                    {
                        sb.Append("");
                    }
                    sb.Append(sepCol);
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append(sepRow);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();

        }
        public static string SerializarItem(Object item, char sepCol, char sepRow, bool incluirHeader)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = item.GetType().GetProperties();
            if (incluirHeader)
            {
                foreach (PropertyInfo propiedad in propiedades)
                {
                    sb.Append(propiedad.Name);
                    sb.Append(sepCol);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(sepRow);
            }

            object valor;
            string tipo;
           
                foreach (PropertyInfo propiedad in propiedades)
                {
                    valor = propiedad.GetValue(item, null);
                    tipo = propiedad.PropertyType.ToString().ToLower();
                    if (valor != null)
                    {
                        if (tipo.Contains("datetime"))
                        {
                            sb.Append(String.Format("{0:d}", valor));
                        }
                        else
                        {
                            sb.Append(valor.ToString());
                        }
                    }
                    else
                    {
                        sb.Append("");
                    }
                    sb.Append(sepCol);
                }
                sb = sb.Remove(sb.Length - 1, 1);
                
            return sb.ToString();

        }
        public static string SerializarListaSLI(List<SelectListItem> lista, char sepCol, char sepRow, bool incluirHeader)
        {
            StringBuilder sb = new StringBuilder();
            if (lista.Count == 0)
            {
                return sb.ToString();
            }

            if (incluirHeader)
            {
                sb.Append("Value");
                sb.Append(sepCol);
                sb.Append("Text");
                sb.Append(sepCol);
                sb.Append("Group");
                sb.Append(sepRow);
            }

            foreach (SelectListItem obj in lista)
            {
                sb.Append(obj.Value);
                sb.Append(sepCol);
                sb.Append(obj.Text);
                sb.Append(sepCol);
                sb.Append(obj.Group.Name);
                sb.Append(sepRow);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();

        }

        public static bool CheckContent(object obj)
        {
            Type myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            bool bResult = false;
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(obj, null);
                if (propValue != null && propValue.ToString() != "")
                {
                    bResult = true;
                    break;
                }
            }

            return bResult;

        }


    }
}
