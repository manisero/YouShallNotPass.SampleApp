using System;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic
{
    public static class BetweenValidation
    {
        public class Rule<TValue> : IValidationRule<TValue, Error>
            where TValue : IComparable<TValue>
        {
            public TValue LowerBound { get; set; }
            public TValue UpperBound { get; set; }
        }

        public class Error
        {
            public bool LowerThanLower { get; set; }
        }

        public class Validator<TValue> : IValidator<Rule<TValue>, TValue, Error>
            where TValue : IComparable<TValue>
        {
            public Error Validate(TValue value, Rule<TValue> rule, ValidationContext context)
            {
                if (value.CompareTo(rule.LowerBound) < 0)
                {
                    return new Error { LowerThanLower = true };
                }

                if (value.CompareTo(rule.UpperBound) > 0)
                {
                    return new Error { LowerThanLower = false };
                }

                return null;
            }
        }

        public static Rule<TValue> Between<TValue>(
            this ValidationRuleBuilder<TValue> builder,
            TValue lowerBound,
            TValue upperBound)
            where TValue : IComparable<TValue>
            => new Rule<TValue>
            {
                LowerBound = lowerBound,
                UpperBound = upperBound
            };
    }
}
