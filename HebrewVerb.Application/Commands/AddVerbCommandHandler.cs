using Ardalis.GuardClauses;
using ErrorOr;
using HebrewVerb.Core;
using MediatR;

namespace HebrewVerb.Application.Commands;

public class AddVerbCommandHandler : IRequestHandler<AddVerbCommand, ErrorOr<Verb>>
{
    private IUnitOfWork _unitOfWork;

    public AddVerbCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<ErrorOr<Verb>> Handle(AddVerbCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request.NewVerb, nameof(request.NewVerb));
        _unitOfWork.VerbRepo.Add(request.NewVerb);
        _unitOfWork.Commit();
        ErrorOr<Verb> res = request.NewVerb.Id == 0 ? Error.Failure() : request.NewVerb; 
        return Task.FromResult(res);
    }
}
