using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters
{
    /// <summary>
    /// Filtro global para mapear excepciones de aplicación a códigos HTTP estandarizados.
    /// </summary>
    public class HttpExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            (int status, string title) = ex switch
            {
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Recurso no encontrado"),
                InvalidOperationException ioe when IsUniquenessError(ioe) => ((int)HttpStatusCode.Conflict, "Conflicto de unicidad"),
                ArgumentException or ArgumentOutOfRangeException => ((int)HttpStatusCode.BadRequest, "Solicitud inválida"),
                _ => ((int)HttpStatusCode.InternalServerError, "Error interno del servidor")
            };

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = BuildDetail(ex),
                Instance = context.HttpContext?.TraceIdentifier
            };

            context.Result = new ObjectResult(problem) { StatusCode = status };
            context.ExceptionHandled = true;
        }

        private static bool IsUniquenessError(InvalidOperationException ex)
        {
            var m = ex.Message ?? string.Empty;
            return m == "NUMERO_MOTOR_DUPLICADO" || m == "NUMERO_CHASIS_DUPLICADO";
        }

        private static string BuildDetail(Exception ex)
        {
            // Devolvemos un mensaje acotado para no filtrar información sensible.
            return ex.Message;
        }
    }
}
