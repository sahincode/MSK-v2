﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.InstructionModelDTOs
{
    public class InstructionCreateDto
    {
        public string Name { get; set; }
        
        

    }
    public class InstructionCreateDtoValidator : AbstractValidator<InstructionCreateDto>
    {
        public InstructionCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(200).WithMessage("Can not be greater than 200 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 3 digits");
           
        }
    }
}
