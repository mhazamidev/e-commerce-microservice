using FluentValidation.Results;

namespace IdentityService.API.Core.CQRS.QueryHandling;

/// <summary>
/// QueryHandler result class
/// </summary>
/// <typeparam name="TResult"></typeparam>
public record class QueryHandlerResult<TResult>
{
    public ValidationResult ValidationResult { get; }

    public TResult Result { get; set; }

    public QueryHandlerResult(IQuery<QueryHandlerResult<TResult>> query)
    {
        ValidationResult = query.Validate();
    }
}