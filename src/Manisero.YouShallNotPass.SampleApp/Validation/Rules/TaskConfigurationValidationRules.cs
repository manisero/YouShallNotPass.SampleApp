using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public static class TaskConfigurationValidationRules
    {
        // Algorithm1

        public static readonly IValidationRule<double> Algorithm2Parameter = new ValidationRuleBuilder<double>().Min(0d);

        // Algorithm2

        public static readonly IValidationRule<Algorithm3Configuration> Algorithm3Configuration = new ValidationRuleBuilder<Algorithm3Configuration>()
            .All(b => b.Member(
                     x => x.Parameter,
                     _ => new MapValidation.Rule<int?, int>
                     {
                         Mapping = x => x.Value,
                         ToRule = new MinValidation.Rule<int> { MinValue = 0 }
                     }),
                 b => b.If(
                     x => x.Parameter.HasValue,
                     b1 => b1.Member(
                         x => x.DependentParameter,
                         b2 => b2.All(
                             b3 => b3.NotNull(),
                             b3 => new MapValidation.Rule<double?, double>
                             {
                                 Mapping = x => x.Value,
                                 ToRule = new MinValidation.Rule<double> { MinValue = 0d }
                             }))));

        // Algorithm3

        // Algorithm4

        public static readonly IValidationRule<Algorithm4PhaseConfiguration> Algorithm4PhaseConfiguration = new ValidationRuleBuilder<Algorithm4PhaseConfiguration>()
            .Member(x => x.Parameter, b => b.Min(0));


    }
}
