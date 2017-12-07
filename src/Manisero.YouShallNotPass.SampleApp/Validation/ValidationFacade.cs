using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.SampleApp.Validation.Factories;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public interface IValidationFacade
    {
        ICollection<IValidationErrorMessage> Validate<TValue>(TValue value);
    }

    public class ValidationFacade : IValidationFacade
    {
        private readonly IValidationEngine _validationEngine;
        private readonly IValidationErrorFormattingEngine<IEnumerable<IValidationErrorMessage>> _validationErrorFormattingEngine;
        private readonly IValidationErrorsMerger _validationErrorsMerger;

        public ValidationFacade(
            IUserRepository userRepository)
        {
            _validationEngine = new ValidationEngineFactory().Create(userRepository);
            _validationErrorFormattingEngine = new ValidationErrorFormattingEngineFactory().Create();
            _validationErrorsMerger = new ValidationErrorsMerger();
        }

        public ICollection<IValidationErrorMessage> Validate<TValue>(TValue value)
        {
            var validationResult = _validationEngine.TryValidate(value);

            if (validationResult == null || !validationResult.HasError())
            {
                return null;
            }

            var errorMessages = _validationErrorFormattingEngine.Format(validationResult);

            return _validationErrorsMerger.Merge(errorMessages.ToArray());
        }
    }
}
