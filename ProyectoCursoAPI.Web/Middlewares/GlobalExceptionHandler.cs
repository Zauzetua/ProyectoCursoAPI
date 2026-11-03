using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Middlewares
{
    /// <summary>
    /// Manejador global de excepciones basado en IExceptionHandler y el estándar RFC 9457 (Problem Details).
    /// Evita fugas de información sensible (como StackTraces) en producción y estandariza las respuestas de error.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Excepción capturada por GlobalExceptionHandler: {Message}", exception.Message);

            var (statusCode, title, detail, errors) = exception switch
            {
                ValidationException validationEx => (
                    StatusCodes.Status400BadRequest,
                    "Error de validación",
                    "Se presentaron uno o más errores de validación.",
                    (IDictionary<string, string[]>?)validationEx.Errors
                ),
                DomainException domainEx => (
                    StatusCodes.Status400BadRequest,
                    "Regla de negocio inválida",
                    domainEx.Message,
                    null
                ),
                NotFoundException notFoundEx => (
                    StatusCodes.Status404NotFound,
                    "Recurso no encontrado",
                    notFoundEx.Message,
                    null
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor",
                    _environment.IsDevelopment()
                        ? exception.Message
                        : "Ocurrió un error inesperado en el servidor.",
                    null
                )
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path,
                Type = "https://datatracker.ietf.org/doc/html/rfc9457"
            };

            if (errors is not null)
            {
                problemDetails.Extensions["errors"] = errors;
            }

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
