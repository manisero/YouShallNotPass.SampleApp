using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public static class Algorithm4ConfigurationPhasesKeysValidation
    {
        public const string Code = "Algorithm4ConfigurationPhasesKeys";

        public class Rule : IValidationRule<Algorithm4Configuration, Error>
        {
        }

        public class Error
        {
            public ICollection<int> InvalidKeyIndices { get; set; }
        }

        public class Validator : IValidator<Rule, Algorithm4Configuration, Error>
        {
            public Error Validate(Algorithm4Configuration value, Rule rule, ValidationContext context)
            {
                if (value.Phases == null)
                {
                    return null;
                }

                var phasesNumber = value.PhasesNumber;
                var phaseKeys = value.Phases.Keys;

                var phaseKeyRule = new ValidationRuleBuilder<IEnumerable<int>>()
                    .Collection(b => b.Between(1, phasesNumber));
                
                var keysValidationResult = context.Engine.Validate<CollectionValidation.Rule<int>, IEnumerable<int>, CollectionValidation.Error>(phaseKeys.AsEnumerable(), phaseKeyRule);

                return keysValidationResult.HasError()
                    ? new Error { InvalidKeyIndices = keysValidationResult.Error.Violations.Keys }
                    : null;
            }
        }
    }
}
