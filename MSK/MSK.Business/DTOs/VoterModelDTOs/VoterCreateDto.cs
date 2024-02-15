using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.VoterModelDTOs
{
    public class VoterCreateDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string FinCode { get; set; }
        public DateTime BirthOfDate { get; set; }
        public IFormFile Image { get; set; }


    }
    public class VoterCreateDtoValidator : AbstractValidator<VoterCreateDto>
    {
        public VoterCreateDtoValidator()
        {
            RuleFor(e => e.FullName).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Address).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(500).WithMessage("Can not be greater than 500 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.FinCode).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(7).WithMessage("Can not be greater than 7 digits").
                                  MinimumLength(7).WithMessage("Can not be less than 7 digits");
            RuleFor(e => e.BirthOfDate).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").LessThan(DateTime.Now.AddYears(-18)).WithMessage( "Voter age can not be less than 18 please add valid birth date!");
                                  

        }
    }
}
