﻿using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.GenericSignIn
{
    public sealed class GenericSignInFeatureSignInCommandValidator : AbstractValidator<GenericSignInFeatureSignInCommand>
    {
        public GenericSignInFeatureSignInCommandValidator()
        {
            RuleFor(e => e.UserAgent)
                .MaximumLength(4092)
                .NotEmpty();

            RuleFor(e => e.RemoteAddress)
                .NotEmpty();

            RuleFor(e => e.EmailAddress)
                .MaximumLength(128)
                .NotEmpty();

            RuleFor(e => e.Password)
                .MaximumLength(1024)
                .NotEmpty();

            RuleFor(e => e.Persistent);
        }
    }
}
