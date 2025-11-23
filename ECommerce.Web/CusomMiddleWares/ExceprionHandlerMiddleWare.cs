using ECommerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.Web.CusomMiddleWares
{
    public class ExceprionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceprionHandlerMiddleWare> logger;

        public ExceprionHandlerMiddleWare(RequestDelegate Next, ILogger<ExceprionHandlerMiddleWare> logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Somthing Went Wrong");
                var Problem = new ProblemDetails()
                {
                    Title = "An Error Occured While Processing Your Request",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    },
                };
                httpContext.Response.StatusCode = Problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Problem = new ProblemDetails()
                {
                    Title = "The Resource You Are Looking For Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = "The Resource You Are Looking For Not Found",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
