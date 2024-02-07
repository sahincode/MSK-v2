using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.SubDecisionModelDTOs
{
    public class SubDecisionUpdateDto
    {
        public int Id { get; set; }
        public int DecisionId { get; set; }
        public string Title { get; set; }
        public IFormFile ? Pdf { get; set; }


    }
    public class SubDecisionUpdateDtoValidator : AbstractValidator<SubDecisionUpdateDto>
    {
        public SubDecisionUpdateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
