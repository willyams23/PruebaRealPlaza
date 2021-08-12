using System;
using System.Collections.Generic;

namespace Prueba.Infrastructure.Crosscutting.ExceptionsTypes
{
    public static class ExceptionManager
    {
        /// <summary>
        /// En base a un código de error genera un AppException. Se debe utilizar para reglas de negocio
        /// </summary>        
        /// <param name="mensaje">Mensaje de la regla de negocio</param>        
        public static void GenerarAppExcepcionReglaNegocio(string codigo, string mensaje)
        {

            GenerarAppExcepcion(TipoException.ReglaNegocio, codigo, mensaje, null, null);
        }

        /// <summary>
        /// En base a un código de error genera un AppException. Se debe utilizar para validar las entidades de negocio
        /// </summary>           
        /// <param name="mensaje">Mensaje de la validación</param>
        /// <param name="mensajes">Lista de mensajes de validación</param>
        public static void GenerarAppExcepcionValidacion(string mensaje, List<string> mensajes)
        {
            GenerarAppExcepcion(TipoException.Validacion, null, mensaje, mensajes, null);
        }

        /// <summary>
        /// En base a un código de error genera un AppException. Se debe utilizar para validar las entidades de negocio
        /// </summary>             
        /// <param name="mensaje">Mensaje de la validación</param>
        /// <param name="mensajes">Lista de mensajes de validación</param>
        public static void GenerarAppExcepcionNoAutorizado(string mensaje, List<string> mensajes)
        {
            GenerarAppExcepcion(TipoException.NoAutorizado, null, mensaje, mensajes, null);
        }

        public static void GenerarAppExcepcionInterna(string mensaje, List<string> mensajes)
        {
            GenerarAppExcepcion(TipoException.Interna, null, mensaje, mensajes, null);
        }

        /// <summary>
        /// En base a un código de error genera un AppException. Se debe utilizar para cualquier tipo de excepción genérica
        /// </summary>
        /// <param name="codigo">Código del error</param>
        public static void GenerarAppExcepcionGenerica(string mensaje, Exception ex)
        {
            GenerarAppExcepcion(TipoException.Otro, null, mensaje, null, ex);
        }

        /// <summary>
        /// En base a un código de error se genera un AppException. Se debe utilizar para excepciones en llamadas a
        /// servicios externos
        /// </summary>
        /// <param name="codigo">Código del error</param>
        /// <param name="ex">Exception del error</param>
        private static void GenerarAppExcepcion(TipoException tipo, string codigo, string mensaje, List<string> mensajes, Exception ex)
        {
            throw new AppException(tipo, codigo, mensaje, mensajes, ex);
        }
    }
}
