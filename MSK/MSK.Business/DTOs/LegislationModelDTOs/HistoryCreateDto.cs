using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.LegislationModelDTOs
{
    public class LegislationCreateDto
    {
        public string Name { get; set; }
        public IFormFile Pdf { get; set; }

      

    }
    public class LegislationCreateDtoValidator : AbstractValidator<LegislationCreateDto>
    {
        public LegislationCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
