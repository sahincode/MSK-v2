using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.SubInstructionModelDTOs
{
    public class SubInstructionUpdateDto
    {
        public int Id { get; set; }
        public int InstructionId { get; set; }
        public string Title { get; set; }
        public IFormFile? Pdf { get; set; }


    }
    public class SubInstructionUpdateDtoValidator : AbstractValidator<SubInstructionUpdateDto>
    {
        public SubInstructionUpdateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(800).WithMessage("Can not be greater than 800 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");

        }
    }
}
