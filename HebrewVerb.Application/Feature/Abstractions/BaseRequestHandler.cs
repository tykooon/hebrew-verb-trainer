using HebrewVerb.Application.Interfaces;

namespace HebrewVerb.Application.Feature.Abstractions;

public abstract class BaseRequestHandler
{
    protected IUnitOfWork _unitOfWork;

    public BaseRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

}
