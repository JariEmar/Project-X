using Api.Contracts.v1.Requests;
using Api.Contracts.v1.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(4)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
