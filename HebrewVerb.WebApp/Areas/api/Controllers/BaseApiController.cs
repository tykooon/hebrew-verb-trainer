using HebrewVerb.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[ApiController]
//[Area("api")]
//[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IUnitOfWork _unitOfWork;
    protected IMediator _mediator;

    public BaseApiController(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }
}
