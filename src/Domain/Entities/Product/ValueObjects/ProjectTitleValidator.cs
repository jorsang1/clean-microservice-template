using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using FluentValidation;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Product.ValueObjects;

public class ProjectTitleValidator : AbstractValidator<ProjectTitle>
{
    public const int MinLength = 5;
    public const int MaxLength = 350;

    public ProjectTitleValidator()
    {
        RuleFor(projectTitle => projectTitle.Value)
            .NotEmpty()
                .WithErrorCode("Title_NotEmpty")
            .MinimumLength(MinLength)
                .WithAutomaticErrorCode(this)
            .MaximumLength(MaxLength)
                .WithErrorCode("Title_MaximumLength");
    }
}
