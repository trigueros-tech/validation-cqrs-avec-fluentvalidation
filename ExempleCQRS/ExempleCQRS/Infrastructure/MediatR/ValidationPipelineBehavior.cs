using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ExempleCQRS.Infrastructure.MediatR
{
    public class ValidationPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var errors = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .ToArray();
            
            // Si pas de validateurs (liste vide), pas d'erreurs
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
            
            return next();
        }
    }
}

