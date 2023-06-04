using FluentValidation;

public class ModelValidator : AbstractValidator<Model>
{
    public ModelValidator()
    {
        RuleFor(x => x.Data.Length).LessThan(100 * 1024);
    }
}
