﻿using System;

namespace Manisero.YouShallNotPass.SampleApp.Utils
{
    public static class StringExtensions
    {
        public static bool EqualsOrdinalIgnoreCase(this string value, string other)
            => value.Equals(other, StringComparison.OrdinalIgnoreCase);
    }
}
