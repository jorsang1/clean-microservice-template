﻿using FluentValidation;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Product.ValueObjects;

internal class ProjectTitleValidator : AbstractValidator<ProjectTitle>
{
    public const int MinLength = 5;
    public const int MaxLength = 350;

    public ProjectTitleValidator()
    {
        RuleFor(projectTitle => projectTitle.Title)
            .NotEmpty()
                .WithErrorCode("Project_Title_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Project_Title_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Project_Title_MaximumLength");
    }
}
