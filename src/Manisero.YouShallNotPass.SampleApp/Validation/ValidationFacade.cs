using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting;
using System.Collections.Generic;
using System.Linq;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public interface IValidationFacade
    {
        IValidationErrorMessage Validate<TValue>(TValue value);
    }

    public class ValidationFacade : IValidationFacade
    {
        private readonly IValidationEngine _validationEngine;
        private readonly IValidationErrorFormattingEngine<IEnumerable<IValidationErrorMessage>> _validationErrorFormattingEngine;

        public ValidationFacade(
            IUserRepository userRepository)
        {
            _validationEngine = new ValidationEngineFactory().Create(userRepository);
            _validationErrorFormattingEngine = new ValidationErrorFormattingEngineFactory().Create();
        }

        public IValidationErrorMessage Validate<TValue>(TValue value)
        {
            var validationResult = _validationEngine.TryValidate(value);

            if (validationResult == null || !validationResult.HasError())
            {
                return null;
            }

            var validationError = _validationErrorFormattingEngine.Format(validationResult);

            return validationError.Single();
        }
    }
}
