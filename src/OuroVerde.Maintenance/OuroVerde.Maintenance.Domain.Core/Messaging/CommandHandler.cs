﻿using FluentValidation.Results;
using OuroVerde.Maintenance.Domain.Core.Data;

namespace OuroVerde.Maintenance.Domain.Core.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow, string message)
        {
            if (!(await uow.Commit()))
            {
                AddError(message);
            }

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            return await Commit(uow, "There was an error saving data").ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
