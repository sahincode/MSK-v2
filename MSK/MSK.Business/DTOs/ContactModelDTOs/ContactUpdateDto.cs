using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.ContactModelDTOs
{
    public class ContactUpdateDto
    {
        public int Id { get; set; }
        public string Info { get; set; }


    }
    public class ContactUpdateDtoValidator : AbstractValidator<ContactUpdateDto>
    {
        public ContactUpdateDtoValidator()
        {
            RuleFor(e => e.Info).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(5000).WithMessage("Can not be greater than 5000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
          

        }
    }
}
