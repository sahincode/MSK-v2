using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.AccredationModelDTOs
{
    public class AccredationCreateDto
    {
        public string Name { get; set; }
        public IFormFile PDFEn { get; set; }
        public IFormFile PDFRu { get; set; }
        public IFormFile PDFAz { get; set; }

    }
    public class AccredationCreateDtoValidator : AbstractValidator<AccredationCreateDto>
    {
        public AccredationCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
