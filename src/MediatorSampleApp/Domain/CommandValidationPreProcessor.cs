using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Domain.Behaviors
{
    internal class CommandValidationPreProcessor<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    {
        private readonly IValidator<TCommand> _validator;

        public CommandValidationPreProcessor(IValidator<TCommand> validator)
        {
            _validator = validator;
        }

        public Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validator != null)
            {
                var result = _validator.Validate(request);

                if (!result.IsValid)
                {
                    if (typeof(TResponse) == typeof(CommandResult))
                    {
                        return Task.FromResult((TResponse)Convert.ChangeType(CommandResult.BadRequest(result.ToString()), typeof(TResponse)));
                    }

                    throw new ValidationException(result.Errors);
                }
            }

            return next();
        }
    }
}
