using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.ElectionModelDTOs
{
    public class ElectionUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public int VotersCount { get; set; }

        public int DecisionId { get; set; }

        public int CalendarPlanId { get; set; }

        public DateTime StartDate { get; set; }
    }
    public class ElectionUpdateDtoValidator : AbstractValidator<ElectionUpdateDto>
    {
        public ElectionUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            
        }
    }
}
