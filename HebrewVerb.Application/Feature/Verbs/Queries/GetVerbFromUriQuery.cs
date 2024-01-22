using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbFromUriQuery(string url,bool passive = false) : IRequest<VerbDto>
{
}
