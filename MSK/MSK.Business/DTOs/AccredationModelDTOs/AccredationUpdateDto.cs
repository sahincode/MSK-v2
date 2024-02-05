using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.AccredationModelDTOs
{
    public class AccredationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? PDFEn { get; set; }
        public IFormFile? PDFRu { get; set; }
        public IFormFile? PDFAz { get; set; }
    }
    public class AccredationUpdateDtoValidator : AbstractValidator<AccredationUpdateDto>
    {
        public AccredationUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            
        }
    }
}
