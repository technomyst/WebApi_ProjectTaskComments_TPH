using Microsoft.AspNetCore.Mvc.Filters;
using WebApi_ProjectTaskComments.Exceptions;
using WebApi_ProjectTaskComments.Models.Responses;

namespace WebApi_ProjectTaskComments.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {

        }
        public void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            int statusCode = 400;
            ApiErrorResponse? response;

            switch (exception)
            {
                case DuplicateEntityException:
                    {
                        statusCode = 409;
                        response = new ApiErrorResponse
                        {
                            Code = 10,
                            Message = exception.Message,
                            Description = exception.ToString()
                        };
                        break;
                    }
                case EntityNotFoundException:
                    {
                        statusCode = 404;
                        response = new ApiErrorResponse
                        {
                            Code = 20,
                            Message = exception.Message,
                            Description = exception.ToString()
                        };
                        break;
                    }
                case SaveEntityException:
                    {
                        statusCode = 500;
                        response = new ApiErrorResponse
                        {
                            Code = 30,
                            Message = exception.Message,
                            Description = exception.ToString()
                        };
                        break;
                    }
                default:
                    {
                        response = new ApiErrorResponse()
                        {
                            Code = -1,
                            Message = exception.Message,
                            Description = exception.ToString()
                        };
                        break;
                    }
            }
        }
    }
}
