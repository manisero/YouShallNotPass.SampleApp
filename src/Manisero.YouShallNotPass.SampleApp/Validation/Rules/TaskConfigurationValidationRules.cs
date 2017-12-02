﻿using System.Linq;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public static class TaskConfigurationValidationRules
    {
        public static readonly IValidationRule<TaskConfiguration> TaskConfiguration = new ValidationRuleBuilder<TaskConfiguration>()
            .All(b => b.Member(x => x.Algorithm, b1 => b1.IsEnumValue()),
                 b => b.If(
                     x => x.Algorithm == Algorithm.Algorithm2,
                     b1 => b1.Member(
                         x => x.Algorithm2Parameter,
                         b2 => b2.All(
                             b3 => b3.NotNull(),
                             b3 => b3.Map(x => x.Value, _ => Algorithm2Parameter)))
                     /* TODO: else */),
                 b => b.If(
                     x => x.Algorithm == Algorithm.Algorithm3,
                     b1 => b1.Member(
                         x => x.Algorithm3Configuration,
                         b2 => b2.All(
                             b3 => b3.NotNull(),
                             _ => Algorithm3Configuration))
                     /* TODO: else */),
                 b => b.If(
                     x => x.Algorithm == Algorithm.Algorithm4,
                     b1 => b1.Member(
                         x => x.Algorithm4Configuration,
                         b2 => b2.All(
                             b3 => b3.NotNull(),
                             _ => Algorithm4Configuration))
                     /* TODO: else */));

        // Algorithm2

        public static readonly IValidationRule<double> Algorithm2Parameter = new ValidationRuleBuilder<double>().Min(0d);

        // Algorithm3

        public static readonly IValidationRule<Algorithm3Configuration> Algorithm3Configuration = new ValidationRuleBuilder<Algorithm3Configuration>()
            .All(b => b.Member(
                     x => x.Parameter,
                     b1 => b1.Map(x => x.Value, b2 => b2.Min(0))),
                 b => b.If(
                     x => x.Parameter.HasValue,
                     b1 => b1.Member(
                         x => x.DependentParameter,
                         b2 => b2.All(
                             b3 => b3.NotNull(),
                             b3 => b3.Map(x => x.Value, b4 => b4.Min(0d))))));

        // Algorithm4

        public static readonly IValidationRule<Algorithm4Configuration> Algorithm4Configuration = new ValidationRuleBuilder<Algorithm4Configuration>()
            .All(b => b.Member(x => x.PhasesNumber, b1 => b1.Min(1)),
                 _ => new Algorithm4ConfigurationPhasesKeysValidation.Rule(),
                 b => b.Member(
                     x => x.Phases,
                     b1 => b1.Map(
                         x => x.Values.AsEnumerable(),
                         b2 => b2.Collection(_ => Algorithm4PhaseConfiguration))));

        public static readonly IValidationRule<Algorithm4PhaseConfiguration> Algorithm4PhaseConfiguration = new ValidationRuleBuilder<Algorithm4PhaseConfiguration>()
            .Member(x => x.Parameter, b => b.Min(0));
    }
}
