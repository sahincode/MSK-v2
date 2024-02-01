using FluentValidation;

namespace MSK.Business.DTOs.SettingModelDTOs
{
    public class SettingUpdateDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
    public class SettingUpdateDtoValidator : AbstractValidator<SettingUpdateDto>
    {
        public SettingUpdateDtoValidator()
        {
            RuleFor(c => c.Key).NotEmpty().WithMessage("the name  must be added").
              NotNull().WithMessage("the name  can not be null").
              MaximumLength(100).WithMessage("the name  must be less than 100 chars").
              MinimumLength(3).WithMessage("the name  must be greater than 3 chars");

            RuleFor(c => c.Value).NotEmpty().WithMessage("the name  must be added").
                NotNull().WithMessage("the name  can not be null").
                MaximumLength(100).WithMessage("the name  must be less than 100 chars").
                MinimumLength(3).WithMessage("the name  must be greater than 3 chars");

        }
    }
}
