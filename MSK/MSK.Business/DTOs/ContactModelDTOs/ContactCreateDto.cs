using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.ContactModelDTOs
{
    public class ContactCreateDto
    {
        public string Info { get; set; }
        
        

    }
    public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateDtoValidator()
        {
            RuleFor(e => e.Info).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 5000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
