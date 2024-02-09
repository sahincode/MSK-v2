using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.ReferendumModelDTOs
{
    public class ReferendumCreateDto
    {
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public int DecisionId { get; set; }
        public List<int> InfoIds { get; set; }
        public int CalendarPlanId { get; set; }

    }
    public class ReferendumCreateDtoValidator : AbstractValidator<ReferendumCreateDto>
    {
        public ReferendumCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
