using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.CalendarPlanModelDTOs
{
    public class CalendarPlanUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? Pdf { get; set; }
        public int? ReferendumId { get; set; }


    }
    public class CalendarPlanUpdateDtoValidator : AbstractValidator<CalendarPlanUpdateDto>
    {
        public CalendarPlanUpdateDtoValidator()
        {
            RuleFor(e => e.Title).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(1000).WithMessage("Can not be greater than 1000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
