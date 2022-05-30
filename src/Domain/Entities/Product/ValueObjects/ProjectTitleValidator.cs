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
                .WithErrorCode("Project_Title_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Project_Title_MinumumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Project_Title_MaximumLength");
    }
}
