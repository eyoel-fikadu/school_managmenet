using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SMS.SERVICE.SMSBasic;
using System.Net;

namespace SMS.SERVICE.ServiceLayer.Exception_Handler
{
    public static class SCMSExceptionHandler
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");

                        
                        if (exception is SCSMExceptionList)
                        {
                            var ex = (SCSMExceptionList)exception;
                            SCMSErrorListResponse response = new SCMSErrorListResponse();
                            CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
                            response.errors = CommonMethods.GetErrorResponse(ex.errorMessages);

                            await context.Response.WriteAsync(response.ToString());
                        }
                        else if (exception is SCMSException)
                        {
                            var ex = (SCMSException)exception;

                            await context.Response.WriteAsync(new SCMSResponse()
                            {
                                responseCode = ex.responseCode,
                                responseMessage = ex.responseMessage
                            }.ToString());
                        }
                        else
                        {
                            
                        }
                        
                    }
                });
            });
        }
    }
}
