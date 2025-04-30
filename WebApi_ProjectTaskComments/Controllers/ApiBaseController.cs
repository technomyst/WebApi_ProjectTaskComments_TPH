using Microsoft.AspNetCore.Mvc;
using WebApi_ProjectTaskComments.Filters;

namespace WebApi_ProjectTaskComments.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class ApiBaseController : ControllerBase
    {
    }
}
