using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.InstructionModelDTOs
{
    public class InstructionUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


    }
    public class InstructionUpdateDtoValidator : AbstractValidator<InstructionUpdateDto>
    {
        public InstructionUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
          

        }
    }
}
