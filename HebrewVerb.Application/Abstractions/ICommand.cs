using ErrorOr;
using MediatR;

namespace HebrewVerb.Application.Abstractions;

public interface ICommand<TResult> : IRequest<ErrorOr<TResult>> 
{
}
