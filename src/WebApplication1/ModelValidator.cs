using System.Net;
using FluentValidation;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.GetArgument<T>(0);
        var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();

        var validationResult = await validator.ValidateAsync(argToValidate!);
        return validationResult.IsValid ? await next.Invoke(context): Results.ValidationProblem(validationResult.ToDictionary());
    }
}
