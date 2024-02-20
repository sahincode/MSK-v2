using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.InfoModelDTOs
{
    public class InfoCreateDto
    {
        public string Name { get; set; }
        public IFormFile Pdf { get; set; }
        public int? ReferendumId { get; set; }
        public int? ElectionId { get; set; }



    }
    public class InfoCreateDtoValidator : AbstractValidator<InfoCreateDto>
    {
        public InfoCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(1000).WithMessage("Can not be greater than 1000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
