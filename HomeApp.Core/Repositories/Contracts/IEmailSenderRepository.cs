using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Repositories.Contracts
{
    public interface IEmailSenderRepository
    {
        Task SendEmailConfirmationAsync(string email, string callbackUrl, Language language);
    }
}
