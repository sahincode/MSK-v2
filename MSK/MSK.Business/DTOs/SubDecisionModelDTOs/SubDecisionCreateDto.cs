using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.SubDecisionModelDTOs
{
    public class SubDecisionCreateDto
    {
        public string Title { get; set; }
        public int DecisionId { get; set; }
        public IFormFile Pdf { get; set; }

      

    }
    public class SubDecisionCreateDtoValidator : AbstractValidator<SubDecisionCreateDto>
    {
        public SubDecisionCreateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(800).WithMessage("Can not be greater than 800 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
