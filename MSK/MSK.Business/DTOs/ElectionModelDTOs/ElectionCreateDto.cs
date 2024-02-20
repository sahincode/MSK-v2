using FluentValidation;
using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.ElectionModelDTOs
{
    public class ElectionCreateDto
    {
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public int VotersCount { get; set; }
     
        public int DecisionId { get; set; }
       
        public int CalendarPlanId { get; set; }
      
        public DateTime StartDate { get; set; }
    }
    public class ElectionCreateDtoValidator : AbstractValidator<ElectionCreateDto>
    {
        public ElectionCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
            RuleFor(e => e.StartDate).NotNull().WithMessage("Can not be null").
                                 NotEmpty().WithMessage("Can not be empty").
                                 GreaterThan(DateTime.Now).WithMessage("Start date can not be pass date.");
                                 

        }
    }
}
