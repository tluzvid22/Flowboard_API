using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using API.Helpers;
using MediatR;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace API.Behavior;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IValidator<TRequest>? _validator;
    private readonly string _requestPath;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="validator"></param>
    public ValidationBehavior(IHttpContextAccessor httpContextAccessor, IValidator<TRequest>? validator = null)
    {
        _validator = validator;
        _requestPath = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty;
    }

    /// <summary>
    /// Collects and reports validation errors from MediatR request that has a validator defined.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, options => options.IncludeRuleSets("DataFormatValidation"));

        if (!validationResult.IsValid)
            return ToErrorResult(validationResult);

        validationResult = await _validator.ValidateAsync(request, options => options.IncludeRulesNotInRuleSet(), cancellationToken);

        if (!validationResult.IsValid)
            return ToErrorResult(validationResult);

        return await next();
    }

    private TResponse ToErrorResult(ValidationResult validationResult)
    {
        var result = new TResponse();
        var problemDetailsList = validationResult.Errors.Select(validationFailure => validationFailure.ToProblemDetails(_requestPath)).ToList();

        foreach (var problemDetails in problemDetailsList)
        {
            result.Reasons.Add(new Error(problemDetails.Title).WithMetadata("ProblemDetails", problemDetails));
        }

        return result;
    }
}
