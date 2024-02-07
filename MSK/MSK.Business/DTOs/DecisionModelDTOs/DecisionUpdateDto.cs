using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.DecisionModelDTOs
{
    public class DecisionUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }


    }
    public class DecisionUpdateDtoValidator : AbstractValidator<DecisionUpdateDto>
    {
        public DecisionUpdateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
          

        }
    }
}
