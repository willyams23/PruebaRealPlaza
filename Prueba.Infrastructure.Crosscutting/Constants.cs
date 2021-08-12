namespace Prueba.Infrastructure.Crosscutting
{
    public class Constants
    {
        public static class HttpStatusCodes
        {
            public const int Ok = 200;
            public const int Created = 201;
            public const int BadRequest = 400;
            public const int Unauthorized = 401;
            public const int NotFound = 404;
            public const int MethodNotAllowed = 405;
            public const int ContentTypeNotAllowed = 406;
            public const int ContentTypeWrong = 415;
            public const int InternalServerError = 500;
        }
        public static class LoggingProperties
        {
            public const string ContextId = "TransactionId";
            public const string ClaimAccionGenerica = "accion.generica";
        }
        public static class ESTADO_REGISTRO
        {
            public const int ACTIVO = 1;
            public const int INACTIVO = 0;
            public const int TODOS = -1;
        }

        public enum TIPO_USUARIO
        {
            CLIENTE,
            CONSULTORA
        }
       
        public enum ESTADO_GENERAL
        {
            ACTIVO = 1,
            INACTIVO = 2,
            TODOS = 0
        }

        public const string TOKEN_TYPE_BEARER = "Bearer";
        public const string autorExcel = "Hospital Hipolito Unanue";
        public static class Headers
        {
            public const string ApplicationCode = "application-code";
            public const string ApplicationUser = "application-user";
            public const string Authorization = "authorization";
            public const string ClientVersion = "client-version";
            public const string ContentType = "Content-Type";
            public const string CountryCode = "country-code";
            public const string TransactionId = "transaction-id";
            public const string Terminal = "terminal";
        }

        public static class FormatoFecha
        {
            public const string DiaMesAnio = "dd/MM/yyyy";            
        }

        public static class Labels
        {
            public const string HeadersMandatory = "Header {0} is mandatory.";
            public const string HeadersMaxLength = "Max length of {0} is {1}.";
            public const string HeadersMissing = "Headers parameters are missing.";
            public const string GenericError = "An unhandled error has been detected, please contact support.";
            public const string NotSupportedCountry = "The country is not supported";
            public const string ExpiredToken = "The token has expired";
            public const string Required = "{0} is mandatory.";
            public const string MaxLength = "Max lenght of {0} is {1}.";
            public const string InvalidValue = "{0} is not a valid value for {1}.";
            public const string ResponseMissing = "Response is missing.";
            public const string LoginDisabled = "El inicio de sesión no esta habilitado para tu país.";
            public const string UserDisabled = "El inicio de sesión no esta habilitado para tu usuario.";
            public const string ReCaptchaInvalid = "El código recaptcha es inválido.";
            public const string OriginError = "An unhandled error has been detected in origin, please contact support.";
            public const string InvalidUserPassword = "El usuario y contraseña ingresados son inválidos.";
            public const string UserDoesNotExist = "The user does not exist";
            public const string Activo = "ACTIVO";
            public const string Inactivo = "INACTIVO"; 
            public const string ErorrCargaTarifa = "El campo {0} no puede ser vacio.";
        }

        public static class ContentType
        {
            public const string Excel = "applitation/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string PDF = "application/pdf";
        }

        public const string DUMMY_KEY = "xxyyzz";

        public static class Combo
        {
            public const int ValorDefecto = 0;
            public const string TextoDefecto = ".::SELECCIONAR::.";
        }

        public static class ComboBusqueda
        {
            public const int ValorDefecto = 0;
            public const string TextoDefecto = ".::TDOS::.";
        }

        public static class Pagina
        {
            public const int PageSize = 10;
        }
    }
}
