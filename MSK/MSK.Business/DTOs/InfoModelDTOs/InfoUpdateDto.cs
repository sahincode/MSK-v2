using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.InfoModelDTOs
{
    public class InfoUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ? Pdf { get; set; }


    }
    public class InfoUpdateDtoValidator : AbstractValidator<InfoUpdateDto>
    {
        public InfoUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(1000).WithMessage("Can not be greater than 1000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
