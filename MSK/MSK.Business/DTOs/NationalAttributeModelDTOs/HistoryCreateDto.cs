using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.NationalAttributeModelDTOs
{
    public class NationalAttributeCreateDto
    {
        public string InfoStart { get; set; }
        public string InfoMiddle { get; set; }

        public string InfoEnd { get; set; }

    }
    public class NationalAttributeCreateDtoValidator : AbstractValidator<NationalAttributeCreateDto>
    {
        public NationalAttributeCreateDtoValidator()
        {
            RuleFor(e => e.InfoStart).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.InfoMiddle).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.InfoEnd).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").

                                    MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
        }
    }
}
