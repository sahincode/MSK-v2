using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Business.DTOs.PressNewDTOs;

namespace MSK.Business.DTOs.HistoryModelDTOs
{
    public class HistoryUpdateDto
    {
        public int Id { get; set; }
        public string InfoStart { get; set; }
        public string InfoMiddle { get; set; }
        public string InfoEnd { get; set; }

    }
    public class HistoryUpdateDtoValidator : AbstractValidator<HistoryUpdateDto>
    {
        public HistoryUpdateDtoValidator()
        {
            RuleFor(e => e.InfoStart).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.InfoMiddle).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.InfoEnd).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").

                                    MaximumLength(8000).WithMessage("Can not be greater than 8000 digits").
                                   MinimumLength(3).WithMessage("Can not be less than 3 digits");

        }
    }
}
