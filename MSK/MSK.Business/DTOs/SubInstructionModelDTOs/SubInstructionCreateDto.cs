using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.SubInstructionModelDTOs
{
    public class SubInstructionCreateDto
    {
        public string Title { get; set; }
        public int InstructionId { get; set; }
        public IFormFile Pdf { get; set; }

      

    }
    public class SubInstructionCreateDtoValidator : AbstractValidator<SubInstructionCreateDto>
    {
        public SubInstructionCreateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
