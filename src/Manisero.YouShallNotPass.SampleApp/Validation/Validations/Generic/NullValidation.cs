namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic
{
    public static class NullValidation
    {
        public const string Code = "Null";

        public class Rule<TValue> : IValidationRule<TValue, Error>
        {
        }

        public class Error
        {
            public static readonly Error Instance = new Error();
        }

        public class Validator<TValue> : IValidator<Rule<TValue>, TValue, Error>
        {
            public Error Validate(TValue value, Rule<TValue> rule, ValidationContext context)
            {
                return Error.Instance;
            }
        }

        public static Rule<TValue> Null<TValue>(this ValidationRuleBuilder<TValue> builder)
            => new Rule<TValue>();
    }
}
