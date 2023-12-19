using HebrewVerb.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[ApiController]
//[Area("api")]
//[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IUnitOfWork _unitOfWork;

    public BaseApiController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
