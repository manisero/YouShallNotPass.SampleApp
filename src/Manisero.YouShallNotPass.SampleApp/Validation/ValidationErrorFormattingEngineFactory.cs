using Manisero.YouShallNotPass.ErrorFormatting;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationErrorFormattingEngineFactory
    {
        public IValidationErrorFormattingEngine<object> Create()
        {
            return new ValidationErrorFormattingEngineBuilder<object>()
                .Build();
        }
    }
}
