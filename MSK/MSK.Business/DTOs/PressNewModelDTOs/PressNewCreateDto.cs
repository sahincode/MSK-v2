using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.PressNewDTOs
{
    public class PressNewCreateDto
    {
        public string Title { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
    public class PressNewCreateDtoValidator : AbstractValidator<PressNewCreateDto>
    {
        public PressNewCreateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Article).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Description).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                    MaximumLength(300).WithMessage("Can not be greater than 300 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
        }
    }
}
