using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProyectoCursoAPI.Web.Extensions
{
    // Metodos de extension sobre ITempDataDictionary para mensajes de alerta uniformes
    public static class TempDataExtensions
    {
        public const string SuccessKey = "SuccessMessage";
        public const string ErrorKey = "ErrorMessage";
        public const string InfoKey = "InfoMessage";

        public static void SetSuccess(this ITempDataDictionary tempData, string message)
        {
            tempData[SuccessKey] = message;
        }

        public static void SetError(this ITempDataDictionary tempData, string message)
        {
            tempData[ErrorKey] = message;
        }

        public static void SetInfo(this ITempDataDictionary tempData, string message)
        {
            tempData[InfoKey] = message;
        }
    }
}
