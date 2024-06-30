// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Ignore local functions for tests
[assembly: SuppressMessage("Style", "IDE0039:Use local function", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
// Ignore array parameters for tests
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
// Bug still not fixed
// https://github.com/xunit/xunit/issues/2075
[assembly: SuppressMessage("Usage", "xUnit1010:The value is not convertible to the method parameter type", Justification = "<Pending>", Scope = "namespaceanddescendants", Target = "~N:Reception.Extension.Test")]
