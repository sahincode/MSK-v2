using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MSK.Business.DTOs.CandidateModelDTOs
{
    public class CandidateCreateDto
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        public int VotedCount { get; set; }
        public string About { get; set; }
        public string Party { get; set; }
        public double VotedPercent { get; set; }
        public IFormFile Image{ get; set; }
        public int ? ElectionId { get; set; }
        



    }
    public class CandidateCreateDtoValidator : AbstractValidator<CandidateCreateDto>
    {
        public CandidateCreateDtoValidator()
        {
            RuleFor(e => e.About).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(5000).WithMessage("Can not be greater than 5000 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.FullName).NotNull().WithMessage("Can not be null").
                                 NotEmpty().WithMessage("Can not be empty").
                                 MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                 MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Party).NotNull().WithMessage("Can not be null").
                                 NotEmpty().WithMessage("Can not be empty").
                                 MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                 MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.Profession).NotNull().WithMessage("Can not be null").
                                 NotEmpty().WithMessage("Can not be empty").
                                 MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                 MinimumLength(3).WithMessage("Can not be less than 3 digits");
           

        }
    }
}
