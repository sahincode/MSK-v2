using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.LegislationModelDTOs
{
    public class LegislationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ? Pdf { get; set; }


    }
    public class LegislationUpdateDtoValidator : AbstractValidator<LegislationUpdateDto>
    {
        public LegislationUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
