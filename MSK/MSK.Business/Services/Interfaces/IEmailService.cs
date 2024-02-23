using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailToUserForConfirmation(UserEmailOption uSerEmailOptions);
    }
}
