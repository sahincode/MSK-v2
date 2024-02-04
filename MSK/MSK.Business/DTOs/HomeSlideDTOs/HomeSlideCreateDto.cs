using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.HomeSlideDTOs
{
    public class HomeSlideCreateDto
    {
        public string Title { get; set; }
        public string SubDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
    public class HomeSlideCreateDtoValidator : AbstractValidator<HomeSlideCreateDto>
    {
        public HomeSlideCreateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.SubDescription).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Description).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                   
                                    MaximumLength(300).WithMessage("Can not be greater than 300 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
        }
    }
}
