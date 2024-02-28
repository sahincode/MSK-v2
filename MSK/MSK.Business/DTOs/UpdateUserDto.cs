using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs
{
    public class UpdateUserDto
    {
        public IFormFile? Image { get;set; }
        public string UserName { get;set; }
        [DataType(DataType.Password)]
        public string? Password { get;set; }
        public string? NewPassword { get;set; }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().WithMessage("can not be null").
                NotEmpty().WithMessage("can not be empty.").
                MinimumLength(5).WithMessage("can not be less than 5 element.")
                .MaximumLength(100).WithMessage("can not be more than 100 element.");

        }
    }
}
