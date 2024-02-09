using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.ReferendumModelDTOs
{
    public class ReferendumUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public int DecisionId { get; set; }
        public List<int> InfoIds { get; set; }
        public int CalendarPlanId { get; set; }
    }
    public class ReferendumUpdateDtoValidator : AbstractValidator<ReferendumUpdateDto>
    {
        public ReferendumUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            
        }
    }
}
