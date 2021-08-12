﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting.Comun
{
    public static class Genericas
    {
        public static bool ObtenerPaginaSiguiente(decimal total, decimal tamaniopagina, int requestpagina)
        {
            if (tamaniopagina <= 0)
                return false;
            decimal pagina = total / tamaniopagina;
            int cantidadActual = Convert.ToInt32(Math.Ceiling(pagina));
            int paginaActual = requestpagina + 1;

            if (paginaActual <= cantidadActual)
                return true;
            else
                return false;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                    values[i] = Props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string ObtenerFechaConFormato(DateTime? fecha, string formato)
        {
            if (fecha != null)
            {
                return Convert.ToDateTime(fecha).ToString(formato);
            }

            return string.Empty;
        }
    }
}
